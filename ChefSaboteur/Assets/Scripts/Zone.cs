using UnityEngine;
using System.Collections;

public abstract class Zone : MonoBehaviour {

	public enum ZONETYPE {
		KNIFE,
		CUTTINGBOARD,
		POT,
		CONVEYOR
	}

	protected ZONETYPE _zonetype;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
