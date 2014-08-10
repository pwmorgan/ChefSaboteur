using UnityEngine;
using System.Collections;

public class Knife : Entity {
	public Sprite NormalKnife;
	public Sprite PickedUpKnife;

	public int ChopSpeed;


	private int _chopTicker;

	public override ActionMethod GetContext()
	{
		if (State == ENTITYSTATE.UNHELD) {
			OnPickup();
			return PickUp;
		} else {
			foreach(GameObject gobj in _collisionList)
			{

				if(gobj.GetComponent<CuttingBoard>() != null)
				{
                    CurrentZone = gobj;
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
		_chopTicker = ChopSpeed;
		return ACTIONRESULT.CHOP;
	}

	protected override void ChildUpdate() {
		if (_chopTicker >= 0) {
			_chopTicker--;
		}
	}
}
