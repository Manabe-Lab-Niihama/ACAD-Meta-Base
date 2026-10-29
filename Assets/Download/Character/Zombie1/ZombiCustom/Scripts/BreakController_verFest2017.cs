using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakController_verFest2017 : MonoBehaviour {
	[Header("キャラクタ本体のオブジェクト")]
	public GameObject body;
	public Zombi1Controller_verFest2017 body_controller;
	[Header("パーツの部位(どれか一つ)")]
	public bool head_object = false; 
	public bool rightArm_object = false; 
	public bool rightBody_object = false; 
	public bool leftArm_object = false; 
	public bool leftBody_object = false; 
	[Header("このパーツにリンクして実際に表示されている体の部分")]
	public GameObject part;
	public int part_life;
	[Header("破壊時に生成されるエフェクト")]
	public GameObject effect;
	[Header("被弾時に生成されるエフェクト")]
	public GameObject effect_hit;

	// Use this for initialization
	void Start () {
		if (body != null)
			body_controller = body.GetComponent<Zombi1Controller_verFest2017> ();
		if (head_object)
			part_life = body_controller.Life_head;
		else if (rightArm_object)
			part_life = body_controller.Life_RightArm;
		else if (rightBody_object)
			part_life = body_controller.Life_RightBody;
		else if (leftArm_object)
			part_life = body_controller.Life_LeftArm;
		else if (leftBody_object)
			part_life = body_controller.Life_LeftBody;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//衝突判定
	void OnTriggerEnter(Collider other){
		//当たった物体の名前がBullet(Clone)なら
		if (other.gameObject.name == "Bullet(Clone)") {
			part_life--;
			if (part_life <= 0) {
				Instantiate (effect, this.transform.position, this.transform.rotation);
				part.SetActive (false);
				Destroy (this.gameObject);
			} else {
				Instantiate (effect_hit, this.transform.position, this.transform.rotation);
			}
			if (head_object)
				body_controller.Life_head = part_life;
			else if (rightArm_object)
				body_controller.Life_RightArm = part_life;
			else if (rightBody_object)
				body_controller.Life_RightBody = part_life;
			else if (leftArm_object)
				body_controller.Life_LeftArm = part_life;
			else if (leftBody_object)
				body_controller.Life_LeftBody = part_life;
		}
	}
}
