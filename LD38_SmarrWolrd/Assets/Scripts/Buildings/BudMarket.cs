using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMarket : BuildingObject
{
    public int woodCost;
    public int stoneCost;
    public int buildingMaterialAmount;
    public int energyCost;
    public BudMarket() : base() { }
    void Start()
    {
        base.Start();
        buildingType = Building.BudMarket;
    }


    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.hasResource(Resource.Wood, woodCost) && ResourcesManager.hasResource(Resource.Stone, stoneCost) && ResourcesManager.getResource(Resource.BuildingMaterial) < ResourcesManager.resourcesCapacity)
            {
                ResourcesManager.decreaseResource(Resource.Wood, woodCost);
                ResourcesManager.decreaseResource(Resource.Stone, stoneCost);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.BuildingMaterial, buildingMaterialAmount);
            }
        }
    }
}
