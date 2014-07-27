using UnityEngine;
using System.Collections;

public class Vegetable : Entity {
	private string _state = "held";

	public override void Move(Vector3 position) {
		position.z = transform.position.z;
		transform.position = position;
	}

	public override ActionMethod GetContext()
	{
		return Use;
	}

	private void Use() {
		if (_hand != null) {
			_hand.Release ();
			_hand = null;
		}
	}
	
}
