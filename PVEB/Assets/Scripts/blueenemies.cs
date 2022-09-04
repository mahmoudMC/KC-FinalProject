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
    bool insiderange = false;

    RectTransform hpslider;
    RectTransform shslider;

    Animator ani;
    WavesManager wavesManager;
    public bool IsKing;
    void Start()
    {
        wavesManager = GameObject.FindObjectOfType<WavesManager>();
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
            GameObject.FindObjectOfType<CoinsManager>().coins += (Maxhp / 5) * 2;
            wavesManager.totalEnemies--;
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
                shield -= collision.gameObject.GetComponent<bullets>().dmgToShield;
            }
            else
            {
                health -= collision.gameObject.GetComponent<bullets>().dmgToHealth;
            }
        }
        if(collision.gameObject.tag == "arrow")
        {
            if (shield > 0)
            {
                shield -= collision.gameObject.GetComponent<bullets>().dmgToShield;
            }
            else
            {
                health -= collision.gameObject.GetComponent<bullets>().dmgToHealth;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("end"))
        {
            Destroy(gameObject);
            wavesManager.totalEnemies--;
            if (IsKing)
                wavesManager.PlayerHealth += 5;
            else if (hasShield)
                wavesManager.PlayerHealth += 2;
            else
                wavesManager.PlayerHealth++;
        }

        if (collision.gameObject.CompareTag("bomb") && !insiderange)
        {
            insiderange = true;
            StartCoroutine(Cancel());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bomb") && insiderange)
        {
            insiderange = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!gettingfire)
        {
            StartCoroutine(Burning(other.transform.parent.GetChild(0).GetComponent<FlameThrower>().damage1, other.transform.parent.GetChild(0).GetComponent<FlameThrower>().damage2));
            gettingfire = true;
        }
    }

    IEnumerator Burning(int damagenumber, int damageshield)
    {
        if (shield > 0)
        {
            shield -= damageshield;
        }
        else
        {
            health -= damagenumber;
        }
        yield return new WaitForSeconds(0.05f);
        gettingfire = false;
    }

    public void booming(int bombdamage)
    {
        if (insiderange)
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

    IEnumerator Cancel()
    {
        yield return new WaitForSeconds(1.1f);
        if (insiderange)
            insiderange = false;
    }
}
