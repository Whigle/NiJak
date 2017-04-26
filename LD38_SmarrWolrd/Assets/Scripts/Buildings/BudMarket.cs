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
    static public int enabledStructures = 0;
    static public int totalStructures = 0;
    public BudMarket() : base() {}
    void Start()
    {
        base.Start();
        totalStructures++;
        enabledStructures++;
        increaseOverTime += buildingMaterialAmount;
        woodGlobalCost += (woodCost / buildingCooldown);
        stoneGlobalCost += (stoneCost / buildingCooldown);
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
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
                    enabledStructures++;
                    woodGlobalCost += (woodCost / buildingCooldown);
                    stoneGlobalCost += (stoneCost / buildingCooldown);
                    PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
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
                if (enabled)
                {
                    enabledStructures--;
                    woodGlobalCost -= (woodCost / buildingCooldown);
                    stoneGlobalCost -= (stoneCost / buildingCooldown);
                    increaseOverTime -= buildingMaterialAmount;
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                enabledStructures--;
                woodGlobalCost -= (woodCost / buildingCooldown);
                stoneGlobalCost -= (stoneCost / buildingCooldown);
                increaseOverTime -= buildingMaterialAmount;
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                enabled = false;
            }
        }
    }
    ~BudMarket()
    {
        if (enabled)
        {
            enabledStructures--;
            woodGlobalCost -= (woodCost / buildingCooldown);
            stoneGlobalCost -= (stoneCost / buildingCooldown);
            increaseOverTime -= buildingMaterialAmount;
            PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
            enabled = false;
        }
        totalStructures--;
    }
}
