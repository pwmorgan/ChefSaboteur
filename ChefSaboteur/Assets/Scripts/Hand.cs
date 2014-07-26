using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {
	
	public string horizontalAxis;
	public string verticalAxis;
	public string useButton;
	public string actionButton;

	private GameObject _heldObject;
	private Vector3 _velocity = new Vector3 (0, 0, 0);
	private	bool _isExtending;
	private int speed = 1000;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		_velocity.x += Input.GetAxis (horizontalAxis) * speed * Time.deltaTime;
		_velocity.y += -1 * Input.GetAxis (verticalAxis) * speed * Time.deltaTime;

		transform.position += _velocity * Time.deltaTime;

		_velocity -= _velocity * 1f * Time.deltaTime;

	}
}
