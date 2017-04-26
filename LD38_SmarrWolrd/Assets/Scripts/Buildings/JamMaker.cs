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
    static public int enabledStructures = 0;
    static public int totalStructures = 0;

    public JamMaker() : base() {}

    void Start ()
    {
        base.Start();
        totalStructures++;
        enabledStructures++;
        increaseOverTime += foodAmount;
        bananasGlobalCost += (bananasCost/buildingCooldown);
        sugarGlobalCost += (sugarCost / buildingCooldown);
        PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
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
                    enabledStructures++;
                    bananasGlobalCost += (bananasCost / buildingCooldown);
                    sugarGlobalCost += (sugarCost / buildingCooldown);
                    PowerTower.energyConsumptionOverTime += (energyCost / frequency.TotalSeconds);
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
                if (enabled)
                {
                    enabledStructures--;
                    bananasGlobalCost -= (bananasCost / buildingCooldown);
                    sugarGlobalCost -= (sugarCost / buildingCooldown);
                    PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                    increaseOverTime -= foodAmount;
                    enabled = false;
                }
            }
        }
        else
        {
            if (enabled)
            {
                enabledStructures--;
                bananasGlobalCost -= (bananasCost / buildingCooldown);
                sugarGlobalCost -= (sugarCost / buildingCooldown);
                PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
                increaseOverTime -= foodAmount;
                enabled = false;
            }
        }
    }

    ~JamMaker()
    {
        if (enabled)
        {
            enabledStructures--;
            bananasGlobalCost -= (bananasCost / buildingCooldown);
            sugarGlobalCost -= (sugarCost / buildingCooldown);
            PowerTower.energyConsumptionOverTime -= (energyCost / frequency.TotalSeconds);
            increaseOverTime -= foodAmount;
            enabled = false;
        }
        totalStructures--;
    }
}
