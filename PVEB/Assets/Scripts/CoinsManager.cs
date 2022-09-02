using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{

    public float coins = 0;
    public Text coinsText;
    public GameObject background;

    public GameObject cpanel, balpanel, flapanel, minpanel, grepanel;

    public GameObject[] TowerID;
    public Transform currentblock;

    blocksclick bc;
    RaycastManager raycm;
    void Start()
    {
        raycm = GetComponent<RaycastManager>();
    }

    void Update()
    {
        coinsText.text = coins + "$";

        if(coins >= 100)
        {
            cpanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            cpanel.GetComponent<Image>().color = Color.red;
        }

        if (coins >= 150)
        {
            balpanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            balpanel.GetComponent<Image>().color = Color.red;
        }

        if (coins >= 500)
        {
            flapanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            flapanel.GetComponent<Image>().color = Color.red;
        }

        if (coins >= 250)
        {
            minpanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            minpanel.GetComponent<Image>().color = Color.red;
        }

        if (coins >= 75)
        {
            grepanel.GetComponent<Image>().color = Color.green;
        }
        else
        {
            grepanel.GetComponent<Image>().color = Color.red;
        }

        //coins++;
    }

    public void closepurchase()
    {
        bc = currentblock.GetComponent<blocksclick>();
        raycm.check = true;
        if (bc.currentTower > 0)
        {
            bc.towerbuilt.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        background.SetActive(false);
        for (int i = background.transform.childCount -1; i >= 0; i--)
        {
            background.transform.GetChild(i).gameObject.SetActive(false);
        }
        Destroy(GameObject.Find("light(Clone)"));
    }

    public void purchase(int cost)
    {
        bc = currentblock.gameObject.GetComponent<blocksclick>();
        if (coins >= cost && cost == 75 && bc.currentTower == 0)
        {
            bc.towerbuilt = Instantiate(TowerID[4], currentblock.position, currentblock.rotation);
            bc.currentTower = 5;
            coins -= cost;
        }
        else if (coins >= cost && cost == 100 && bc.currentTower == 0)
        {
            bc.towerbuilt = Instantiate(TowerID[0], currentblock.position, currentblock.rotation);
            bc.currentTower = 1;
            coins -= cost;
        }
        else if (coins >= cost && cost == 150 && bc.currentTower == 0)
        {
            bc.towerbuilt = Instantiate(TowerID[1], currentblock.position, currentblock.rotation);
            bc.currentTower = 2;
            coins -= cost;
        }
        else if (coins >= cost && cost == 250 && bc.currentTower == 0)
        {
            bc.towerbuilt = Instantiate(TowerID[3], currentblock.position, currentblock.rotation);
            bc.currentTower = 4;
            coins -= cost;
        }
        else if (coins >= cost && cost == 500 && bc.currentTower == 0)
        {
            bc.towerbuilt = Instantiate(TowerID[2], currentblock.position, currentblock.rotation);
            bc.currentTower = 3;
            coins -= cost;
        }
    }
}
