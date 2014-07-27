using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	public enum ENTITYSTATE {
		HELD,
		UNHELD
	}

	public enum ACTIONRESULT {
		NOTHING,
		PICKEDUP,
		DROPPED
	}

	public delegate ACTIONRESULT ActionMethod();

	protected Hand _hand = null;

	private ENTITYSTATE _state;
	public ENTITYSTATE State {
		get { return _state; }
		protected set { _state = value; }
	}


	// Use this for initialization
	void Start () {
		_state = ENTITYSTATE.UNHELD;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void Move(Vector3 position) {
		position.z = transform.position.z;
		transform.position = position;
	}

	public bool IsFree() {
		return _hand == null;
	}

	public abstract ActionMethod GetContext ();

	protected ACTIONRESULT PickUp() {
		State = ENTITYSTATE.HELD;
		return ACTIONRESULT.PICKEDUP;
	}

	protected ACTIONRESULT Drop() {
		State = ENTITYSTATE.UNHELD;
		return ACTIONRESULT.DROPPED;
	}

}
