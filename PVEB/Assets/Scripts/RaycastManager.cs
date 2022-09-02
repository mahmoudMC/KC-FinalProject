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
    Text[] prices;
    Text[] levels;

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
                upgrade.GetChild(2).gameObject.SetActive(true);
                upgrade.GetChild(0).gameObject.SetActive(false);
                upgrade.GetChild(1).gameObject.SetActive(false);
            }
            else if (bsc.currentTower == 3)
            {
                upgrade.GetChild(1).gameObject.SetActive(true);
                upgrade.GetChild(0).gameObject.SetActive(false);
                upgrade.GetChild(2).gameObject.SetActive(false);
            }
            else if (bsc.currentTower == 4)
            {
                upgrade.GetChild(0).gameObject.SetActive(true);
                upgrade.GetChild(1).gameObject.SetActive(false);
                upgrade.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
