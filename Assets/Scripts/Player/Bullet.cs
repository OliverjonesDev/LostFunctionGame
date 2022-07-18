using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public int bulletSpeed = 4;
    public int[] layersToIgnore;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        for (int i = 0; i < layersToIgnore.Length; i++)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, layersToIgnore[i]);
        }
    }

    private void OnEnable()
    {
        _rb2d.velocity = transform.right * bulletSpeed;
    }

    private void OnDisable()
    {
        _rb2d.velocity = new Vector2 (0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < layersToIgnore.Length; i++)
        {
            if (collision.gameObject.layer == layersToIgnore[i])
            {
                Debug.Log("Non Bullet");
            }
            else
            {
                gameObject.SetActive(false);

            }
        }
    }

}
