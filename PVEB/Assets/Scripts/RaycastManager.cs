using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastManager : MonoBehaviour
{
    public GameObject purchase;
    Transform upgrade;
    public CoinsManager manager;
    bool purchaseopen = false;
    public bool check = false;
    RaycastHit2D hit;

    public GameObject particle;
    Text[] prices = new Text[6];
    Text[] levels = new Text[6];
    GameObject[] panels = new GameObject[6];
    int[] maths = new int[6];

    Camera cam;
    public LayerMask blocks;
    blocksclick bsc;
    void Start()
    {
        cam = Camera.main;
        upgrade = purchase.transform.GetChild(2);
        knowingPricesandLevels();
    }

    void knowingPricesandLevels()
    {
        panels[0] = upgrade.GetChild(0).GetChild(0).gameObject;
        panels[1] = upgrade.GetChild(1).GetChild(0).gameObject;
        panels[2] = upgrade.GetChild(1).GetChild(1).gameObject;
        panels[3] = upgrade.GetChild(2).GetChild(0).gameObject;
        panels[4] = upgrade.GetChild(2).GetChild(1).gameObject;
        panels[5] = upgrade.GetChild(2).GetChild(2).gameObject;
        // prices
        prices[0] = upgrade.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        prices[1] = upgrade.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        prices[2] = upgrade.GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
        prices[3] = upgrade.GetChild(2).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        prices[4] = upgrade.GetChild(2).GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
        prices[5] = upgrade.GetChild(2).GetChild(2).GetChild(1).gameObject.GetComponent<Text>();

        // levels
        levels[0] = upgrade.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
        levels[1] = upgrade.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
        levels[2] = upgrade.GetChild(1).GetChild(1).GetChild(2).gameObject.GetComponent<Text>();
        levels[3] = upgrade.GetChild(2).GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
        levels[4] = upgrade.GetChild(2).GetChild(1).GetChild(2).gameObject.GetComponent<Text>();
        levels[5] = upgrade.GetChild(2).GetChild(2).GetChild(2).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == true)
        {
            check = false;
            purchaseopen = false;
        }

        Vector2 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(mousepos, transform.TransformDirection(Vector3.forward), 20f, blocks);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit & !purchaseopen)
            {
                StartCoroutine(cooldown());
            }
        }
    }

    /*private void OnMouseDown()
    {
        if (purchaseopen == false)
        {
            StartCoroutine(cooldown());
        }
    }*/

    IEnumerator cooldown()
    {
        if (hit.collider.gameObject.transform.position.y < -1.7f)
        {
            purchase.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 275, 0);
            purchase.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(379, -70, 0);
        }
        else
        {
            purchase.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 59, 0);
            purchase.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(379, 66, 0);
        }
        purchaseopen = true;
        Instantiate(particle, hit.collider.gameObject.transform.position, particle.transform.rotation);
        bsc = hit.collider.gameObject.GetComponent<blocksclick>();
        manager.currentblock = hit.collider.gameObject.transform;
        yield return new WaitForSeconds(0.1f);
        purchase.SetActive(true);
        purchase.transform.GetChild(0).gameObject.SetActive(true);
        if (bsc.currentTower == 0)
        {
            purchase.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            bsc.towerbuilt.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            upgrade.gameObject.SetActive(true);
            if (bsc.currentTower == 1 || bsc.currentTower == 2)
            {
                // levels
                levels[3].text = "Level: " + bsc.currentlvl[3];
                levels[4].text = "Level: " + bsc.currentlvl[4];
                levels[5].text = "Level: " + bsc.currentlvl[5];
                // maths
                maths[3] = (100 * bsc.currentlvl[3]) + (50 * (bsc.currentlvl[3] - 1));
                maths[4] = (100 * bsc.currentlvl[3]) + (50 * (bsc.currentlvl[3] - 1));
                maths[5] = (100 * bsc.currentlvl[3]) + (50 * (bsc.currentlvl[3] - 1));
                // prices
                prices[3].text = maths[3] + "$";
                prices[4].text = maths[4] + "$";
                prices[5].text = maths[5] + "$";
                // checking prices
                for (int i = 3; i <6; i++)
                {
                    if (manager.coins >= maths[i])
                    {
                        panels[i].GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        panels[i].GetComponent<Image>().color = Color.red;
                    }
                }
                // showing
                upgrade.GetChild(2).gameObject.SetActive(true);
                upgrade.GetChild(0).gameObject.SetActive(false);
                upgrade.GetChild(1).gameObject.SetActive(false);
            }
            else if (bsc.currentTower == 3)
            {
                // levels
                levels[1].text = "Level: " + bsc.currentlvl[1];
                levels[2].text = "Level: " + bsc.currentlvl[2];
                // maths
                maths[1] = (300 * bsc.currentlvl[1]) + (75 * (bsc.currentlvl[1] - 2));
                maths[2] = (300 * bsc.currentlvl[1]) + (75 * (bsc.currentlvl[1] - 2));
                // prices
                prices[1].text = maths[1] + "$";
                prices[2].text = maths[2] + "$";
                // checking prices
                for (int i = 1; i < 3; i++)
                {
                    if (manager.coins >= maths[i])
                    {
                        panels[i].GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        panels[i].GetComponent<Image>().color = Color.red;
                    }
                }
                // showing
                upgrade.GetChild(1).gameObject.SetActive(true);
                upgrade.GetChild(0).gameObject.SetActive(false);
                upgrade.GetChild(2).gameObject.SetActive(false);
            }
            else if (bsc.currentTower == 4)
            {
                levels[0].text = "Level: " + bsc.currentlvl[0];
                maths[0] = (150 * bsc.currentlvl[0]);
                prices[0].text = maths[0] + "$";
                if (manager.coins >= maths[0])
                {
                    panels[0].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    levels[0].GetComponent<Image>().color = Color.red;
                }
                upgrade.GetChild(0).gameObject.SetActive(true);
                upgrade.GetChild(1).gameObject.SetActive(false);
                upgrade.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void UpgradeManager(int ID)
    {
        if (panels[ID].GetComponent<Image>().color == Color.green)
        {
            manager.coins -= maths[ID];
            bsc.currentlvl[ID]++;
            levels[ID].text = "Level: " + bsc.currentlvl[ID];
            if (ID <= 5 && ID >= 3)
            {
                maths[ID] = (100 * bsc.currentlvl[ID]) + (50 * (bsc.currentlvl[ID] - 1));
            }
            else if (ID >= 1)
            {
                maths[ID] = (300 * bsc.currentlvl[ID]) + (75 * (bsc.currentlvl[ID] - 2));
            }
            else if (ID == 0)
            {
                maths[ID] = (150 * bsc.currentlvl[ID]);
            }
            prices[ID].text = maths[ID] + "$";
            
            if (manager.coins >= maths[ID])
            {
                panels[ID].GetComponent<Image>().color = Color.green;
            }
            else
            {
                panels[ID].GetComponent<Image>().color = Color.red;
            }
        }

        for (int i = 0; i < 6; i++) 
        {
            if (manager.coins >= maths[i])
            {
                panels[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                panels[i].GetComponent<Image>().color = Color.red;
            }
        }
    }
}
