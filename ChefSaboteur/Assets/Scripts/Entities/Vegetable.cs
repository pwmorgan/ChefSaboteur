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
		GameObject gobj = Instantiate(VegPiece, transform.position, Quaternion.identity) as GameObject;
		Vector2 randompos = Random.insideUnitCircle;
		gobj.transform.localPosition = new Vector3(randompos.x * 30, -80 + randompos.y * 30, 0);


	}

	private ACTIONRESULT Use() {

		return ACTIONRESULT.CHOP;
	}

}
