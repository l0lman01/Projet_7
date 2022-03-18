using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolShurikens : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    public float speed = 2f;

    int goalPoint = 0;

    private void FixedUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<DayNightController>().Day)
        {
            ShurikenNight();
        }
        
    }


    private void ShurikenNight()
    {
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * speed);
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
        }
    }
}
