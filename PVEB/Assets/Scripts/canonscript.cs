using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonscript : MonoBehaviour
{
    GameObject currentenemy;
    Transform mainbody;
    public GameObject bullet;
    GameObject currentbullet;
    AudioSource auds;

    // stats
    public int damage1, damage2;
    public float speed = 2;

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
        yield return new WaitForSeconds(speed);
        if (currentenemy != null)
        {
            auds.Play();
            currentbullet = Instantiate(bullet, mainbody.position, Quaternion.FromToRotation(Vector3.up, currentenemy.transform.position - mainbody.position));
            currentbullet.GetComponent<bullets>().dmgToHealth = damage1;
            currentbullet.GetComponent<bullets>().dmgToShield = damage2;
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
