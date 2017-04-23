using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static List<IslandField> buildingFields;

    static BuildingManager ()
    {
        buildingFields = new List<IslandField> ();
    }

    void Start ()
    {

    }

    void Update ()
    {

    }

    public static void AddBuildingField (IslandField bf)
    {
        if (!buildingFields.Contains (bf))
            buildingFields.Add (bf);
    }

}
