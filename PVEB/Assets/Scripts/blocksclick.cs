using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blocksclick : MonoBehaviour
{
    public int currentTower;
    public GameObject towerbuilt;
    public int[] currentlvl = new int[6];
    public int dmgtohealth, dmgtoshield, speed;
    public float multi;
    void Start()
    {
        currentlvl[0] = 1;
        currentlvl[1] = 1;
        currentlvl[2] = 1;
        currentlvl[3] = 1;
        currentlvl[4] = 1;
        currentlvl[5] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTower == 5 && towerbuilt != null)
        {
            towerbuilt.transform.GetChild(0).gameObject.GetComponent<Grenades>().currentblockon = GetComponent<blocksclick>();
        }

        if (currentTower == 1)
        {
            dmgtohealth = currentlvl[3] * 20;
            dmgtoshield = currentlvl[4] * 10;
            speed = -currentlvl[5] + 4;
            towerbuilt.transform.GetChild(0).GetComponent<canonscript>().damage1 = dmgtohealth;
            towerbuilt.transform.GetChild(0).GetComponent<canonscript>().damage2 = dmgtoshield;
            towerbuilt.transform.GetChild(0).GetComponent<canonscript>().speed = speed;
        }
        else if (currentTower == 2)
        {
            dmgtohealth = currentlvl[3] * 10;
            dmgtoshield = currentlvl[4] * 20;
            speed = -currentlvl[5] + 4;
            towerbuilt.transform.GetChild(0).GetComponent<BallistaScript>().damage1 = dmgtohealth;
            towerbuilt.transform.GetChild(0).GetComponent<BallistaScript>().damage2 = dmgtoshield;
            towerbuilt.transform.GetChild(0).GetComponent<BallistaScript>().speed = speed;
        }
        else if (currentTower == 3)
        {
            dmgtohealth = currentlvl[1] * 1;
            dmgtoshield = currentlvl[2] * 1;
            towerbuilt.transform.GetChild(0).GetComponent<FlameThrower>().damage1 = dmgtohealth;
            towerbuilt.transform.GetChild(0).GetComponent<FlameThrower>().damage2 = dmgtoshield;
        }
        else if (currentTower == 4)
        {
            multi = (currentlvl[0] * 0.5f) + 0.5f;
            towerbuilt.GetComponent<GoldMiner>().CoinsMultiply = multi;
        }
    }

}
