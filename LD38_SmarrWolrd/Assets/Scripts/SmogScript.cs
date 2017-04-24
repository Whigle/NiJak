﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogScript : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    Vector3 startPosition;
    float spwnTime;

    void Awake ()
    {
        spwnTime = Time.time;
        direction = Vector3.zero - transform.position;
        direction.Normalize ();
        transform.position = new Vector3 (transform.position.x, transform.position.y, -0.51f);
        transform.rotation = Quaternion.Euler (-180f, 0f, 0f);
        transform.localScale = new Vector3 (ResourcesManager.getResource (Resource.Smog) + 10, ResourcesManager.getResource (Resource.Smog) + 10, 1f);
        startPosition = transform.position;


        speed = Random.Range (2f, 4f);
    }


    void Update ()
    {
        if (Time.time > spwnTime + 30f)
        {
            Destroy (gameObject);
        }
        transform.Translate (direction * speed * Time.deltaTime);

    }

    void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.tag != "Island" && c.gameObject.tag != "ChunkIsland")
        {
            Destroy (c.gameObject);
        }
        //else if (c.gameObject.tag != "Rtree")
        //{
        //    Destroy (gameObject);
        //}
    }

    void OnCollisionEnter (Collision c)
    {
        if (c.gameObject.tag != "Island" && c.gameObject.tag != "ChunkIsland")
        {
            Destroy (c.gameObject);
        }
        //else if (c.gameObject.tag != "Rtree")
        //{
        //    Destroy (gameObject);
        //}
    }
}
