using UnityEngine;
using System.Collections;

public class Knife : Entity {
	public Sprite NormalKnife;
	public Sprite PickedUpKnife;

	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			OnPickup();
			return PickUp;
		} else {
			foreach(GameObject gobj in _collisionList)
			{
				if(gobj.GetComponent<Board>() != null)
				{
					return Use;
				}
			}
			OnDrop ();
			return Drop;
		}
	}

	private void OnPickup() {
		_spriteRenderer.sprite = NormalKnife;
	}

	private void OnDrop() {
		_spriteRenderer.sprite = PickedUpKnife;
	}

	private ACTIONRESULT Use() {
		return ACTIONRESULT.NOTHING;
	}

}
