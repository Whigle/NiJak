﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingIslandScript : MonoBehaviour
{

    #region zmienne

    static public MainGame mainGame;
    public Vector3 direction;
    public float speed;
    public bool partOfIsland;
    public GameObject box;
    bool flaga = false;
    public Resource resource;
    public int resourceCount;
    public List<Vector3> fields;
    public bool hooked;

    Vector3 startPosition;
    float spwnTime;


    #endregion

    #region główne funckje

    // Use this for initialization
    void Awake ()
    {
        spwnTime = Time.time;
        hooked = false;
        fields = new List<Vector3> ();
        startPosition = transform.position;

        int scalex = (int) Random.Range (0f, 100f);
        if (scalex < 70)
        {
            scalex = 0;
        }
        else if (scalex < 90)
        {
            scalex = 2;
        }
        else
        {
            scalex = 4;
        }
        if (scalex % 2 != 0)
            scalex++;
        int scaley = (int) Random.Range (0f, 100f);
        if (scaley < 70)
        {
            scaley = 0;
        }
        else if (scaley < 90)
        {
            scaley = 2;
        }
        else
        {
            scaley = 4;
        }
        if (scaley % 2 != 0)
            scaley++;
        transform.localScale += new Vector3 (scalex, scaley);
        direction = Vector3.zero - transform.position;
        direction.Normalize ();
        speed = Random.Range (2f, 4f);
        partOfIsland = false;
        if (mainGame == null)
            mainGame = GameObject.Find ("MainGame").GetComponent<MainGame> ();
        int r = Random.Range (0, 6);
        switch (r)
        {
            case 0:
                resource = Resource.None;
                break;
            case 1:
                resource = Resource.Stone;
                break;
            case 2:
                resource = Resource.Bananas;
                break;
            case 3:
                resource = Resource.Sugar;
                break;
            case 4:
                resource = Resource.Wood;
                break;
            default:
                break;
        }
        calculateFields ();
        resourceCount = GetComponent<spawnResources> ().randomSpawn (resource, gameObject);

    }

    // Update is called once per frame
    void Update ()
    {
        if (!partOfIsland) { if (Time.time > spwnTime + 30f)
            {
                mainGame.removeIslandfromList(this.gameObject); Destroy(this.gameObject);
            }
        }
        if (partOfIsland && !flaga)
        {
            testuj();
            flaga = true;
        }
        else
        {
            //if (Time.time > spwnTime + 5f) Destroy(this.gameObject);
            transform.Translate(direction * speed * Time.deltaTime);
            //else transform.position = (Quaternion.Euler(0, 0, Time.time*10) * startPosition);
        }
        //testuj();
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (!partOfIsland)
            direction = (transform.position - collision.transform.position).normalized;
        else if (collision.gameObject.GetComponent<floatingIslandScript> () != null && !collision.gameObject.GetComponent<floatingIslandScript> ().partOfIsland)
        {
            Vector3 temp = collision.gameObject.transform.position;
            temp.z = 0;
            //print(temp.x + " " + System.Convert.ToInt32(temp.x));
            temp.x = System.Convert.ToInt32 (temp.x);
            temp.y = System.Convert.ToInt32 (temp.y > 0 ? temp.y - 0.5f : temp.y);
            collision.gameObject.transform.position = temp;
            mainGame.removeIslandfromList (collision.gameObject);
            collision.gameObject.GetComponent<floatingIslandScript> ().speed = 0f;
            collision.gameObject.GetComponent<floatingIslandScript> ().direction = new Vector3 (0, 0, 0);
            collision.gameObject.GetComponent<floatingIslandScript> ().partOfIsland = true;
            collision.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
            ResourcesManager.increaseResource (resource, resourceCount);
            FindObjectOfType<CameraScript> ().SHAKE ();
        }
    }

    public float x;
    public float y;

    private void calculateFields ()
    {
        x = transform.localScale.x;
        y = transform.localScale.y;
        float transx = 1 / x;
        float transy = 1 / y;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 temp = transform.position;
                temp.x += i - (int) x / 2;
                temp.y += j - (int) y / 2;
                fields.Add (temp);
            }
        }
    }

    private void testuj ()
    {
        //Gizmos.color = Color.yellow;
        x = transform.localScale.x;
        y = transform.localScale.y;
        float transx = 1 / x;
        float transy = 1 / y;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 temp = transform.position;
                temp.x += i - (int) x / 2;
                temp.y += j - (int) y / 2;
                CheckNeighbors ((int) temp.x, (int) temp.y);
            }
        }
    }

    private void CheckNeighbors (int x, int y)
    {
        GameObject cube;
        if (mainGame.grid [x + 100] [y + 101] == 0 && mainGame.grid [x + 100] [y + 99] == 0 && mainGame.grid [x + 101] [y + 100] == 0 && mainGame.grid [x + 99] [y + 100] == 0)
        {
            Vector3 temp = transform.position;
            float xx = temp.x;
            float yy = temp.y;
            if (Mathf.Abs (xx) > Mathf.Abs (yy))
            {
                if (xx < 0)
                {
                    transform.position += Vector3.right;
                    mainGame.grid [x + 101] [y + 100] = 1;
                    cube = Instantiate (box, new Vector3 (x + 1, y, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;

                    BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());
                    //print("right");
                }
                else
                {
                    transform.position += Vector3.left;
                    mainGame.grid [x + 99] [y + 100] = 1;
                    cube = Instantiate (box, new Vector3 (x - 1, y, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;

                    BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());
                    // print("left");
                }
            }
            else
            {
                if (yy < 0)
                {
                    transform.position += Vector3.up;
                    mainGame.grid [x + 100] [y + 101] = 1;
                    cube = Instantiate (box, new Vector3 (x, y + 1, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;

                    BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());
                    //print("up");
                }
                else
                {
                    transform.position += Vector3.down;
                    mainGame.grid [x + 100] [y + 99] = 1;
                    cube = Instantiate (box, new Vector3 (x, y - 1, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;

                    BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());
                    //print("down");
                }
            }

        }
        else
        {
            if (mainGame.grid [x + 100] [y + 100] == 1 && mainGame.grid [x + 100] [y + 101] == 0)
            {
                //print ("here 1");
                //transform.position += Vector3.up;
                //mainGame.grid[x + 100][y + 101] = 1;
                //cube = Instantiate (box, new Vector3 (x, y + 1, 0), new Quaternion ());
                //cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid [x + 100] [y + 100] == 1 && mainGame.grid [x + 100] [y + 99] == 0)
            {
                //print ("here 2");
                //transform.position += Vector3.down;
                //mainGame.grid[x + 100][y + 99] = 1;
                //cube = Instantiate (box, new Vector3 (x, y - 1, 0), new Quaternion ());
                //cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid [x + 100] [y + 100] == 1 && mainGame.grid [x + 99] [y + 100] == 0)
            {
                //print ("here 3");
                //transform.position += Vector3.left;
                //mainGame.grid[x + 99][y + 100] = 1;
                //cube = Instantiate (box, new Vector3 (x - 1, y, 0), new Quaternion ());
                //cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid [x + 100] [y + 100] == 1 && mainGame.grid [x + 101] [y + 100] == 0)
            {
                //print ("here 4");
                //transform.position += Vector3.right;
                //mainGame.grid[x + 101][y + 100] = 1;
                //cube = Instantiate (box, new Vector3 (x + 1, y, 0), new Quaternion ());
                //cube.transform.parent = gameObject.transform;
            }
            //else
            //{
            //print("here 5");
            mainGame.grid [x + 100] [y + 100] = 1;
            cube = Instantiate (box, new Vector3 (x, y, 0), new Quaternion ());
            cube.transform.parent = gameObject.transform;

            BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());
            //}
        }
    }


    #endregion

    #region dodatkowe funkcje

    #endregion
}
