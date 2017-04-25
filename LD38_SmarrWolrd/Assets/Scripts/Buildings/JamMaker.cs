using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamMaker : BuildingObject
{
    public int bananasCost;
    public int sugarCost;
    public int foodAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;

    public JamMaker() : base() {}

    void Start ()
    {
        base.Start();
        increaseOverTime += foodAmount;
        productionFrequency = buildingCooldown;
        buildingType = Building.JamMaker;
    }
    

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.hasResource(Resource.Bananas, bananasCost) && ResourcesManager.hasResource(Resource.Sugar, sugarCost) && ResourcesManager.getResource(Resource.Food) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    increaseOverTime += foodAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Bananas, bananasCost);
                ResourcesManager.decreaseResource(Resource.Sugar, sugarCost);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Food, foodAmount);

            }
            else
            {
                increaseOverTime -= foodAmount;
                enabled = false;
            }
        }
        else {
            increaseOverTime -= foodAmount;
            enabled = false;
        }
    }
}
