using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resources
{
    public int Food;
    public int BuildingMaterial;
    public int Bananas;
    public int Sugar;
    public int Wood;
    public int Stone;
};

public class BuildingRequirements : MonoBehaviour
{
    public Resources requiredResources;


    void Start ()
    {
        //requiredResources = new Resources();
    }

    void Update ()
    {

    }

    public bool CanBuild ()
    {
        if (ResourcesManger.getResource (Resource.Food) < requiredResources.Food)
        {
            return false;
        }
        else if (ResourcesManger.getResource (Resource.Bananas) < requiredResources.Bananas)
        {
            return false;
        }
        else if (ResourcesManger.getResource (Resource.BuildingMaterial) < requiredResources.BuildingMaterial)
        {
            return false;
        }
        else if (ResourcesManger.getResource (Resource.Stone) < requiredResources.Stone)
        {
            return false;
        }
        else if (ResourcesManger.getResource (Resource.Sugar) < requiredResources.Sugar)
        {
            return false;
        }
        else if (ResourcesManger.getResource (Resource.Wood) < requiredResources.Wood)
        {
            return false;
        }


        return true;
    }


}
