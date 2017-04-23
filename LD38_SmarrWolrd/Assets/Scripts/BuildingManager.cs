using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static List<IslandField> buildingFields;

    static BuildingManager()
    {
        buildingFields = new List<IslandField>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public static void AddBuildingField(IslandField bf)
    {
        if (!buildingFields.Contains(bf))
            buildingFields.Add(bf);
    }


    static public List<GameObject> getIslandsOfTypeConnected(Resource resourceType)
    {
        List<GameObject> ret = new List<GameObject>();
        foreach (IslandField island in buildingFields)
        {
            if (island.fieldType == ResourcesManager.ResourceToBuilding(resourceType)) ret.Add(island.gameObject);
        }
        return ret;
    }

}
