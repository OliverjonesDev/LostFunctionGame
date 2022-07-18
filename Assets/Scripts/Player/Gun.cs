using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Vector3 _gunMousePos;
    private LineRenderer _lineRenderer;
    public Material lineMat;
    public Shooting shootingScript;
    public LayerMask layersToIgnore;
    private float minSizeLine = .1f;
    private float maxSizeLine = .2f;
    public RaycastHit2D hit;
    private AudioSource _audioSource;
    private float laserLength = 40f;
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _lineRenderer = gameObject.AddComponent<LineRenderer>().GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _lineRenderer.generateLightingData = true;
    }

    private void Update()
    {
        _gunMousePos = Mouse.current.position.ReadValue();
        Vector3 target = Camera.main.ScreenToWorldPoint(_gunMousePos);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg);
        hit = Physics2D.Raycast(transform.position, target - transform.position,Mathf.Infinity, ~layersToIgnore);


        //Line render
        _lineRenderer.positionCount = 2;
        if (hit.transform != null)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, hit.point);
        }
        _lineRenderer.numCapVertices = 4;

        //Size To set
        if (!shootingScript.canShoot)
        {
            float curLineSize = Mathf.Lerp(maxSizeLine, minSizeLine, 1.5f * Time.deltaTime);
            _lineRenderer.SetWidth(curLineSize, curLineSize);
        }
        else
        {
            float curLineSize = Mathf.Lerp(minSizeLine, maxSizeLine, 1.5f * Time.deltaTime);
            _lineRenderer.SetWidth(curLineSize, curLineSize);
        }
        minSizeLine = Random.Range(.025f, .075f);
        _lineRenderer.SetColors(Color.red, Color.magenta);
        _lineRenderer.material = lineMat;
        _lineRenderer.sortingOrder = 100;
        Debug.DrawLine(transform.position, hit.point);

    }
}
