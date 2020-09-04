using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] musicSource;

    public AudioClip menuClip;
    public AudioClip musicIG;
    public AudioClip musicHardIG;
    public AudioClip musicCredit;

    public float volume = 0.5f;
    private int _currentAudio;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource[_currentAudio].volume < volume)
        {
            if (_currentAudio == 0)
            {
                musicSource[1].volume -= Time.deltaTime;
            }
            else
            {
                musicSource[0].volume -= Time.deltaTime;
            }
            musicSource[_currentAudio].volume += Time.deltaTime;
        }
        else
        {
            musicSource[_currentAudio].volume = volume;
        }
    }

    public void PlayMusic(AudioClip Music, bool Loop = true, ulong Time = 0)
    {
        if (musicSource[_currentAudio].clip == Music)
        {
            return;
        }
        if (_currentAudio == 0)
        {
            _currentAudio = 1;
            musicSource[1].clip = Music;
            musicSource[1].loop = Loop;
            musicSource[1].Play(Time);
        }
        else
        {
            _currentAudio = 0;
            musicSource[0].clip = Music;
            musicSource[0].loop = Loop;
            musicSource[0].Play(Time);
        }
    }
    public void StopMusic()
    {
        musicSource[_currentAudio].Pause();
    }
    public void PlayMusic()
    {
        musicSource[_currentAudio].Play();
    }

    public void PlayDangerMusic()
    {
        PlayMusic(musicHardIG);
    }
    public void PlayNormalMusic()
    {
        PlayMusic(musicIG);
    }

    public void PlayCreditMusic()
    {
        PlayMusic(musicCredit);
    }
}
