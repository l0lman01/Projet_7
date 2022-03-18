using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforme : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    public float speed = 2f;

    int goalPoint = 0;

    private void FixedUpdate()
    {
        PlatformRoute();
    }

    private void PlatformRoute()
    {
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * speed);
        if(Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    }



    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(platform.transform, true);      / Impossible de parenter player avec la plateforme
    } 

    void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }
    */
}
