using System.Collections;
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

    #endregion

    #region główne funckje

    // Use this for initialization
    void Awake()
    {
        fields = new List<Vector3>();
        int scalex = (int)Random.Range(0f, 8f);
        if (scalex % 2 != 0) scalex++;
        int scaley = (int)Random.Range(0f, 8f);
        if (scaley % 2 != 0) scaley++;
        transform.localScale += new Vector3(scalex, scaley);
        direction = Vector3.zero - transform.position;
        direction.Normalize();
        speed = Random.Range(1f, 2f);
        partOfIsland = false;
        if (mainGame == null) mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        int r = Random.Range(0, 6);
        switch (r)
        {
            case 0:
                resource = Resource.Food;
                break;
            case 1:
                resource = Resource.BuildingMaterial;
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
            case 5:
                resource = Resource.Stone;
                break;
            default:
                break;
        }
        calculateFields();
        resourceCount = GetComponent<spawnResources>().randomSpawn(resource,gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (partOfIsland && !flaga)
        {
            testuj(); flaga = true;
        }
        //testuj();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!partOfIsland) direction = (transform.position - collision.transform.position).normalized;
        else if (collision.gameObject.GetComponent<floatingIslandScript>() != null && !collision.gameObject.GetComponent<floatingIslandScript>().partOfIsland)
        {
            Vector3 temp = collision.gameObject.transform.position;
            //print(temp.x + " " + System.Convert.ToInt32(temp.x));
            temp.x = System.Convert.ToInt32(temp.x);
            temp.y = System.Convert.ToInt32(temp.y > 0 ? temp.y - 0.5f : temp.y);
            collision.gameObject.transform.position = temp;
            mainGame.removeIslandfromList(collision.gameObject);
            collision.gameObject.GetComponent<floatingIslandScript>().speed = 0f;
            collision.gameObject.GetComponent<floatingIslandScript>().direction = new Vector3(0, 0, 0);
            collision.gameObject.GetComponent<floatingIslandScript>().partOfIsland = true;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ResourcesManager.increaseResource(resource, resourceCount);
        }
    }

    public float x;
    public float y;

    private void calculateFields()
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
                temp.x += i - (int)x / 2;
                temp.y += j - (int)y / 2;
                fields.Add(temp);
            }
        }
    }

    private void testuj()
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
                temp.x += i - (int)x / 2;
                temp.y += j - (int)y / 2;
                CheckNeighbors((int)temp.x, (int)temp.y);
            }
        }
    }

    private void CheckNeighbors(int x, int y)
    {
        GameObject cube;
        if (mainGame.grid[x + 100][y + 101] == 0 && mainGame.grid[x + 100][y + 99] == 0 && mainGame.grid[x + 101][y + 100] == 0 && mainGame.grid[x + 99][y + 100] == 0)
        {
            Vector3 temp = transform.position;
            float xx = temp.x;
            float yy = temp.y;
            if (Mathf.Abs(xx) > Mathf.Abs(yy))
            {
                if (xx < 0)
                {
                    transform.position += Vector3.right;
                    mainGame.grid[x + 101][y + 100] = 1;
                    cube = Instantiate(box, new Vector3(x + 1, y, 0), new Quaternion());
                    cube.transform.parent = gameObject.transform;
                    //print("right");
                }
                else
                {
                    transform.position += Vector3.left;
                    mainGame.grid[x + 99][y + 100] = 1;
                    cube = Instantiate (box, new Vector3 (x - 1, y, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;
                    // print("left");
                }
            }
            else
            {
                if (yy < 0)
                {
                    transform.position += Vector3.up;
                    mainGame.grid[x + 100][y + 101] = 1;
                    cube = Instantiate (box, new Vector3 (x, y + 1, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;
                    //print("up");
                }
                else
                {
                    transform.position += Vector3.down;
                    mainGame.grid[x + 100][y + 99] = 1;
                    cube = Instantiate (box, new Vector3 (x, y - 1, 0), new Quaternion ());
                    cube.transform.parent = gameObject.transform;
                    //print("down");
                }
            }

        }
        else
        {
            if (mainGame.grid[x + 100][y + 100] == 1 && mainGame.grid[x + 100][y + 101] == 0)
            {
                //print("here 1");
                transform.position += Vector3.up;
                mainGame.grid[x + 100][y + 101] = 1;
                cube = Instantiate (box, new Vector3 (x, y + 1, 0), new Quaternion ());
                cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid[x + 100][y + 100] == 1 && mainGame.grid[x + 100][y + 99] == 0)
            {
                //print("here 2");
                transform.position += Vector3.down;
                mainGame.grid[x + 100][y + 99] = 1;
                cube = Instantiate (box, new Vector3 (x, y - 1, 0), new Quaternion ());
                cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid[x + 100][y + 100] == 1 && mainGame.grid[x + 99][y + 100] == 0)
            {
               // print("here 3");
                transform.position += Vector3.left;
                mainGame.grid[x + 99][y + 100] = 1;
                cube = Instantiate (box, new Vector3 (x - 1, y, 0), new Quaternion ());
                cube.transform.parent = gameObject.transform;
            }
            else if (mainGame.grid[x + 100][y + 100] == 1 && mainGame.grid[x + 101][y + 100] == 0)
            {
                //print("here 4");
                transform.position += Vector3.right;
                mainGame.grid[x + 101][y + 100] = 1;
                cube = Instantiate (box, new Vector3 (x + 1, y, 0), new Quaternion ());
                cube.transform.parent = gameObject.transform;
            }
            else
            {
                //print("here 5");
                mainGame.grid[x + 100][y + 100] = 1;
                cube = Instantiate (box, new Vector3 (x, y, 0), new Quaternion ());
                cube.transform.parent = gameObject.transform;
            }
        }
    }


    #endregion

    #region dodatkowe funkcje

    #endregion
}
