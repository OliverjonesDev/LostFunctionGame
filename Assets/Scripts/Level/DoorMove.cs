using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public bool open;
    public Vector2 movementOfDoor;
    public float speed;
    public Vector2 orgPos;
    public AudioClip doorOpening;
    public AudioClip doorClosing;
    private AudioSource audioSource;
    private bool doorCurOpen;
    // Update is called once per frame\
    private void Start()
    {
        orgPos = (Vector2)transform.position;
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (open)
        {
            transform.position = Vector2.Lerp(transform.position, movementOfDoor + orgPos, speed * Time.deltaTime);
            audioSource.clip = doorOpening;
            if (!doorCurOpen)
            {
                playAudio();
                doorCurOpen = true;
            }
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, orgPos, speed * Time.deltaTime);
            audioSource.clip = doorClosing;
            if (doorCurOpen)
            {
                playAudio();
                doorCurOpen = false;
            }
        }
    }

    void playAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
