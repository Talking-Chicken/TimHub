using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using NaughtyAttributes;

[System.Serializable]
public struct audio {
    public string clipName;
    public AudioClip clip;
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
        string newName = GameManager.substituteUnderscoreWithSpace(clipName);
        foreach (audio SFX in SFXAudios) {
            if (SFX.clipName.ToLower().Trim().Equals(newName.ToLower().Trim())) {
                SFXSource.clip = SFX.clip;
                SFXSource.loop = false;
                SFXSource.Play();
                return;
            }
        }
        Debug.LogWarning("cannot find audio clip with name "+newName);
    }
}
