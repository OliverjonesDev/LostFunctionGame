using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float aimOffset;
    public GameObject target;
    public Vector3 targetPos;
    public float shootingDelay = 2.9f;
    public float timer;
    public Material lineMat;
    public LineRenderer _lineRenderer;
    public EnemyLook visionScript;
    public float _playerTickUpdateRate = 2;
    public float _playerPosUpdateTimer;
    public float distanceToPlayerCheck = 20;
    private AudioSource _audioSource;

    private void Start()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        visionScript = transform.parent.GetComponent<EnemyLook>();
        target = GameObject.Find("Player");
        _lineRenderer.material = lineMat;
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distanceToPlayerCheck)
        {
            if (visionScript.canPlayerSee)
            {
                _playerPosUpdateTimer += Time.deltaTime;
                if (targetPos == Vector3.zero)
                {
                    targetPos = target.transform.position;
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                    }
                }
                if (_playerPosUpdateTimer > _playerTickUpdateRate)
                {
                    targetPos = target.transform.position;
                    _playerPosUpdateTimer = 0;
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                    }
                }
                _lineRenderer.enabled = true;
                DrawLine();
                ShootAtPlayer();

            }
            else
            {
                _lineRenderer.enabled = false;
            }

        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    void ShootAtPlayer()
    {
        if (Vector3.Distance(target.transform.position, targetPos) < .5)
        {

            timer += Time.fixedDeltaTime;
            if (timer > .25f)
            {
                target.SetActive(false);
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }
    void DrawLine()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, targetPos);
        _lineRenderer.material.SetColor("_Color", new Color(255f, 0f, 0f, 0.3f));
        _lineRenderer.SetWidth(.075f, .075f);
        _lineRenderer.sortingOrder = 100;
    }

}
