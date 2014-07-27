using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	public float maxX;
	public float minX;

	public GameObject upperSleeve;

	public Sprite openHand;
	public Sprite closedHand;

	public string horizontalAxis;
	public string verticalAxis;
	public string useButton;
	public string actionButton;

	private int _graspRange = 150;
	private bool _actionButtonActive = false;
	private SpriteRenderer _spriteRenderer;
	private Entity _heldObject = null;
	private Vector3 _velocity = new Vector3 (0, 0, 0);
	private	bool _isExtending;
	private float _controllerThreshold = 0.5f;
	private float _friction = 5f;
	private float speed = 750f;
	private float _maxVelocity = 500f;
	private int _health = 3;


	// Use this for initialization
	void Start () {
		_spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer> () as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
		Entity targetobj;
		Entity.ActionMethod action = GetContext (out targetobj);
		Interact (action, targetobj);

	}

	void Move() {

		bool isActive = false;

		//Moves hand left and right
		if (Mathf.Abs (Input.GetAxis (horizontalAxis)) > _controllerThreshold) {
			_velocity.x = Input.GetAxis (horizontalAxis) * speed;

//			_velocity.x = Mathf.Max(-1 * _maxVelocity, _velocity.x);
//			_velocity.x = Mathf.Min(_maxVelocity, _velocity.x);
			isActive = true;
		}

//		if (Input.GetAxis (useButton) > _controllerThreshold) {
//			Debug.Log ("Extend Arm.");
//		}

		//moves hand up and down
		if (Mathf.Abs (Input.GetAxis (verticalAxis)) > _controllerThreshold) {
			_velocity.y = -1 * Input.GetAxis (verticalAxis) * speed;
//			_velocity.y = Mathf.Max(-1 * _maxVelocity, _velocity.y);
//			_velocity.y = Mathf.Min(_maxVelocity, _velocity.y);
			isActive = true;
		}
		
		transform.position += _velocity * Time.deltaTime;
		
		if (!isActive) {
			_velocity -= _velocity * _friction * Time.deltaTime;
		}

		AdjustToBoundaries ();
		MoveSleeves ();

	}

	public void Release() {
		_heldObject = null;
		_spriteRenderer.sprite = openHand;
	}

	Entity.ActionMethod  GetContext(out Entity targetobj) { 
		if (HasItem()) {
			targetobj = _heldObject;
			return _heldObject.GetContext();
		}

		Entity[] entities = FindObjectsOfType (typeof(Entity)) as Entity[];
		
		foreach (Entity entity in entities) {
			if (Vector3.Distance(transform.position, entity.transform.position) < _graspRange ){ 
				targetobj = entity;
				return entity.GetContext();
			}
		}
		targetobj = null;
		return null;
	}

	void Interact(Entity.ActionMethod actionmethod, Entity targetobj) {
		if (Input.GetAxis (useButton) > 0.5f) {
			if (!_actionButtonActive) {
				_actionButtonActive = true;
				Debug.Log("ACTION BUTTON!");
				if(actionmethod != null) { 
					Debug.Log ("ACTION METHOD");
					Entity.ACTIONRESULT result = actionmethod(); 
					switch(result)
					{
						case Entity.ACTIONRESULT.PICKEDUP :
							_heldObject = targetobj;
							break;
						case Entity.ACTIONRESULT.DROPPED :
							_heldObject = null;
							break;
					}
				}
			}
		} else {
			_actionButtonActive = false;
		}

		if (HasItem ()) {
			_heldObject.Move(transform.position);
		}
			
	}

	void AdjustToBoundaries() {
		if (transform.position.x < minX) {
			Vector3 newPos = transform.position;
			newPos.x = minX;
			if (_velocity.x < 0){
				_velocity.x = 0;
			}
			transform.position = newPos;
		}
		if (transform.position.x > maxX) {
			Vector3 newPos = transform.position;	
			newPos.x = maxX;
			if (_velocity.x > 0){
				_velocity.x = 0;
			}
			transform.position = newPos;	
		}
		if (transform.position.y < 0) {
			Vector3 newPos = transform.position;
			newPos.y = 0;
			if (_velocity.y < 0){
				_velocity.y = 0;
			}
			transform.position = newPos;
		}
		if (transform.position.y > 720) {
			Vector3 newPos = transform.position;	
			newPos.y = 720;
			if (_velocity.y > 0){
				_velocity.y = 0;
			}
			transform.position = newPos;	
		}
	}

	void MoveSleeves() {
		Vector3 upperPos = upperSleeve.transform.position;
		upperPos.y = transform.position.y;
		upperSleeve.transform.position = upperPos;
	}

	/*
	void PickUp() {

		Entity[] entities = FindObjectsOfType (typeof(Entity)) as Entity[];

		foreach (Entity entity in entities) {
			if (Vector3.Distance(transform.position, entity.transform.position) < 150 && entity.IsFree()) {
				_heldObject = entity;
				_spriteRenderer.sprite = closedHand;
				//entity.PickUp(this);

				return;
			}
		}

	}*/

	private bool HasItem() {
		if (_heldObject != null) {
			return true;
		} 
		return false;
	}

	public void Cut() {
		// Hand takes damage
		// Hand drops blood
	}

}
