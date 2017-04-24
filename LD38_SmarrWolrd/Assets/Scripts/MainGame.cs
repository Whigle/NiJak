using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {

    #region zmienne

    int minx, miny, maxx, maxy;
    public int[][] grid;
    List<GameObject> floatingIslands;
    public GameObject floatingIsland;
    public int initialIslandCount;
    float range;

    Vector3 initPosition;

    #endregion

    #region główne funkcje

    // Use this for initialization
    void Start () {
        initPosition = Vector3.up * 10f;
        minx = 100; miny = 100; maxx = 100; maxy = 100;
        grid = new int[200][];
        for(int i=0;i<200;i++)
            grid[i] = new int[200];
        range = 70f;
        floatingIslands = new List<GameObject>();
        //for (int i = 0; i < initialIslandCount; i++) instantiateFI11(floatingIsland);
        
        FindObjectOfType<initialIslandScript>().Mystart();
    }
	
	// Update is called once per frame
	void Update () {


        //print(minx + " " + miny + " " + maxx + " " + maxy);
        //print((minx + maxx) / 2f + " " + (miny + maxy) / 2f);

        float distx = Mathf.Abs(minx - (minx + maxx) / 2f) + 5f;
        float disty = Mathf.Abs(miny - (miny + maxy) / 2f) + 5f;
        float dist = Mathf.Sqrt(distx * distx + disty * disty);

        //print(dist);

        initPosition = Vector3.up * (dist);
        if (floatingIslands.Count < initialIslandCount) {
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    if (grid != null && grid[i][j] == 1)
                    {
                        if (i < minx) minx = i;
                        if (i > maxx) maxx = i;
                        if (j < miny) miny = j;
                        if (j > maxy) maxy = j;
                        /*Vector3 temp = transform.position;
                        temp.x += i - 100;
                        temp.y += j - 100;
                        Gizmos.DrawCube(temp, new Vector3(0.9f, 0.9f, 1.1f));*/
                    }
                }
            }
            instantiateFI11(floatingIsland); }

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
                    if (i < minx) minx = i;
                    if (i > maxx) maxx = i;
                    if (j < miny) miny = j;
                    if (j > maxy) maxy = j;
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
        /*float positionx = Random.Range(-range, range);
        float positiony = Random.Range(-range, range);
        Vector3 translate = new Vector3(positionx, positiony, 0f);*/
        int choice = Random.Range(1, 9);
        Vector3 direct=Vector3.zero;
        switch (choice)
        {
            case 1:
                initPosition.x = minx - 100 - 4;
                initPosition.y = miny - 100 - 30;
                direct = Vector3.up;
                break;
            case 2:
                initPosition.x = minx - 100 - 30;
                initPosition.y = miny - 100 - 4;
                direct = Vector3.right;
                break;
            case 3:
                initPosition.x = minx - 100 - 4;
                initPosition.y = maxy - 100 + 30;
                direct = Vector3.down;
                break;
            case 4:
                initPosition.x = minx - 100 - 30;
                initPosition.y = maxy - 100 + 4;
                direct = Vector3.right;
                break;
            case 5:
                initPosition.x = maxx - 100 + 30;
                initPosition.y = miny - 100 - 4;
                direct = Vector3.left;
                break;
            case 6:
                initPosition.x = maxx - 100 + 4;
                initPosition.y = miny - 100 - 30;
                direct = Vector3.up;
                break;
            case 7:
                initPosition.x = maxx - 100 + 30;
                initPosition.y = maxy - 100 + 4;
                direct = Vector3.left;
                break;
            case 8:
                initPosition.x = maxx - 100 + 4;
                initPosition.y = maxy - 100 + 30;
                direct = Vector3.down;
                break;
            default:
                break;
        }
        GameObject temp = Instantiate(island, initPosition, new Quaternion());
        temp.GetComponent<floatingIslandScript>().direction = direct;
        floatingIslands.Add(temp);
    }

    #endregion

    

}
