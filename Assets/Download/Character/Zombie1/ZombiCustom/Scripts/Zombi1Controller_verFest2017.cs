using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombi1Controller_verFest2017 : MonoBehaviour {
	[Header("ターゲット")]
	public GameObject target;
	NavMeshAgent agent;
	Animator animator;
	[Header("パラメータ")]
	public int Life_head = 1;
	public int Life_RightArm = 1;
	public int Life_RightBody = 3;
	public int Life_LeftArm = 1;
	public int Life_LeftBody = 3;
	[Header("破壊時に生成されるエフェクト")]
	public GameObject effect;

	int wait;
	// Use this for initialization
	void Start () {
		if (target == null) {
			target = GameObject.FindWithTag ("MainCamera");
		}
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		wait = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;
		if (Life_head > 0) {
			agent.SetDestination (target.transform.position);
			float distance = agent.remainingDistance;
			if (distance < 1.2f) {
				agent.speed = 0.0f;
				animator.SetBool ("Attack", true);
			} else {
				agent.speed = 0.3f;
				animator.SetBool ("Attack", false);
			}
		} else {
			pos = this.transform.position + this.transform.forward;
			agent.SetDestination (pos);
			agent.speed = 0.2f;
			animator.SetBool ("Attack", false);
		}

		if (Life_RightBody <= 0 && Life_LeftBody <= 0) {
			wait ++;
			if (wait > 200) {
				Instantiate (effect, this.transform.position, this.transform.rotation);
				Destroy (this.gameObject);
			} else if (wait >= 50) {
				animator.SetBool ("Dead", true);
			}
		}
	}
}
