using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	protected Hand _hand = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PickUp(Hand hand) {
		_hand = hand;
	}

	public abstract void Use ();
	public abstract void Move (Vector3 position);
	public abstract void Cut();

}
