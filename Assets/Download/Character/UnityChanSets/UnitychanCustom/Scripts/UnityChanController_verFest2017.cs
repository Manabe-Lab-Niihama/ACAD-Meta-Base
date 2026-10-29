using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityChanController_verFest2017 : MonoBehaviour {
	[Header("メインターゲット:Camera(eye)をセット")]
	public GameObject target;
	[Header("視認距離[m](ターゲットの追跡をあきらめる距離)")]
	public float target_length = 3f;
	[Header("視認距離(ターゲットの追跡をあきらめる時間(目安/整数))")]
	public int target_time = 1000;
	[Header("サブターゲット(巡回対象)をセット")]
	public GameObject[] sub_target = new GameObject[5];
	[Header("追跡頻度[%](整数で)")]
	public int[] sub_target_rate = new int[5];
	[Header("接触時間(対象のもとにいる時間(目安/整数))")]
	public int[] sub_target_stay = new int[5];
	//コンポーネント
	NavMeshAgent agent;
	Animator animator;

	[Header("**** デバッグ用内部変数確認 ****")]
	//メインターゲット追跡中
	public bool trace_main = false;
	//サブターゲット追跡中
	public bool trace_sub = false;
	//ターゲットまでの距離
	public float remain_distance;
	//追跡時間
	public int now_tracing_time;
	//コミュニケーション時間
	public int now_stay_time;
	//待機時間
	public int wait;
	public int waitMode_flag = 0;
	public int waitMode_counter = 0;
	//サブターゲット追跡相手
	public int sub_target_ID = 0;
	//サブターゲット数
	public int num_target = 0;
	//サブターゲット追跡率合計
	public int total_rate = 0;

	[Header("**** デバッグ用内部変数確認(汎用) ****")]
	//汎用チェッカー
	public int checkerInt_1;
	public int checkerInt_2;

	// Use this for initialization
	void Start () {
		//[CameraRig]情報だけはPrefab化できないので自動設定
		if (target == null)
			target = GameObject.FindWithTag ("MainCamera");
		//コンポーネント割り当て
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		//フラグ初期化
		waitMode_flag = 0;
		waitMode_counter = 200;

		//サブターゲットチェック
		num_target = 0;
		foreach (GameObject checkobject in sub_target) {
			if (checkobject != null) {
				num_target++;
			} else {
				break;
			}
		}
		total_rate = 0;
		for (int i = 0; i < num_target; i++)
			total_rate += sub_target_rate [i];
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos;

		//メインターゲットを追跡中か？
		if (trace_main || trace_sub) {
			//***** ターゲット追跡中 *****
			if (wait <= 0) {
				this.GetComponent<Rigidbody> ().isKinematic = false;
				//遅延がないとき
				//ターゲットまでの距離を算出し…
				if (trace_sub)
					pos = sub_target [sub_target_ID].transform.position;
				else
					pos = target.transform.position;
				agent.SetDestination (pos);
				remain_distance = agent.remainingDistance;
				//ターゲットの方向を向き…
				//pos = target.transform.position;
				pos.y = this.transform.position.y;
				this.transform.LookAt (pos);
				//メインターゲットが遠すぎるとき
				if (trace_main && remain_distance >= target_length) {
					agent.speed = 0.0f;
					animator.SetInteger ("Mode", 0);
					trace_main = false;
					trace_sub = false;
					return;
				}
				//追跡中時間を減じ続ける
				now_tracing_time--;
				//ターゲットにたどり着けないとき
				if (now_tracing_time <= 0) {
					agent.speed = 0.0f;
					animator.SetInteger ("Mode", 0);
					trace_main = false;
					trace_sub = false;
					return;
				}
				//コミュニケーション時間が無くなる
				if (trace_sub && now_stay_time <= 0) {
					//now_stay_time = sub_target_stay [sub_target_ID]; 
					agent.speed = 0.0f;
					animator.SetInteger ("Mode", 0);
					trace_main = false;
					trace_sub = false;
					return;
				}

				//ターゲットまでの距離に応じてアクション
				if (remain_distance < 1.2f) {
					agent.speed = 0.0f;
					animator.SetInteger ("Mode", 0);
					this.GetComponent<Rigidbody> ().linearVelocity = Vector3.zero;
					this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
					this.GetComponent<Rigidbody> ().isKinematic = true;
					//追跡中でないので追跡時間を常時初期化
					now_tracing_time = target_time;
					//コミュニケーション時間を減じる
					now_stay_time--; 
				} else {
					agent.speed = 0.5f;
					animator.SetInteger ("Mode", 1);
					this.GetComponent<Rigidbody> ().isKinematic = false;
					//追跡中なのでコミュニケーション時間を常時初期化
					if (trace_sub)
						now_stay_time = sub_target_stay [sub_target_ID]; 
				}
				animator.SetInteger ("Emotion", 0);

				//待機児モーションの設定と自動切換え
				if (waitMode_counter <= 0) {
					waitMode_flag = ((int)(Random.Range (0f, 100f))) % 3;
					animator.SetInteger ("Wait_Mode", waitMode_flag);
					waitMode_counter = 200;
				} else {
					waitMode_counter = 0;
				}
			} else {
				//遅延中
//				pos = this.transform.position;
//				agent.SetDestination (pos);
				this.GetComponent<Rigidbody> ().linearVelocity = Vector3.zero;
				this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				this.GetComponent<Rigidbody> ().isKinematic = true;
				//待機時間減少
				wait--;
				//追跡中でないので追跡時間を常時初期化
				now_tracing_time = target_time;
				//コミュニケーション時間を減じる
				now_stay_time--; 
			}
		} else {
			//ターゲットの切り替え
			//サブターゲットチェック
			if (num_target <= 0) {
				trace_main = true;
				return;
			}
			//追跡相手を乱数選択
			int local_rate = Random.Range (0, total_rate);
			checkerInt_1 = local_rate;
			sub_target_ID = 0;
			for (int i = 0; i < num_target; i++) {
				local_rate -= sub_target_rate [i];
				if (local_rate < 0) {
					sub_target_ID = i;
					trace_sub = true;
					now_tracing_time = target_time;
					now_stay_time = sub_target_stay [sub_target_ID]; 
					break;
				}
			}
		}
	}

	public void ReAction(int action_index, int emotion_index){
		//コントローラ等からの接触によるリアクション(action)と感情変化(emotion)
		bool enable_Action = false;
		Vector3 pos;
		//待機中は何もしない
		if (wait > 0)
			return;
		//感情変化(表情を変える)
		if (emotion_index > 0) {
			agent.speed = 0.0f;
			animator.SetInteger ("Emotion", emotion_index);
			wait = 200;
			enable_Action = true;
		}
		//リアクション(動きを変える)
		if (action_index > 0) {
			agent.speed = 0.0f;
			animator.SetInteger ("Mode", action_index);
			wait = 200;
			enable_Action = true;
		}

		if (enable_Action) {
			pos = target.transform.position;
			pos.y = this.transform.position.y;
			this.transform.LookAt (pos);
			trace_main = true;
			trace_sub = false;
		}
	}
}
