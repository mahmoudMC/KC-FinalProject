using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    GameObject currentenemy;
    Transform mainbody;
    ParticleSystem fire;
    AudioSource auds;

    // stats
    float range;
    int damage;
    void Start()
    {
        fire = transform.parent.GetChild(1).GetComponent<ParticleSystem>();
        fire.Stop();
        mainbody = transform.parent.gameObject.transform;
        auds = mainbody.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentenemy != null)
        {
            Vector2 lookingAt = currentenemy.transform.position - mainbody.position;
            mainbody.rotation = Quaternion.FromToRotation(Vector3.right, lookingAt);
        }
        else
        {
            fire.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            if (currentenemy == null)
            {
                currentenemy = collision.gameObject;
                auds.Play();
                fire.Play();
            }
            else if (Vector3.Distance(mainbody.transform.position, currentenemy.transform.position) > Vector3.Distance(mainbody.transform.position, collision.transform.position))
            {
                currentenemy = collision.gameObject;
                fire.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentenemy == collision.gameObject)
        {
            currentenemy = null;
            auds.Stop();
        }
    }
}
