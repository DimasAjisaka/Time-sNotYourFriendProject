using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public Sound[] 
        bgmAudio, 
        sfxAudio, 
        playerVoiceAudio,
        unlockLevelAudio,
        enviFeedbackAudio;
    public AudioSource 
        bgmSource, 
        sfxSource, 
        playerVoiceSource,
        unlockLevelSource,
        enviFeedbackSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(string name) {
        Sound bgm = Array.Find(bgmAudio, x => x.name == name);
        if (bgm == null) {
            Debug.Log("BGM Not Found!");
        } else {
            bgmSource.clip = bgm.clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound sfx = Array.Find(sfxAudio, x => x.name == name);
        if (sfx == null) {
            Debug.Log("SFX Not Found!");
        } else { sfxSource.PlayOneShot(sfx.clip); }
    }

    public void PlayPlayerVoice(string name) {
        Sound playerVoice = Array.Find(playerVoiceAudio, x => x.name == name);
        if (playerVoice == null) {
            Debug.Log("SFX Not Found!");
        } else { playerVoiceSource.PlayOneShot(playerVoice.clip); }
    }

    public void PlayUnlockLevel(string name) {
        Sound unlockLev = Array.Find(unlockLevelAudio, x => x.name == name);
        if (unlockLev == null) {
            Debug.Log("SFX Not Found!");
        } else { unlockLevelSource.PlayOneShot(unlockLev.clip); }
    }

    public void PlayEnviFeedback(string name) {
        Sound enviFeedback = Array.Find(enviFeedbackAudio, x => x.name == name);
        if (enviFeedback == null) {
            Debug.Log("SFX Not Found!");
        } else { playerVoiceSource.PlayOneShot(enviFeedback.clip); }
    }
}
