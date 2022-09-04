using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMiner : MonoBehaviour
{
    int miningSpeed = 10;
    public float CoinsMultiply = 1;
    bool currentlymining = false;
    CoinsManager managers;
    // Start is called before the first frame update
    void Start()
    {
        managers = GameObject.FindObjectOfType<CoinsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!currentlymining && collision.gameObject.CompareTag("gold"))
        {
            currentlymining = true;
            StartCoroutine(Mining());
        }
    }

    IEnumerator Mining()
    {
        managers.coins += 30 * CoinsMultiply;
        yield return new WaitForSeconds(miningSpeed);
        currentlymining = false;
    }
}
