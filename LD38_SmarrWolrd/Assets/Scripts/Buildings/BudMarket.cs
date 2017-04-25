using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudMarket : BuildingObject
{
    public int woodCost;
    public int stoneCost;
    public int buildingMaterialAmount;
    public int energyCost;
    static public int increaseOverTime = 0;
    static public double productionFrequency;
    public BudMarket() : base() {}
    void Start()
    {
        base.Start();
        increaseOverTime += buildingMaterialAmount;
        productionFrequency = buildingCooldown;
        buildingType = Building.BudMarket;
    }


    protected override void iterateProduction()
    {
        if (ResourcesManager.hasResource(Resource.Energy, energyCost))
        {
            if (ResourcesManager.hasResource(Resource.Wood, woodCost) && ResourcesManager.hasResource(Resource.Stone, stoneCost) && ResourcesManager.getResource(Resource.BuildingMaterial) < ResourcesManager.resourcesCapacity)
            {
                if (!enabled)
                {
                    increaseOverTime += buildingMaterialAmount;
                    enabled = true;
                }
                ResourcesManager.decreaseResource(Resource.Wood, woodCost);
                ResourcesManager.decreaseResource(Resource.Stone, stoneCost);
                ResourcesManager.decreaseResource(Resource.Energy, energyCost);
                ResourcesManager.increaseResource(Resource.BuildingMaterial, buildingMaterialAmount);
            }
            else
            {
                increaseOverTime -= buildingMaterialAmount;
                enabled = false;
            }
        }
        else
        {
            increaseOverTime -= buildingMaterialAmount;
            enabled = false;
        }
    }
}
