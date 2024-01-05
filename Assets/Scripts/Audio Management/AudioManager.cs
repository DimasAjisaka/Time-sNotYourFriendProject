using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public Sound[] bgmAudio, sfxAudio;
    public AudioSource bgmSource, sfxSource;

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
}
