﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionNextScene : MonoBehaviour
{
    public string nextScene;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
