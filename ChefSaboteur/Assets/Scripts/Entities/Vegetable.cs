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

	public void Chop() {


		//Create Pieces
		GameObject gobj = Instantiate(VegPiece) as GameObject;
		gobj.GetComponent<SpriteRenderer> ().sprite = ChildSprite;
		gobj.transform.parent = transform;
		Vector2 randompos = Random.insideUnitCircle;
		gobj.transform.localPosition = new Vector3(randompos.x * 30, -80 + randompos.y * 100, 0);
		gobj.transform.localScale = new Vector3(1, 1, 1);

	}

	private ACTIONRESULT Use() {

		return ACTIONRESULT.CHOP;
	}

}
