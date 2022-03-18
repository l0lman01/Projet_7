using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatManager : MonoBehaviour
{
    public static FallPlatManager Instance = null; //Singleton

    [SerializeField] GameObject FallingPlatform;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       
    }

    IEnumerator SpawnPlatform(Vector2 spawnPos)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(FallingPlatform, spawnPos, FallingPlatform.transform.rotation);
    }
}
