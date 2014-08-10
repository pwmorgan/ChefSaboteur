using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entity : MonoBehaviour {

	public enum ENTITYSTATE {
		HELD,
		UNHELD
	}

	public enum ACTIONRESULT {
		NOTHING,
		PICKEDUP,
		DROPPED,
		CHOP
	}

	public delegate ACTIONRESULT ActionMethod();

	protected Hand _hand = null;
	protected List<GameObject> _collisionList;
	protected SpriteRenderer _spriteRenderer;
	protected BoxCollider _collider;
	protected Zone.ZONETYPE[] _allowedZones;

	private ENTITYSTATE _state;
	public ENTITYSTATE State {
		get { return _state; }
		protected set { _state = value; }
	}

    private GameObject _currentZone = null;
    public GameObject CurrentZone
    {
        get { return _currentZone; }
        protected set { _currentZone = value; }
    }

	// Use this for initialization
	void Start () {
		_state = ENTITYSTATE.UNHELD;	
		_collisionList = new List<GameObject> ();
			
		_spriteRenderer = transform.GetComponent<SpriteRenderer> () as SpriteRenderer;
		_collider = transform.GetComponent<BoxCollider> () as BoxCollider;
	}
	
	// Update is called once per frame
	void Update () {
		ChildUpdate ();
	}

	protected virtual void ChildUpdate() {
		//
	}
	
	void OnTriggerExit(Collider other) {
		Debug.Log ("COLL Exit: " +  other.gameObject.ToString());
		_collisionList.Remove (other.gameObject);
	}
	void OnTriggerEnter(Collider other) {
		Debug.Log ("COLL Enter: " + other.gameObject.ToString());
		GameObject gameobj = other.gameObject;

		if (gameobj.GetComponent<Zone> () != null) {
			bool isUnique = true;
			foreach (GameObject gobj in _collisionList) {
				if (gobj == gameobj) {
					isUnique = false;
					break;
				}
			}
			if (isUnique) {
				_collisionList.Add (gameobj);
			}
		}
	}


	public virtual void Move(Vector3 position) {
		position.x -= _collider.center.x;
		position.y -= _collider.center.y;
		position.z = transform.position.z;
		transform.position = position;
	}

	public abstract ActionMethod GetContext ();

	protected virtual ACTIONRESULT PickUp() {
		State = ENTITYSTATE.HELD;
		return ACTIONRESULT.PICKEDUP;
	}

	protected virtual ACTIONRESULT Drop() {
		State = ENTITYSTATE.UNHELD;
		return ACTIONRESULT.DROPPED;
	}

}
