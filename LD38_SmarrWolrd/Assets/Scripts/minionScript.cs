using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionScript : MonoBehaviour
{
    static public int population;
    static public int maxPop = 10;
    public int maxPopulation = 10;
    float speed;
    int gridPositionx;
    int gridPositiony;
    int aimGridx;
    int aimGridy;
    public GameObject spawner;
    public int[][] grid;
    static public bool die = false;
    bool dieingTime = false;
    // Use this for initialization
    void Start()
    {
        population++;
        aimGridx = gridPositionx = (int)transform.position.x + 100;
        aimGridy = gridPositiony = (int)transform.position.y + 100;
        speed = UnityEngine.Random.Range(1f,2f) ;
        time = Time.time+3f;
    }

    float time;
    float timetoDie;
    
    // Update is called once per frame
    void Update()
    {
        if(ResourcesManager.hasResource(Resource.Food, population)){
            die = false; dieingTime = false;
        }
        if (die&&!dieingTime)
        {
            timetoDie = Time.time + UnityEngine.Random.Range(3f, 15f);
            dieingTime = true;
        }

        print(population);
        maxPop =BuildingManager.buildingFields.Count+maxPopulation;
        if (Time.time > timetoDie && die)
        {
            Destroy(this.gameObject);
            population--;
        }
        if(Time.time > time && ResourcesManager.getResource(Resource.Food)>0)
        {
            //print(maxPop);
            time += 3f;
        }
        /*if (grid == null)
        {
            //grid = spawner.GetComponent<initialIslandScript>().mainGame.GetComponent<MainGame>().grid;
        }*/
        else
        {
            if (Mathf.Abs(transform.position.x - aimGridx+100) < 0.1f && Mathf.Abs(transform.position.y - aimGridy+100) < 0.1f)
            {
                gridPositionx = aimGridx;
                gridPositiony = aimGridy;
                bool done = false;
                while (!done)
                {
                    int tempx = UnityEngine.Random.Range(-1, 2);
                    int tempy = UnityEngine.Random.Range(-1, 2);
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
