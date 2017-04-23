using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMarket : BuildingObject
{
    public int woodCost = 10;
    public int stoneCost = 10;
    public int buildingMaterialAmount = 10;
    public BudMarket() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.BudMarket;
    }


    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Wood, woodCost) && ResourcesManager.hasResource(Resource.Stone, stoneCost) && ResourcesManager.getResource(Resource.BuildingMaterial) < ResourcesManager.resourcesCapacity)
        {
            ResourcesManager.decreaseResource(Resource.Wood, woodCost);
            ResourcesManager.decreaseResource(Resource.Stone, stoneCost);
            ResourcesManager.increaseResource(Resource.BuildingMaterial, buildingMaterialAmount);
        }
    }
}
