using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnResources : MonoBehaviour {

    public GameObject stone;
    public GameObject tree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void randomSpawn(Resource resource,GameObject island)
    {
        if(resource==Resource.Wood)
        {
            foreach (var item in island.GetComponent<floatingIslandScript>().fields)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    Instantiate(tree, item+new Vector3(0,0,-0.5f), Quaternion.Euler(Vector3.left * 90)).transform.parent = island.transform;
                }
            }
        }
        else /*if(resource==Resource.Stone)*/
        {
            foreach (var item in island.GetComponent<floatingIslandScript>().fields)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    Instantiate(stone, item + new Vector3(0, 0, -0.5f), Quaternion.Euler(Vector3.left * 90)).transform.parent = island.transform;
                }
            }
        }
    }

}
