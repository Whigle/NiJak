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

    void OnGUI()
    {
        Vector3 p = new Vector3();
        Camera c = Camera.main;
        Event e = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = e.mousePosition.x;
        mousePos.y = c.pixelHeight - e.mousePosition.y;

        p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -c.transform.position.z));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + p.ToString("F3"));
        GUILayout.EndArea();
    }

}
