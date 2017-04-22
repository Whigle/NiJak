using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    #region zmienne

    List<GameObject> floatingIslands1_1;
    List<GameObject> floatingIslands3_1;
    public GameObject floatingIsland1_1;
    public GameObject floatingIsland3_1;
    public int initialIslandCount1_1;
    public int initialIslandCount3_1;
    float range;

    #endregion

    #region główne funkcje

    // Use this for initialization
    void Start () {
        range = 70f;
        floatingIslands1_1 = new List<GameObject>();
        floatingIslands3_1 = new List<GameObject>();
        for (int i = 0; i < initialIslandCount1_1; i++) instantiateFI11(floatingIsland1_1);
        for (int i = 0; i < initialIslandCount3_1; i++) instantiateFI31(floatingIsland1_1);
    }
	
	// Update is called once per frame
	void Update () {
		if(floatingIslands1_1.Count<initialIslandCount1_1) instantiateFI11(floatingIsland1_1);
        if (floatingIslands3_1.Count < initialIslandCount3_1)  instantiateFI31(floatingIsland1_1);
        
    }

    #endregion

    #region dodatkoweFunkcje

    public void removeIslandfromList(GameObject island)
    {
        if(floatingIslands1_1.Contains(island))
            floatingIslands1_1.Remove(island);
        if (floatingIslands3_1.Contains(island))
            floatingIslands3_1.Remove(island);
    }
    
    void instantiateFI11(GameObject island)
    {
        float positionx = Random.Range(-range, range);
        float positiony = Random.Range(-range, range);
        Vector3 translate = new Vector3(positionx, positiony, 0f);
        floatingIslands1_1.Add(Instantiate(island, this.transform.position + translate, new Quaternion()));
    }
    void instantiateFI31(GameObject island)
    {
        float positionx = Random.Range(-range, range);
        float positiony = Random.Range(-range, range);
        Vector3 translate = new Vector3(positionx, positiony, 0f);
        floatingIslands3_1.Add(Instantiate(island, this.transform.position + translate, new Quaternion()));
    }

    #endregion

}
