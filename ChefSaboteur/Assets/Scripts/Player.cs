using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Hand lefthand;
	public Hand righthand;

	public string leftVerticalAxis;
	public string leftHorizontalAxis;
	public string rightVerticalAxis;
	public string rightHorizontalAxis;
	public string leftHandButton;
	public string rightHandButton;
	public string actionButton;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis (leftVerticalAxis) != 0) {
			Debug.Log ("Detected Left Hand move);
		}
	}
}
