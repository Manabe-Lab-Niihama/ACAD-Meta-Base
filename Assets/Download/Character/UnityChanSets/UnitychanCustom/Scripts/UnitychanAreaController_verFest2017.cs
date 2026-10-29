using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanAreaController_verFest2017 : MonoBehaviour {
	[Header("キャラクターの本体")]
	public GameObject character;
	public UnityChanController_verFest2017 character_controller;
	[Header("キャラクターリアクションID")]
	public int reaction_ID = 11;
	public int emotion_ID = 1;
	// Use this for initialization
	void Start () {
		character_controller = character.gameObject.GetComponent<UnityChanController_verFest2017> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		//当たった物体のタグチェック
		if (other.gameObject.tag == "Controller") {
			character_controller.ReAction (reaction_ID, emotion_ID);
		}
	}
}
