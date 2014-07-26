using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	private Hand _hand = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PickUp(Hand hand) {
		_hand = hand;
	}

	public void Move(Vector3 position) {
		transform.position = position;
	}

	public void Use() {
		if (_hand != null) {
			_hand.Release ();
			_hand = null;
		}
	}

}
