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
	    
	}

    public void CutVegetable()
    {
        if (_currentVegetable != null)
        {
            if (_currentVegPile == null)
            {
                GameObject gobj = Instantiate(_vegPrefab) as GameObject;
                gobj.transform.parent = transform;
                gobj.transform.localPosition = new Vector3(10, 10, 0);
                gobj.transform.localScale = new Vector3(1, 1, 1);

                _currentVegPile = gobj.GetComponent<VegPile>();
            }


            GameObject gobjVPiece = Instantiate(_currentVegetable.VegPiece) as GameObject;
            gobjVPiece.GetComponent<SpriteRenderer>().sprite = _currentVegetable.ChildSprite;
            gobjVPiece.transform.parent = _currentVegPile.transform;
            Vector2 randompos = Random.insideUnitCircle;
            gobjVPiece.transform.localPosition = new Vector3(randompos.x * 30, -80 + randompos.y * 100, 0);
            gobjVPiece.transform.localScale = new Vector3(1, 1, 1);
            //_currentVegetable
        }
    }

    public void AddVegetable(Vegetable veg)
    {
        _currentVegetable = veg;
    }

}
