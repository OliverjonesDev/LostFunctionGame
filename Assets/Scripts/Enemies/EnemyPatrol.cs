using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int destPoint = 1;
    public float speed = 4;
    public bool increment = true;
    private EnemyLook canSeePlayer;
    private EnemyShoot enemyShoot;
    private GameObject _player;
    public float distanceToNextPoint;
    public float angleOffset = 90;
    private void Start()
    {
        _player = GameObject.Find("Player");
        canSeePlayer = GetComponent<EnemyLook>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<EnemyShoot>())
            {
                enemyShoot = child.GetComponent<EnemyShoot>();
            }
        }
    }
    void GoToNextPoint()
    {
        if (increment)
        {
            destPoint++;
        }
        else
        {
            destPoint--;
        }
        if (destPoint == patrolPoints.Length)
        {
            destPoint = 0;
            increment = true;
        }
        else
        {
            increment = true;
        }
        if (destPoint == 0)
        {
            increment = true;
        }

    }

    private void Update()
    {

        if (patrolPoints.Length == 0)
        {
            return;
        }
        if (Vector3.Distance(transform.position, patrolPoints[destPoint].position) < 1f)
        {
            GoToNextPoint();
        }
        if (GetComponent<EnemyShoot>())
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > enemyShoot.distanceToPlayerCheck || !canSeePlayer.canPlayerSee)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[destPoint].position, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                var newRot = Mathf.Atan2(transform.position.y - patrolPoints[destPoint].position.y, transform.position.x - patrolPoints[destPoint].position.x) * Mathf.Rad2Deg + angleOffset;
                Quaternion rotQ = Quaternion.Euler(0, 0, newRot);
                transform.rotation = rotQ;
            }
        }
        else if (GetComponent<EnemyLook>())
        {
            if (!canSeePlayer.canPlayerSee)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[destPoint].position, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                var newRot = Mathf.Atan2(transform.position.y - patrolPoints[destPoint].position.y, transform.position.x - patrolPoints[destPoint].position.x) * Mathf.Rad2Deg + angleOffset;
                Quaternion rotQ = Quaternion.Euler(0, 0, newRot);
                transform.rotation = rotQ;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[destPoint].position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            var newRot = Mathf.Atan2(transform.position.y - patrolPoints[destPoint].position.y, transform.position.x - patrolPoints[destPoint].position.x) * Mathf.Rad2Deg + angleOffset;
            Quaternion rotQ = Quaternion.Euler(0, 0, newRot);
            transform.rotation = rotQ;
        }

    }


}
