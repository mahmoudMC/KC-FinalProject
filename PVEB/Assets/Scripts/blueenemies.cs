using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blueenemies : MonoBehaviour
{
    public float Maxhp;
    public float health;
    public float Maxshield;
    public float shield;
    public float speed;
    bool hasShield = true;

    bool gettingfire = false;

    RectTransform hpslider;
    RectTransform shslider;

    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.speed = speed;
        health = Maxhp;
        shield = Maxshield;
        hpslider = transform.GetChild(1).GetChild(0).gameObject.GetComponent<RectTransform>();
        shslider = transform.GetChild(1).GetChild(1).gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        hpslider.localScale = new Vector3(health / Maxhp, 0.5f, 1);
        if (shield > 0)
        {
            shslider.localScale = new Vector3(shield / Maxshield, 0.5f, 1);
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (shield <= 0 && hasShield)
        {
            hasShield = false;
            Destroy(gameObject.transform.GetChild(0).gameObject);
            Destroy(shslider.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "canonball")
        {
            if (shield > 0)
            {
                shield -= 10;
            }
            else
            {
                health -= 20;
            }
        }
        if(collision.gameObject.tag == "arrow")
        {
            if (shield > 0)
            {
                shield -= 20;
            }
            else
            {
                health -= 10;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("end"))
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!gettingfire)
        {
            StartCoroutine(Burning());
            gettingfire = true;
        }
    }

    IEnumerator Burning()
    {
        if (shield > 0)
        {
            shield -= 1;
        }
        else
        {
            health -= 2;
        }
        yield return new WaitForSeconds(0.05f);
        gettingfire = false;
    }

    public void booming(int bombdamage)
    {
        if (shield > 0)
        {
            shield -= bombdamage;
        }
        else
        {
            health -= bombdamage;
        }
        if (shield < 0)
        {
            health += shield;
            shield = 0;
        }
    }
}
