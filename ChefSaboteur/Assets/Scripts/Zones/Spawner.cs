using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] Items;

	private const float SPAWNTIMERMAX = 4f;
	private float _spawntimer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_spawntimer -= Time.deltaTime;

		if (_spawntimer < 0f) {
			int i = Mathf.FloorToInt(Random.Range(0, Items.Length));
			Instantiate(Items[i], transform.position, transform.rotation);
			_spawntimer = SPAWNTIMERMAX;
		}
	}
}
