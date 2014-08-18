using UnityEngine;
using System.Collections;

public class StockPot : Zone {

	public RecipeManager recipeManager;


	// Use this for initialization
	void Start () {
	
	}


	public void AddIngredient(Entity ingredient) {

		recipeManager.AddScore(ingredient.value);
		ingredient.DestroyEntity();

	}


	// Update is called once per frame
	void Update () {
	
	}

}
