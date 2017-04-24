using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugarery : BuildingObject
{

    public int sugarAmount = 10;

    public Sugarery() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.Sugarery;
    }

    protected override void iterateProduction()
    {
        if (ResourcesManager.getResource(Resource.Sugar) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.increaseResource(Resource.Sugar, sugarAmount);
        }
    }
}
