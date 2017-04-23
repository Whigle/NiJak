using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class IslandField : MonoBehaviour
{
    public Building fieldType;
    public GameObject resourceObject;

    void Start ()
    {
        //fieldType = Building.None;
        RaycastHit ray;
        if (Physics.Raycast (transform.position, Vector3.back, out ray, 10f))
        {
            if (ray.collider.gameObject.GetComponent<ResourceScript>() != null)
            {
                fieldType = ResourcesManager.ResourceToBuilding(ray.collider.gameObject.GetComponent<ResourceScript>().resourceType);
                resourceObject = ray.collider.gameObject;
            }
        }
        /*foreach (IslandField field in BuildingManager.buildingFields)
        {
            if (field.transform.position.x == transform.position.x
             && field.transform.position.y == transform.position.y)
            {
                BuildingManager.buildingFields.Remove (this);
                Destroy (gameObject);
            }
        }*/
    }

    void Update ()
    {

    }


    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.CompareTag ("ChunkIsland"))
        {
            Destroy (gameObject);
        }
    }
    

}
