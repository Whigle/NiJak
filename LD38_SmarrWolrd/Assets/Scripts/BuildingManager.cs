using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static List<IslandField> buildingFields;
    static public GameObject selectedCube;
    Ray ray;
    RaycastHit hit;
    public static Resources reqResources;
    public GameObject townCenter;

    static BuildingManager()
    {
        buildingFields = new List<IslandField>();
    }

    void Start()
    {
        GameObject building = Instantiate(townCenter, Vector3.zero, Quaternion.identity);
        building.transform.Rotate(new Vector3(-90f, 0f, 0f));
        building.transform.localScale /= 3;
        building.transform.Translate(new Vector3(0f, 0.5f, 0f));

    }

    void Update()
    {
        GetMouseInput();
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
            if (island != null)
            {
                if (island.fieldType == ResourcesManager.ResourceToBuilding(resourceType)) ret.Add(island.gameObject);
            }
        }
        return ret;
    }
    void GetMouseInput()
    {
        if (Input.GetMouseButtonUp(0))
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<IslandField>() != null)
                {
                    if (BuildingSelection.lastIslands.Contains(hit.collider.gameObject))
                    {
                        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                        GameObject building = Instantiate(BuildingSelection.selected.GetComponent<BuildingSelection>().buildingPrefab, hit.collider.gameObject.transform.position, Quaternion.identity);
                        building.transform.Rotate(new Vector3(-90f, 0f, 0f));
                        building.transform.localScale /= 3;
                        building.transform.Translate(new Vector3(0f, 0.5f, 0f));
                        Destroy(hit.collider.gameObject);
                        if (hit.collider.gameObject.GetComponent<IslandField>().resourceObject != null)
                        {
                            Destroy(hit.collider.gameObject.GetComponent<IslandField>().resourceObject);
                        }

                        ResourcesManager.useResources(reqResources);
                        BuildingSelection.UnHighlightIslands();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            BuildingSelection.UnHighlightIslands();
        }
    }
}
