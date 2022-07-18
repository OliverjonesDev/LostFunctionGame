using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenShake : MonoBehaviour
{
    public Shooting shootingClass;
    public Vector3 orgPos;
    public GameObject player;
    public float followSpeed;
    [Range(0,.1f)]
    public float screenShakeAmount = .025f;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void FixedUpdate()
    {
        orgPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Vector3 randomPos = new Vector3 (transform.position.x + Random.Range(-1 * screenShakeAmount, screenShakeAmount), transform.position.y + Random.Range(-1 * screenShakeAmount, screenShakeAmount), transform.position.z);
        if (!shootingClass.canShoot)
        {
            transform.position = randomPos;
            transform.position = Vector3.Lerp(transform.position, orgPos, 1.5f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, orgPos, followSpeed * Time.deltaTime);
        }
    }
}
