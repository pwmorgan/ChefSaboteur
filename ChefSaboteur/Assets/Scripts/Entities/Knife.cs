using UnityEngine;
using System.Collections;

public class Knife : Entity {
	
	public override void Move(Vector3 position) {
		transform.position = position;
	}
	
	public override void Use() {
		if (_hand != null) {
			_hand.Release ();
			_hand = null;
		}
	}

	public override void Cut(){

	}

}
