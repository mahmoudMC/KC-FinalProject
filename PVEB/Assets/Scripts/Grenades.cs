using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    int totaldamage = 300;
    //bool ready = false;
    bool ignited = false;
    public blocksclick currentblockon;

    public ParticleSystem smoke;
    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = transform.parent.GetComponent<AudioSource>();
        //ready = false;
        totaldamage = 300;
        smoke.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && !ignited)
        {
            ignited = true;
            StartCoroutine(boom());
        }
    }

    

    IEnumerator boom()
    {
        yield return new WaitForSeconds(1);
        aud.Play();
        explosion();
        smoke.Play();
        yield return new WaitForSeconds(0.5f);
        smoke.Stop();
        currentblockon.currentTower = 0;
        currentblockon.towerbuilt = null;
        Destroy(transform.parent.gameObject);
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (ready)
        {
            ready = false;
            collision.gameObject.GetComponent<blueenemies>().booming(totaldamage);
        }
    }*/

    void explosion()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            enemy.GetComponent<blueenemies>().booming(totaldamage);
        }
    }
}
