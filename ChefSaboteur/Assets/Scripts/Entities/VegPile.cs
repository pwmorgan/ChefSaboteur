using UnityEngine;
using System.Collections;

public class VegPile : Entity {

	public override ActionMethod GetContext()
	{
        if (State == ENTITYSTATE.UNHELD)
        {
            return PickUp;
        }
        return Drop;
	}

	private void OnPickup() {

	}

	private void OnDrop() {

	}

    
}
