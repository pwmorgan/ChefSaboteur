using UnityEngine;
using System.Collections;

public class RecipeManager : MonoBehaviour {
	
	public GameObject indicator;
	
	private float _score = 150;
	
	private float _minScore = 0;
	private float _maxScore = 300;
	private float _minY = 100;
	private float _maxY = 620;


	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {

		// Move tomato position based on score;
		Vector3 position = indicator.transform.position;
		position.y = (_score / _maxScore) * (_maxY - _minY) + _minY;
		indicator.transform.position = position;

	}


	public void AddScore(float points) {

		_score += points;

		if (_score > _maxScore) {
			_score = _maxScore;		
		} else if (_score < _minScore) {
			_score = _minScore;	
		}
	
	}

	public float GetScore() {
		return _score;
	}

}
