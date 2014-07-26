using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	public float maxX;
	public float minX;

	public GameObject upperSleeve;
	public GameObject lowerSleeve;

	public string horizontalAxis;
	public string verticalAxis;
	public string useButton;
	public string actionButton;

	private Entity _heldObject = null;
	private Vector3 _velocity = new Vector3 (0, 0, 0);
	private	bool _isExtending;
	private float _controllerThreshold = 0.5f;
	private float _friction = 5f;
	private float speed = 1000f;
	private float _maxVelocity = 3000f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
		Interact ();

	}

	void Move() {

		bool isActive = false;
		
		if (Mathf.Abs (Input.GetAxis (horizontalAxis)) > _controllerThreshold) {
			_velocity.x += Input.GetAxis (horizontalAxis) * speed * Time.deltaTime;

			_velocity.x = Mathf.Max(-1 * _maxVelocity, _velocity.x);
			_velocity.x = Mathf.Min(_maxVelocity, _velocity.x);
			isActive = true;
		}
		
		if (Input.GetAxis (useButton) > _controllerThreshold) {
			Debug.Log ("Extend Arm.");
		}

		if (Mathf.Abs (Input.GetAxis (verticalAxis)) > _controllerThreshold) {
			_velocity.y += -1 * Input.GetAxis (verticalAxis) * speed * Time.deltaTime;
			_velocity.y = Mathf.Max(-1 * _maxVelocity, _velocity.y);
			_velocity.y = Mathf.Min(_maxVelocity, _velocity.y);
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
	}

	void Interact() {

		
		if (_heldObject != null) {
			_heldObject.Move(transform.position);
		}

		if (Input.GetButtonDown (actionButton)) {
			if (_heldObject == null) {
				PickUp();
				Debug.Log ("Pick Up Item");
			} else {
				_heldObject.Use();
				Debug.Log ("Use Item");
			}
		}

	}

	void AdjustToBoundaries() {
		if (transform.position.x < 0) {
			Vector3 newPos = transform.position;
			newPos.x = 0;
			if (_velocity.x < 0){
				_velocity.x = 0;
			}
			transform.position = newPos;
		}
		if (transform.position.x > 1280) {
			Vector3 newPos = transform.position;	
			newPos.x = 1280;
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
		Vector3 lowerPos = lowerSleeve.transform.position;
		lowerPos.y = transform.position.y;
		lowerSleeve.transform.position = lowerPos;

	}

	void PickUp() {

		Entity[] entities = FindObjectsOfType (typeof(Entity)) as Entity[];

		foreach (Entity entity in entities) {
			if (Vector3.Distance(transform.position, entity.transform.position) < 150) {
				_heldObject = entity;
				entity.PickUp(this);
				return;
			}
		}

	}
	
	public void Cut() {
		// Hand takes damage
		// Hand drops blood
	}

}
