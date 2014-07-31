using UnityEngine;
using System.Collections;

public class Morsel : Entity {
	
	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			return PickUp;
		} else {
			return Drop;
		}
	}

}
