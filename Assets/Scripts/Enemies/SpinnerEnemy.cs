using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerEnemy : MonoBehaviour
{
    [Header("Beam Settings")]
    public int beamAmount = 2;
    public float beamWidth;
    public float beamLength;
    public float rotSpeed;
    public Color colour;
    public float beamRotation;
    [Header("Beam Type")]
    public GameObject beamPrefab;
    public GameObject particleEffect;

    public bool beamHasDuration = false;

    public bool oneBeam;
    public bool reloadSpinner;

    [Header("HasDuration")]
    public float durationOfBeam = 5f;
    public float timeInbetweenBeams = 5f;
    public float curDuration;
    public float timerUntilBeam;
    public float beamLengthBetween;

    private void Start()
    {
        for (int i = 0; i < beamAmount; i++)
        {
            var obj =  Instantiate(beamPrefab, gameObject.transform);
            obj.transform.position = gameObject.transform.position;
            var diff = 360 / beamAmount;
            obj.transform.rotation = Quaternion.Euler(0, 0, diff * i);
        }
        reloadSpinner = true;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * (rotSpeed * Time.deltaTime));

        if (reloadSpinner)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < beamAmount; i++)
            {
                var obj = Instantiate(beamPrefab, gameObject.transform);
                obj.transform.position = gameObject.transform.position;
                if (oneBeam)
                {
                    transform.rotation = Quaternion.Euler(0, 0, beamRotation);
                }
                else
                {
                    var diff = 360 / beamAmount;
                    obj.transform.rotation = Quaternion.Euler(0, 0, diff * i);
                }
            }
            reloadSpinner = false;
        }
    }
}
