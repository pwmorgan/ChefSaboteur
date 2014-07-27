using UnityEngine;
using System.Collections;

public class Vegetable : Entity {
	
	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			return PickUp;
		} else {
			return Drop;
		}
	}

	private ACTIONRESULT Use() {
		return ACTIONRESULT.NOTHING;
	}
	
}
