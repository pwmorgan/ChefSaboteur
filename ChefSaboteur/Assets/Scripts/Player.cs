using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Hand tophand;
	public Hand bottomhand;

	private float _dishQuality;
	private float _dishProgress;
	private int _phase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (tophand.transform.position.y < bottomhand.transform.position.y + 50) {
			Vector3 newPosition = tophand.transform.position;
			newPosition.y = bottomhand.transform.position.y + 50;
			tophand.transform.position = newPosition;
		}
		if (bottomhand.transform.position.y > tophand.transform.position.y - 50) {
			Vector3 newPosition = bottomhand.transform.position;
			newPosition.y = tophand.transform.position.y - 50;
			bottomhand.transform.position = newPosition;
		}

	}
}
