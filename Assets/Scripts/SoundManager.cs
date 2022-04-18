using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using NaughtyAttributes;

[System.Serializable]
public struct audio {
    string clipName;
    AudioClip clip;

    public audio(string clipName, AudioClip clip) {
        this.clipName = clipName;
        this.clip = clip;
    }
}

public class SoundManager : MonoBehaviour
{
    [SerializeField, BoxGroup("BGM")] private AudioSource BGMSource;
    [SerializeField, BoxGroup("SFX")] private AudioSource SFXSource;
    [SerializeField, BoxGroup("SFX")] private List<audio> SFXAudios;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    [YarnCommand("Play_SFX")]
    public void playSFX(string clipName) {
        
    }
}
