using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	protected enum ENTITYSTATE {
		HELD,
		UNHELD
	}
	
	public delegate void ActionMethod();

	protected Hand _hand = null;
	protected ENTITYSTATE state = ENTITYSTATE.UNHELD;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PickUp(Hand hand) {
		_hand = hand;
	}

	public bool IsFree() {
		return _hand == null;
	}

	public abstract void Move(Vector3 position);
	public abstract ActionMethod GetContext ();

	protected virtual void ActionHeld(string context)
	{

	}

	protected virtual void ActionUnheld(string context)
	{

	}

}
