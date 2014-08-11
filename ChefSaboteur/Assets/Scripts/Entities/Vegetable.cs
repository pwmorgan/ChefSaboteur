using UnityEngine;
using System.Collections;

public class Vegetable : Entity {
	public Sprite ChildSprite;
	public GameObject VegPiece;

	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			return PickUp;
		} else {
			return Drop;
		}
	}


	public void ChildUpdate() {

	}


	public void Chop() {

		//Create Pieces
		/*
        GameObject gobj = Instantiate(VegPiece, transform.position, Quaternion.identity) as GameObject;
		Vector2 randompos = Random.insideUnitCircle;
		gobj.transform.localPosition = new Vector3(randompos.x * 30, -80 + randompos.y * 30, 0);
        */

        /*GameObject gobj = Instantiate(VegPiece) as GameObject;
		gobj.GetComponent<SpriteRenderer> ().sprite = ChildSprite;
		gobj.transform.parent = transform;
		Vector2 randompos = Random.insideUnitCircle;
		gobj.transform.localPosition = new Vector3(randompos.x * 30, -80 + randompos.y * 100, 0);
		gobj.transform.localScale = new Vector3(1, 1, 1);*/

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
