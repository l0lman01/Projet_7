using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform Target;
    
    public float Speed = 2f;
    public float speed = 5;
    public int destroyDelay = 10;


    DayNightController DayNight;
    bool isDay;
    private void Start()
    {
        
            Destroy(GameObject.FindGameObjectWithTag("Bullet"), 3); 
       
    }
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>().Day)
        {
            BulletDay();
        }
        else
        {
            BulletNight();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsTouchingLayers(8))
            Destroy(GameObject.FindGameObjectWithTag("Bullet"));

        if (other.CompareTag("Player"))        
            other.GetComponent<PlayerController>().EnteredDeathZone();        
    }
    void BulletDay()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void BulletNight()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 dir = (Target.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;
        transform.up = dir;
    }    
}
