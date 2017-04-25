using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : BuildingObject {

    public int foodPerMinionCost = 1;
    int foodCost;
    public GameObject minion;
    MainGame mainGame;

    // Use this for initialization
    void Start () {
        base.Start();
        mainGame=GameObject.Find("MainGame").GetComponent<MainGame>();
        buildingType = Building.TownCenter;
    }

    protected override void iterateProduction()
    {
        foodCost = foodPerMinionCost * minionScript.population;
        if (ResourcesManager.hasResource(Resource.Food, foodCost))
        {
            minionScript.die = false;
            ResourcesManager.decreaseResource(Resource.Food, foodCost);
            if (minionScript.population < minionScript.maxPop) {
                Instantiate(minion, transform.position+ new Vector3(0f,0f,-0.25f), Quaternion.identity).GetComponent<minionScript>().grid = mainGame.grid;
            }
        }
        else
        {
            minionScript.die = true;
        }
    }
}
