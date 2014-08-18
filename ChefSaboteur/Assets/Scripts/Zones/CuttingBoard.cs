using UnityEngine;
using System.Collections;

public class CuttingBoard : Zone {

    public GameObject _vegPrefab;

    private Vegetable _currentVegetable = null;
    private VegPile _currentVegPile = null;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
		if (_currentVegetable != null) {
			if (_currentVegetable.GetHealth() == 0) {
				_currentVegetable.DestroyEntity();
				_currentVegetable = null;
			}
		}

	}

	
	
	public void OnTriggerExit(Collider other) {
		GameObject gameobj = other.gameObject;
		if (gameobj.GetComponent<VegPile> () != null) {
			if (gameobj == _currentVegPile.gameObject) {
				_currentVegPile = null;
			}
		}
	}

    public void CutVegetable()
    {
        if (_currentVegetable != null)
        {
            if (_currentVegPile == null)
            {
                GameObject gobj = Instantiate(_vegPrefab) as GameObject;
                gobj.transform.parent = transform;
				gobj.transform.position = new Vector3(transform.position.x, transform.position.y - 50, _currentVegetable.transform.position.z);
                gobj.transform.localScale = new Vector3(1, 1, 1);

                _currentVegPile = gobj.GetComponent<VegPile>();
            }

			_currentVegetable.Chop();
			_currentVegPile.AddValue(5);
            GameObject gobjVPiece = Instantiate(_currentVegetable.VegPiece) as GameObject;
            gobjVPiece.GetComponent<SpriteRenderer>().sprite = _currentVegetable.ChildSprite;
            gobjVPiece.transform.parent = _currentVegPile.transform;
            Vector2 randompos = Random.insideUnitCircle;
            gobjVPiece.transform.localPosition = new Vector3(randompos.x * 20, randompos.y * 20, 0);
            gobjVPiece.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void AddVegetable(Vegetable veg)
    {
		// If current vegetable
		if (_currentVegetable != null) {
			//_currentVegetable.Launch();
		}

        _currentVegetable = veg;
		_currentVegetable.transform.localPosition = new Vector3 (transform.position.x, transform.position.y + 75, _currentVegetable.transform.position.z);
    }

}
