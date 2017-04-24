using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamMaker : BuildingObject
{
    public int bananasCost;
    public int sugarCost;
    public int foodAmount;
    public int energyCost;

    public JamMaker() : base(){}

    void Start ()
    {
        base.Start();
        buildingType = Building.JamMaker;
    }
    

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.hasResource(Resource.Bananas, bananasCost) && ResourcesManager.hasResource(Resource.Sugar, sugarCost) && ResourcesManager.getResource(Resource.Food) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.decreaseResource(Resource.Bananas, bananasCost);
                ResourcesManager.decreaseResource(Resource.Sugar, sugarCost);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Food, foodAmount);
            }
        }
    }
}
