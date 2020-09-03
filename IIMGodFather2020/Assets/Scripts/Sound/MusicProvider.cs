using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicProvider : MonoBehaviour
{
    public AudioClip musicToPlay;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(musicToPlay);
    }
}
