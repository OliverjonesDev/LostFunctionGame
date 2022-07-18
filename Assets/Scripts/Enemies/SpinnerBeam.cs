using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBeam : MonoBehaviour
{
    private SpinnerEnemy parent;
    private LineRenderer _lineRenderer;
    private RaycastHit2D hit;
    public GameObject vfx;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        parent = transform.parent.gameObject.GetComponent<SpinnerEnemy>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startColor = parent.colour;
        _lineRenderer.endColor = parent.colour;
        _lineRenderer.numCapVertices = 4;
        vfx = Instantiate(parent.particleEffect);
        vfx.GetComponent<ParticleSystem>().startColor = parent.colour;
        _lineRenderer.generateLightingData = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.beamHasDuration)
        {
            if (parent.curDuration > parent.durationOfBeam)
            {
                parent.timerUntilBeam += Time.deltaTime;
                if (parent.timerUntilBeam >= parent.beamLengthBetween)
                {
                    parent.curDuration = 0;
                    parent.timerUntilBeam = 0;
                }
            }

            if (parent.curDuration < parent.durationOfBeam && parent.timerUntilBeam <= .5)
            {
                _lineRenderer.enabled = true;
                parent.curDuration += Time.deltaTime;
                _lineRenderer.SetWidth(parent.beamWidth, parent.beamWidth);
                _lineRenderer.SetPosition(0, transform.position);
                hit = Physics2D.Raycast(transform.position, transform.right, distance: parent.beamLength);
                Debug.DrawRay(transform.position, transform.right * parent.beamLength);
                vfx.transform.position = hit.point;
                if (hit.transform == null)
                {
                    _lineRenderer.SetPosition(1, transform.position + transform.right * parent.beamLength);
                    vfx.SetActive(false);
                }
                else
                {
                    _lineRenderer.SetPosition(1, hit.point);
                    if (!vfx.GetComponent<ParticleSystem>().isPlaying)
                    {
                        vfx.SetActive(true);
                        vfx.GetComponent<ParticleSystem>().Play();
                    }
                    if (hit.transform.gameObject == player)
                    {
                        player.SetActive(false);
                        Debug.Log("HIT PLAYER");
                    }
                }
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
        else
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetWidth(parent.beamWidth, parent.beamWidth);
            _lineRenderer.SetPosition(0, transform.position);
            hit = Physics2D.Raycast(transform.position, transform.right, distance: parent.beamLength);
            Debug.DrawRay(transform.position, transform.right * parent.beamLength);
            vfx.transform.position = hit.point;
            if (hit.transform == null)
            {
                _lineRenderer.SetPosition(1, transform.position + transform.right * parent.beamLength);
                vfx.SetActive(false);
            }
            else
            {
                _lineRenderer.SetPosition(1, hit.point);
                if (!vfx.GetComponent<ParticleSystem>().isPlaying)
                {
                    vfx.SetActive(true);
                    vfx.GetComponent<ParticleSystem>().Play();
                }
                if (hit.transform.gameObject == player)
                {
                    player.SetActive(false);
                    Debug.Log("HIT PLAYER");
                }
            }
        }
    }
}
