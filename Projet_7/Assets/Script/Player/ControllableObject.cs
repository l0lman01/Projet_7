using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControllableObject : MonoBehaviour
{
    public GameObject MovingObject;
    public GameObject ObjectAppeared;
    public GameObject Camera;
    
    private GameObject player;
    private bool isCollision;
    private bool isControllable;
    Transform MovingObjectTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ObjectAppeared.SetActive(false);
        isCollision = false;
        isControllable = false;
        MovingObjectTransform = MovingObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isCollision == true)
        {

            
            if (isControllable)
            {
                unControlObject();
                Camera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
            }
            else
            {
                ControlObject();
            }
            
            
        }
        if(isControllable == true)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, v).normalized;
            Vector3 offset = dir * Time.deltaTime * 2f;

            MovingObject.transform.position += offset;
            Camera.GetComponent<CinemachineVirtualCamera>().Follow = MovingObject.transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectAppeared.SetActive(true);
        isCollision = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ObjectAppeared.SetActive(false);
        isCollision = false;
    }

    private void ControlObject()
    {
        player.SetActive(false);
        isControllable = true;
    }
    private void unControlObject()
    {
        player.SetActive(true);
        isControllable = false;
    }
}
