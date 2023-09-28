using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnableObjectsOnEnter : MonoBehaviour
{
    public GameObject[] hiddenGameObjects;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < hiddenGameObjects.Length; i++)
            {
                hiddenGameObjects[i].SetActive(true);
            }
            Debug.Log("Collision Entered");   
        }
    }
    
}
