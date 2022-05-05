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
    [SerializeField, BoxGroup("UI Sound")] private AudioSource UISource;
    [SerializeField, BoxGroup("SFX")] private List<audio> SFXAudios;
    [SerializeField, BoxGroup("BGM")] private List<audio> BGMAudios;
    [SerializeField, BoxGroup("UI Sound")] private List<audio> UIAudios;
    void Start()
    {
        playBGM("Orzo Room Music");
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

    [YarnCommand("Play_BGM")]
    public void playBGM(string clipName) {
        string newName = GameManager.substituteUnderscoreWithSpace(clipName);
        foreach (audio BGM in BGMAudios) {
            if (BGM.clipName.ToLower().Trim().Equals(newName.ToLower().Trim())) {
                BGMSource.clip = BGM.clip;
                BGMSource.loop = true;
                BGMSource.Play();
                return;
            }
        }
        Debug.LogWarning("cannot find audio clip with name " +newName);
    }

    public void playUISound(string clipName) {
        if (!UISource.isPlaying) {
            string newName = GameManager.substituteUnderscoreWithSpace(clipName);
            foreach (audio UIAudio in UIAudios) {
                if (UIAudio.clipName.ToLower().Trim().Equals(newName.ToLower().Trim())) {
                    UISource.clip = UIAudio.clip;
                    UISource.loop = false;
                    UISource.Play();
                    return;
                }
            }
        }
    }
}
