using UnityEngine;
using System.Collections;

public class Knife : Entity {
	
	public override void Move(Vector3 position) {
		transform.position = position;
	}
	
	public override void Use() {
		// Cut Nearby Objects
		// If collides with hand != _hand or entity != this
			// Cut Object
			
	}

	public override void Cut(){

	}

}
