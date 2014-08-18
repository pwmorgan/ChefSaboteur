using UnityEngine;
using System.Collections;

public class Vegetable : Entity {
	public Sprite ChildSprite;
	public GameObject VegPiece;

	private int _health = 5;

	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			return PickUp;
		} else {
			return Drop;
		}
	}

	public int GetHealth() {
		return _health;
	}

	public void ChildUpdate() {

	}


	public void Chop() {

		_health--;

	}

	private ACTIONRESULT Use() {

		return ACTIONRESULT.CHOP;
	}

    protected override ACTIONRESULT PickUp()
    {
        CurrentZone = null;
        State = ENTITYSTATE.HELD;
        return ACTIONRESULT.PICKEDUP;
    }

    protected override ACTIONRESULT Drop()
    {
        foreach (GameObject gobj in _collisionList)
        {
            CuttingBoard board = gobj.GetComponent<CuttingBoard>();
            if (board != null)
            {
                board.AddVegetable(this);
                CurrentZone = gobj;
            }
        }

        State = ENTITYSTATE.UNHELD;
        return ACTIONRESULT.DROPPED;
    }

}
