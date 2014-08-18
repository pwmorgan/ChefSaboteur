using UnityEngine;
using System.Collections;

public class Vegetable : Entity {
	public Sprite ChildSprite;
	public GameObject VegPiece;

	private Vector3 _velocity = Vector3.zero;

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

	protected override void ChildUpdate() {
		transform.position = transform.position + _velocity * Time.deltaTime;
	}

	public void Launch(int multiplier) {
		_velocity = new Vector3 ((Random.value + 1 * 500) * multiplier, (Random.value + 1 * 500) * multiplier, 0);
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
				break;
            }
			
			StockPot pot = gobj.GetComponent<StockPot>();
			if (pot != null)
			{
				pot.AddIngredient(this);
				break;
			}
        }

        State = ENTITYSTATE.UNHELD;
        return ACTIONRESULT.DROPPED;
    }

}
