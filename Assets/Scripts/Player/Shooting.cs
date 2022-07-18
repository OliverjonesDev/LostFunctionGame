using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public bool canShoot = true;
    public float timer = 1;
    public float _elapsedTime;
    public LayerMask layersToIgnore;
    public LayerMask layersToHit;
    public GameObject gunObject;
    private bool objDestoryed;
    public int amountOfEnemiesDestroyed;
    public int maxEnemiesDestroyed = 4;
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Debug.Log(layersToHit.ToString());
        Debug.DrawLine(gunObject.transform.position, gunObject.GetComponent<Gun>().hit.point);
        if (!canShoot)
        {
            _elapsedTime += Time.fixedDeltaTime;
            if (_elapsedTime >= timer)
            {
                _elapsedTime = 0;
                canShoot = true;
                objDestoryed = false;
                amountOfEnemiesDestroyed = 0;
            }

        }
        if (_elapsedTime / 2 < timer / 2 && !canShoot)
        {
            if (gunObject.GetComponent<Gun>().hit.transform == null)
            {
                return;
            }
            if (gunObject.GetComponent<Gun>().hit.transform.gameObject.layer == 13 || gunObject.GetComponent<Gun>().hit.transform.gameObject.layer == 9 && amountOfEnemiesDestroyed < maxEnemiesDestroyed) //objDestoryed == false)
            {
                gunObject.GetComponent<Gun>().hit.transform.gameObject.GetComponent<EnemyDeath>().enemyDeath = true;
                objDestoryed = true;
                if (gunObject.GetComponent<Gun>().hit.transform.gameObject.layer == 9)
                {
                    amountOfEnemiesDestroyed++;
                }
            }
            if (gunObject.GetComponent<Gun>().hit.transform.gameObject.layer == 14)
            {
                gunObject.GetComponent<Gun>().hit.transform.gameObject.GetComponent<EnemyDeath>().enemyDeath = true;
            }
            // On a different layer we can have different strengths of enemies to determine to destory right away or to get their components and - HP
        }
    }

    void OnFire()
    {
        if (canShoot)
        {
            gunObject.GetComponent<AudioSource>().Play();
            canShoot = false;
        }
    }
}
