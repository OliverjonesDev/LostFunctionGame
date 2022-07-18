using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : MonoBehaviour
{
    public AudioClip[] audioClipsSpeak;
    public AudioClip selectedClip;
    public AudioClip[] audioClipEmotive;
    public List<AudioClip> _selectedAudioClips;
    public AudioSource audioPlayer;
    public int audioLength = 4;
    private float _timer;
    public GameObject outline;
    private GameObject _player;
    private bool _canPlayAudio = true;
    private int _curClip;
    public GameObject[] interactableObject;
    public bool differentAudioOnInteract;
    private bool changed;

    private void Start()
    {
        _player = GameObject.Find("Player");
        outline.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
        SetupAudioForNPCs();
    }

    void OnInteractWithPlayer()
    {
        if (Vector2.Distance(_player.transform.position, gameObject.transform.position) <= 2)
        {
            if (differentAudioOnInteract)
            {
                if (!changed)
                {
                    _selectedAudioClips.Clear();
                    SetupAudioForNPCs();
                    changed = true;
                }
            }
            outline.SetActive(true);
            if (_canPlayAudio )
            {
                PlayAudio();
            }
            for (int i = 0; i < interactableObject.Length; i++)
            {
                if (interactableObject[i].GetComponent<Hidden>())
                {
                    interactableObject[i].SetActive(true);
                }
            }
        }
        else
        {
            audioPlayer.Pause();
            outline.SetActive(false);
            audioPlayer.clip = _selectedAudioClips[0];
            _timer = 0;
            _curClip = 0;
            _canPlayAudio = true;
            changed = false;
        }
    }
    private void FixedUpdate()
    {
        OnInteractWithPlayer();

    }
    void SetupAudioForNPCs()
    {
        audioLength = Random.Range(1, 4);
        for (int i = 0; i < audioLength; i++)
        {
            var index = Random.Range(0, audioClipsSpeak.Length);
            selectedClip = audioClipsSpeak[index];
            _selectedAudioClips.Add(selectedClip);
        }

        for (int i = 0; i < 1; i++)
        {
            var index = Random.Range(0, audioClipsSpeak.Length);
            selectedClip = audioClipEmotive[index];
            _selectedAudioClips.Add(selectedClip);
        }

        if (_selectedAudioClips.Count == audioLength + 1)
        {
            audioPlayer.clip = _selectedAudioClips[0];
        }
    }

    void PlayAudio()
    {
        _timer += Time.deltaTime;
        bool goNextClip;
        if (audioPlayer.clip == _selectedAudioClips[_selectedAudioClips.Count - 1])
        {
            if (_timer > audioPlayer.clip.length)
            {
                audioPlayer.Pause();
                _canPlayAudio = false;
            }
        }
        if (_canPlayAudio)
        {
            if (_timer > audioPlayer.clip.length)
            {
                audioPlayer.Pause();
                goNextClip = true;

            }
            else
            {
                goNextClip = false;
            }

            if (!audioPlayer.isPlaying)
            {
                audioPlayer.Play();
                if (goNextClip)
                {
                    goNextClip = false;
                    audioPlayer.clip = _selectedAudioClips[_curClip += 1];
                    _timer = 0;
                }
            }
        }
    }

}

