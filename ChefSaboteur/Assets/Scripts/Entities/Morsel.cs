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
