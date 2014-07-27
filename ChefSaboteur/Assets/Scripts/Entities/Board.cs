using UnityEngine;
using System.Collections;

public class Board : Entity {
	
	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			return PickUp;
		} else {
			return Drop;
		}
	}
		
}
