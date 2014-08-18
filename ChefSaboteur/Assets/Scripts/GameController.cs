using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public float GameTimer = 60;
	public GUIStyle Style;

	public RecipeManager Player1;
	public RecipeManager Player2;

	private bool _gameover = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameTimer -= Time.deltaTime;

		if (GameTimer <= 0) {
			GameTimer = 0;
			Time.timeScale = 0;
			_gameover = true;
		}
	}

	void OnGUI() {
		string timeleft = Mathf.RoundToInt (GameTimer).ToString ();
		GUI.Label (new Rect (550, 15, 80, 20), timeleft, Style);

		if (_gameover) {

			float _p1 = Player1.GetScore();
			float _p2 = Player2.GetScore();
			if (_p1 > _p2) {
				GUI.Label (new Rect (300, 200, 400, 20), "Player 1 Wins!", Style);
			} else if (_p2 > _p1) {
				GUI.Label (new Rect (300, 200, 400, 20), "Player 2 Wins!", Style);
			} else {
				GUI.Label (new Rect (300, 200, 400, 20), "Tie!", Style);
			}
		}
	}
}
