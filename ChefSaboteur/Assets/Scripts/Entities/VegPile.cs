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

	public void AddValue(int newvalue) {
		value += newvalue;
	}

	
	protected override ACTIONRESULT Drop()
	{
		foreach (GameObject gobj in _collisionList)
		{
			StockPot pot = gobj.GetComponent<StockPot>();
			if (pot != null)
			{
				pot.AddIngredient(this);
			}
		}
		
		State = ENTITYSTATE.UNHELD;
		return ACTIONRESULT.DROPPED;
	}
	
}
