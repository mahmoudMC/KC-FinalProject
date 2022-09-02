using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    float speed = 0.05f;
    public Transform theEnemy;
    public int dmgToHealth, dmgToShield;
    void Start()
    {
        
    }

    void Update()
    {
        if (theEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, theEnemy.position, speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
