using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blocksclick : MonoBehaviour
{
    public int currentTower;
    public GameObject towerbuilt;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTower == 5 && towerbuilt != null)
        {
            towerbuilt.transform.GetChild(0).gameObject.GetComponent<Grenades>().currentblockon = GetComponent<blocksclick>();
        }
    }

}
