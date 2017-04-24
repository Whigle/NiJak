using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialIslandScript : MonoBehaviour
{

    #region zmienne

    public GameObject Minion;
    public MainGame mainGame;
    GameObject firstMinion;

    public GameObject box;
    public bool isGrid = false;
    bool flaga = false;
    #endregion

    #region główne funkcje

    // Use this for initialization
    void Start()
    {
        
        firstMinion = Instantiate(Minion, transform.position + Vector3.back * 0.75f, Quaternion.Euler(0, 0, 0));
        firstMinion.GetComponent<minionScript>().spawner=this.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mystart()
    {
        if (mainGame == null) mainGame = GameObject.Find("MainGame").GetComponent<MainGame>();
        firstMinion.GetComponent<minionScript>().grid = mainGame.grid;
        testuj();

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<floatingIslandScript>().speed = 0f;
        collision.gameObject.GetComponent<floatingIslandScript>().direction = new Vector3(0, 0, 0);
        collision.gameObject.GetComponent<floatingIslandScript>().partOfIsland = true;
        Vector3 temp = collision.gameObject.transform.position;

        temp.z = 0;
        temp.x = System.Convert.ToInt32(temp.x);
        temp.y = System.Convert.ToInt32(temp.y);
        collision.gameObject.transform.position = temp;
        mainGame.removeIslandfromList(collision.gameObject);
        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (collision.gameObject.GetComponent<floatingIslandScript> () != null && !collision.gameObject.GetComponent<floatingIslandScript> ().partOfIsland)
        {
            FindObjectOfType<CameraScript> ().SHAKE ();
        }
    }

    private void testuj()
    {
        GameObject cube;
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float transx = 1 / x;
        float transy = 1 / y;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 temp = transform.position;
                temp.x += i - (int)x / 2;
                temp.y += j - (int)y / 2;
                mainGame.grid[(int)temp.x + 100][(int)temp.y + 100] = 1;
                cube = Instantiate (box, temp, new Quaternion ());
                cube.transform.parent = gameObject.transform;

                BuildingManager.AddBuildingField (cube.GetComponent<IslandField> ());

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
    #endregion

    #region dodatkowe funkcje

    #endregion

}
