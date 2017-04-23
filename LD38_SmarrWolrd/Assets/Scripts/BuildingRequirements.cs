using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingRequirements : MonoBehaviour
{
    public Resources requiredResources;
    public Resource islandTypeNeeded;

    void Start ()
    {
        //requiredResources = new Resources();
    }

    void Update ()
    {

    }

    public bool CanBuild ()
    {
        if (ResourcesManager.getResource (Resource.Food) < requiredResources.Food)
        {
            return false;
        }
        else if (ResourcesManager.getResource (Resource.Bananas) < requiredResources.Bananas)
        {
            return false;
        }
        else if (ResourcesManager.getResource (Resource.BuildingMaterial) < requiredResources.BuildingMaterial)
        {
            return false;
        }
        else if (ResourcesManager.getResource (Resource.Stone) < requiredResources.Stone)
        {
            return false;
        }
        else if (ResourcesManager.getResource (Resource.Sugar) < requiredResources.Sugar)
        {
            return false;
        }
        else if (ResourcesManager.getResource (Resource.Wood) < requiredResources.Wood)
        {
            return false;
        }


        return true;
    }


}
