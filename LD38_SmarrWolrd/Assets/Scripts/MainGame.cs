using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    #region zmienne

    public int[][] grid;
    List<GameObject> floatingIslands;
    public GameObject floatingIsland;
    public int initialIslandCount;
    float range;

    #endregion

    #region główne funkcje

    // Use this for initialization
    void Start () {
        grid = new int[200][];
        for(int i=0;i<200;i++)
            grid[i] = new int[200];
        range = 70f;
        floatingIslands = new List<GameObject>();
        for (int i = 0; i < initialIslandCount; i++) instantiateFI11(floatingIsland);

        FindObjectOfType<initialIslandScript>().Mystart();
    }
	
	// Update is called once per frame
	void Update () {
		if(floatingIslands.Count<initialIslandCount) instantiateFI11(floatingIsland);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                if (grid!=null && grid[i][j] == 1)
                {
                    Vector3 temp = transform.position;
                    temp.x += i - 100;
                    temp.y += j - 100;
                    Gizmos.DrawCube(temp, new Vector3(0.9f, 0.9f, 1.1f));
                }
            }
        }
    }

    #endregion

    #region dodatkoweFunkcje

    public void removeIslandfromList(GameObject island)
    {
        if(floatingIslands.Contains(island))
            floatingIslands.Remove(island);
    }
    
    void instantiateFI11(GameObject island)
    {
        float positionx = Random.Range(-range, range);
        float positiony = Random.Range(-range, range);
        Vector3 translate = new Vector3(positionx, positiony, 0f);
        floatingIslands.Add(Instantiate(island, this.transform.position + translate, new Quaternion()));
    }

    #endregion

    

}
