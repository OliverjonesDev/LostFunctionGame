using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDeath : MonoBehaviour
{
    public ParticleSystem bloodParticles;
    public bool enemyDeath;
    public bool changeSprite;
    public bool breakCollision = true;
    public Sprite deadSprite;
    public Color bloodColor;
    public Color deadColor;
    private MonoBehaviour[] _scripts;
    public AnimatorOverrideController animationsOverride;
    public SpriteRenderer bloodAnim;
    public float timer;
    private AudioSource _audioSource;
    [Header("Key items only")]
    public bool respawn;
    private Vector2 orgPos;

    private void Start()
    {
        orgPos = (Vector2)transform.position;
        if (gameObject.layer == 14)
        {
            respawn = true;
        }
        _audioSource = GetComponent<AudioSource>();
        _scripts = gameObject.GetComponents<MonoBehaviour>();
        if (bloodAnim != null)
        {
            bloodAnim.enabled = false;
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<ParticleSystem>())
            {
                bloodParticles = child.gameObject.GetComponent<ParticleSystem>();
            }
            if (child.GetComponent<Animator>())
            {
                child.GetComponent<Animator>().enabled = false;
            }
        }

        bloodParticles.gameObject.SetActive(false);
    }


    public void FixedUpdate()
    {
        if (respawn && enemyDeath)
        {
            onEnemyDeath();
            for (int i = 0; i < 1; i++)
            {
                _audioSource.pitch = Random.Range(.9f, 1.1f);
                _audioSource.Play();
            }
            transform.position = orgPos;
            enemyDeath = false;
        }
        if (enemyDeath && !respawn)
        {
            onEnemyDeath();
            if (bloodAnim != null)
            {
                bloodAnim.enabled = true;
            }
            if (breakCollision)
            {
                if (GetComponent<Collider2D>())
                {
                    GetComponent<Collider2D>().enabled = false;
                }
            }
            if (changeSprite)
            {
                GetComponent<SpriteRenderer>().sprite = deadSprite;
            }
            foreach (Transform child in transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                {
                    child.GetComponent<SpriteRenderer>().color = deadColor;
                }
                if (child.GetComponent<Collider2D>())
                {
                    child.GetComponent<Collider2D>().enabled = false;
                }
                if (child.GetComponent<EnemyShoot>())
                {
                    child.GetComponent<EnemyShoot>().enabled = false;
                    child.GetComponent<LineRenderer>().enabled = false;
                }
                if (child.GetComponent<Animator>())
                {
                    child.GetComponent<Animator>().enabled = true;
                    child.GetComponent<Animator>().speed = Random.RandomRange(.01f, .2f);

                }
            }
            for (int i = 0; i < 1; i++)
            {
                _audioSource.pitch = Random.Range(.9f, 1.1f);
                _audioSource.Play();
            }
            foreach (MonoBehaviour script in _scripts)
            {
                script.enabled = false;
            }
            return;
        }
    }

    void onEnemyDeath()
    {
        if (!bloodParticles.isPlaying)
        {
            bloodParticles.gameObject.SetActive(true);
            bloodParticles.loop = false;
            bloodParticles.startColor = bloodColor;
            bloodParticles.Play();
            Debug.Log("hit");
        }
    }

}
