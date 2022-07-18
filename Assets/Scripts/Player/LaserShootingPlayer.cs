using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserShootingPlayer : MonoBehaviour
{
    public bool canShoot = true;
    public float timer = 3;
    private float _elapsedTime;
    public LayerMask layersToIgnore;

    void onFire()
    {
        if (canShoot)
        {
            var _gunMousePos = Mouse.current.position.ReadValue();
            Vector3 target = Camera.main.ScreenToWorldPoint(_gunMousePos);
            var hit = Physics2D.Raycast(transform.position, target - transform.position, Mathf.Infinity, ~layersToIgnore);
        }
    }

    private void FixedUpdate()
    {
        if (!canShoot)
        {
            _elapsedTime += Time.fixedDeltaTime;
            if (_elapsedTime == timer)
            {
                _elapsedTime = 0;
                canShoot = true;
            }

        }
    }
}
