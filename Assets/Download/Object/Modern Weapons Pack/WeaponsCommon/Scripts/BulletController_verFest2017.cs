using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController_verFest2017 : MonoBehaviour {
	[Header("弾丸命中時のエフェクト")]
	public GameObject hitEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//当たり判定
	void OnCollisionEnter(Collision other){
		//当たった物体(othrer)の名前を確認
		if (other.gameObject.name == "") {

		}
		//何に当たっていてもエフェクトを残して消滅させる
		Instantiate(hitEffect,this.transform.position,this.transform.rotation);
		Destroy (this.gameObject);
	}
}
