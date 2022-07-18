using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Player Basics
    private Rigidbody2D _rb2d;
    public Vector3 _player_MousePos;
    private Vector2 _player_XY_Movement;
    public float player_XY_MovementSpeed = 8;
    public int breakAccel = 6;
    public int rotationFactor = 4;
    public AudioSource _audioSource;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _rb2d.velocity = Vector2.Lerp(_rb2d.velocity, player_XY_MovementSpeed * _player_XY_Movement,
            Time.fixedDeltaTime * breakAccel);
        /*
        _player_MousePos = Mouse.current.position.ReadValue();
        Vector3 target = Camera.main.ScreenToWorldPoint(_player_MousePos);
        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg - 90);
        */

        if (_player_XY_Movement != Vector2.zero)
        {
            var newRot = Mathf.Atan2(_player_XY_Movement.y, _player_XY_Movement.x) * Mathf.Rad2Deg + 90;
            Quaternion rotQ = Quaternion.Euler(0, 0, newRot);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotQ, 12 * Time.deltaTime);
            if (!_audioSource.isPlaying)
            {
                _audioSource.pitch = Random.Range(.70f,1f);
                _audioSource.Play();
            }
            else
            {
            }
        }
    }

    void OnMove(InputValue value)
    {
        _player_XY_Movement = value.Get<Vector2>();
    }

}
