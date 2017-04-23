using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnResources : MonoBehaviour {

    public GameObject stone;
    public GameObject tree;
    public GameObject banana;
    public GameObject sugar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int randomSpawn(Resource resource, GameObject island)
    {
        int count = 0;
        if(resource==Resource.Wood)
        {
            foreach (var item in island.GetComponent<floatingIslandScript>().fields)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    GameObject res = Instantiate(tree, item + new Vector3(0, 0, -0.5f),Quaternion.Euler(Vector3.left * 90));
                    res.transform.localScale *= 0.4f;
                    res.transform.parent = island.transform;
                    count++;
                }
            }
        }
        else /*if(resource==Resource.Stone)*/
        {
            foreach (var item in island.GetComponent<floatingIslandScript>().fields)
            {
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    GameObject res = Instantiate(stone, item + new Vector3(0, 0, -0.5f), Quaternion.Euler(Vector3.left * 90));
                    res.transform.localScale *= 0.4f;
                    res.transform.parent = island.transform;
                    count++;
                }
            }
        }
        return count;
    }

}
