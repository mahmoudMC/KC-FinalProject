using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaScript : MonoBehaviour
{
    GameObject currentenemy;
    Transform mainbody;
    public GameObject bullet;
    GameObject currentbullet;
    AudioSource auds;

    // stats
    float range;
    int damage;
    float speed = 3;

    void Start()
    {
        mainbody = transform.parent.gameObject.transform;
        auds = mainbody.GetComponent<AudioSource>();
        StartCoroutine(shootenemy());
    }

    void Update()
    {
        if (currentenemy != null)
        {
            Vector2 lookingAt = currentenemy.transform.position - mainbody.position;
            mainbody.rotation = Quaternion.FromToRotation(Vector3.right, lookingAt);
        }
    }


    IEnumerator shootenemy()
    {
        yield return new WaitForSeconds(speed - 1);
        if (currentenemy != null)
        {
            auds.Play();
            yield return new WaitForSeconds(1);
            currentbullet = Instantiate(bullet, mainbody.position, Quaternion.FromToRotation(Vector3.up, currentenemy.transform.position - mainbody.position));
            currentbullet.GetComponent<bullets>().theEnemy = currentenemy.transform;
        }
        StartCoroutine(shootenemy());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            if (currentenemy == null)
            {
                currentenemy = collision.gameObject;
            }
            else if (Vector3.Distance(mainbody.transform.position, currentenemy.transform.position) > Vector3.Distance(mainbody.transform.position, collision.transform.position))
            {
                currentenemy = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentenemy == collision.gameObject)
        {
            currentenemy = null;
        }
    }
}
