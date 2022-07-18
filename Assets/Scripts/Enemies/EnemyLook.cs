using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    public GameObject player;
    public float rotSpeed = 2;
    public Quaternion orgRot;
    public LayerMask layerMask;
    public bool canPlayerSee;
    public float angleOffset;

    private void Start()
    {
        orgRot = transform.rotation;
        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = null;
        }
    }
    private void FixedUpdate()
    {
        if (player.active)
        {
            float _angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg - angleOffset;
            var _angleQ = Quaternion.Euler(0, 0, _angle);
            var canSeePlayer = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, ~layerMask);

            Debug.DrawLine(transform.position, canSeePlayer.point, Color.red);
            //This will make the world feel more alive if the enemies do not have any gun scripts and are passive or standard robots,
            //Causes them to look at the players
            if (canSeePlayer.transform.gameObject.name == "Player")
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _angleQ, rotSpeed * Time.fixedDeltaTime);
                //Debug.DrawLine(transform.position, player.transform.position, Color.red);
                canPlayerSee = true;
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, orgRot, rotSpeed * Time.fixedDeltaTime);
                canPlayerSee = false;
            }
        }
    }
}
