using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

	public float maxX;
	public float minX;

	public GameObject finger;
	public GameObject bandage;
	public GameObject upperSleeve;

	public Sprite[] openHand;
	public Sprite[] closedHand;

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
	private int _damage = 0;
	private float _damageCooldown = 0f;
	private const float DAMAGETIMER = 1f;

	private List<GameObject> _collisionList;
	private List<GameObject> _zoneList;


	// Use this for initialization
	void Start () {

		_spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer> () as SpriteRenderer;
		_collisionList = new List<GameObject> ();
		_zoneList = new List<GameObject> ();
	
	}
	

	// Update is called once per frame
	void Update () {

		Move ();
		Entity targetobj;
		Entity.ActionMethod action = GetContext (out targetobj);
		Interact (action, targetobj);
		_damageCooldown -= Time.deltaTime;

	}


	void Move() {

		bool isActive = false;

		//Moves hand left and right
		if (Mathf.Abs (Input.GetAxis (horizontalAxis)) > _controllerThreshold) {
			_velocity.x = Input.GetAxis (horizontalAxis) * speed;

			isActive = true;
		}


		//moves hand up and down
		if (Mathf.Abs (Input.GetAxis (verticalAxis)) > _controllerThreshold) {
			_velocity.y = -1 * Input.GetAxis (verticalAxis) * speed;
			isActive = true;
		}
		
		transform.position += _velocity * Time.deltaTime;
		
		if (!isActive) {
			_velocity -= _velocity * _friction * Time.deltaTime;
		}

		AdjustToBoundaries ();
		MoveSleeves ();

	}


	public void OnTriggerExit(Collider other) {
		GameObject gameobj = other.gameObject;
		if (gameobj.GetComponent<Entity> () != null) {
			_collisionList.Remove (other.gameObject);
		}

		if (gameobj.GetComponent<Zone> () != null) {
			_zoneList.Remove(other.gameObject);
		}
	}


	void OnTriggerEnter(Collider other) {
		GameObject gameobj = other.gameObject;
		if (gameobj.GetComponent<Entity> () != null) {
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


	Entity.ActionMethod  GetContext(out Entity targetobj) { 
		if (HasItem()) {
			targetobj = _heldObject;
			return _heldObject.GetContext();
		}

		if(_collisionList.Count > 0)
		{
			Entity currentent =  _collisionList[0].GetComponent<Entity>() as Entity;
			targetobj = currentent;
			return currentent.GetContext();
		}
		
		targetobj = null;
		return null;
	}


	void Interact(Entity.ActionMethod actionmethod, Entity targetobj) {
		if (Input.GetAxis (useButton) > 0.5f) {
			if (!_actionButtonActive) {
				_actionButtonActive = true;
				Debug.Log("ACTION BUTTON!: ");
				if(actionmethod != null) { 
					Debug.Log ("ACTION METHOD");

					Entity.ACTIONRESULT result = actionmethod(); 
					switch(result)
					{
					    case Entity.ACTIONRESULT.PICKEDUP :
						    _spriteRenderer.sprite = closedHand[_damage];
							_heldObject = targetobj;
							Vector3 pos = _heldObject.transform.position;
							pos.z += -50;
							_heldObject.transform.position = pos;
						    break;
					    case Entity.ACTIONRESULT.DROPPED :
							_spriteRenderer.sprite = openHand[_damage];
							Vector3 pos2 = _heldObject.transform.position;
							pos2.z += 50;
							_heldObject.transform.position = pos2;
						    _heldObject = null;
						    break;
					    case Entity.ACTIONRESULT.CHOP :
						    //Play Use animation
                            GameObject zone = targetobj.CurrentZone;
                            CuttingBoard board = zone.GetComponent<CuttingBoard>();
                            if (board != null)
                            {
                                board.CutVegetable();
                            }

							// Get all hands 		
							Hand[] hands = Resources.FindObjectsOfTypeAll (typeof(Hand)) as Hand[];
							Vector3 knifePosition = targetobj.transform.GetChild(0).transform.position;
							knifePosition.z = 0;
							foreach (Hand hand in hands) {
								Vector3 handPosition = hand.transform.position;
								handPosition.z = 0;
								if (Vector3.Distance(handPosition, knifePosition) < 75) {
									hand.Cut();		
								}
							}
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


	private bool HasItem() {
		if (_heldObject != null) {
			return true;
		} 
		return false;
	}


	public void Cut() {

		if (_damageCooldown > 0) {
			return;		
		}

		_damage++;
		_damageCooldown = DAMAGETIMER;

		Vector3 position = transform.position;
		position.z = 0;

		if (_damage > 3) {
			Instantiate (bandage, transform.position, transform.rotation);
			_damage = 3;
			return;
		}

		if (_heldObject != null) {
			_spriteRenderer.sprite = closedHand [_damage];
		} else {
			_spriteRenderer.sprite = openHand [_damage];
		}

		Instantiate (finger, transform.position, transform.rotation);
		
	}
	
}
