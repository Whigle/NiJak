using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugarery : BuildingObject
{

    public int sugarAmount;
    public int energyCost;

    public Sugarery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Sugarery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.getResource(Resource.Sugar) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.Sugar, sugarAmount);
            }
        }
    }
}
