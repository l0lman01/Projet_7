
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapRun : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform groundDetection;

    private bool _movingRight = true;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D turnSide = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        
        if(turnSide.collider == false)
        {
            if(_movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _movingRight = true;
            }
        }
    }
}
