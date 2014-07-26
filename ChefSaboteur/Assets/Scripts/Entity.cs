using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	protected enum ENTITYSTATE {
		HELD,
		UNHELD
	}

	protected Hand _hand = null;
	protected ENTITYSTATE state = ENTITYSTATE.UNHELD;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AttemptAction()
	{

	}
	
	public void PickUp(Hand hand) {
		_hand = hand;
	}

	public abstract void Use ();
	public abstract void Move (Vector3 position);
	public abstract void Cut();

	public virtual void ActionHeld(string context)
	{

	}

	public virtual void ActionUnheld(string context)
	{

	}
	//Pickup
	//In Hand
	//Not in Hand
	//Context



}
