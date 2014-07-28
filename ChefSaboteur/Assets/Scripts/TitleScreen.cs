using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	private bool _isStarting = false;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = transform.GetComponent<Animator> () as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_isStarting) {
			if (Input.GetButtonDown("joystick button 16")) {
				//_animator.Play();
			}
		}
	}
}
