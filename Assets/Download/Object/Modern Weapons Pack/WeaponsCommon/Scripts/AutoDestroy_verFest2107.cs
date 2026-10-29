using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy_verFest2107 : MonoBehaviour {
	[Header("寿命")]
	public int lifeTime = 1000;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//寿命がなくなったら自動消滅
		lifeTime--;
		if (lifeTime < 0)
			Destroy (this.gameObject);
	}
}
