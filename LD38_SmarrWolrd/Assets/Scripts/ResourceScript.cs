using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    public Resource resourceType;

    void Start ()
    {

    }

    void Update ()
    {

    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.GetComponent<ResourceScript> () != null)
        {
            Destroy (col.gameObject);
        }
        if (col.gameObject.GetComponent<BuildingObject> () != null)
        {
            Destroy (col.gameObject);
        }
    }

}
