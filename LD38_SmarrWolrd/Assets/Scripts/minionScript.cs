using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionScript : MonoBehaviour
{
    static int population;
    static int maxPop = 10;
    public int maxPopulation = 10;
    float speed;
    int gridPositionx;
    int gridPositiony;
    int aimGridx;
    int aimGridy;
    public GameObject spawner;
    public int[][] grid;
    // Use this for initialization
    void Start()
    {
        population++;
        aimGridx = gridPositionx = (int)transform.position.x + 100;
        aimGridy = gridPositiony = (int)transform.position.y + 100;
        speed = Random.Range(1f,2f) ;
        time = Time.time+3f;
        timetoDie = Time.time + Random.Range(3f, 15f);
    }

    float time;
    float timetoDie;

    // Update is called once per frame
    void Update()
    {
        maxPop=BuildingManager.buildingFields.Count+maxPopulation;
        if (Time.time > timetoDie)
        {
            Destroy(this.gameObject);
            population--;
        }
        if(Time.time > time)
        {
            print(maxPop);
            time += 3f;
            if (population<maxPop) Instantiate(gameObject,transform.position,transform.rotation).GetComponent<minionScript>().grid=this.grid;
        }
        if (grid == null)
        {
            //grid = spawner.GetComponent<initialIslandScript>().mainGame.GetComponent<MainGame>().grid;
        }
        else
        {
            if (Mathf.Abs(transform.position.x - aimGridx+100) < 0.1f && Mathf.Abs(transform.position.y - aimGridy+100) < 0.1f)
            {
                gridPositionx = aimGridx;
                gridPositiony = aimGridy;
                bool done = false;
                while (!done)
                {
                    int tempx = Random.Range(-1, 2);
                    int tempy = Random.Range(-1, 2);
                    if (grid[gridPositionx + tempx][gridPositiony + tempy]==1)
                    {
                        aimGridx = gridPositionx + tempx;
                        aimGridy = gridPositiony + tempy;
                        done = true;
                    }
                }
            }
            else
            {
                transform.Translate(new Vector3(aimGridx - transform.position.x-100, aimGridy - transform.position.y-100, 0).normalized * speed * Time.deltaTime);
            }
        }
    }
}
