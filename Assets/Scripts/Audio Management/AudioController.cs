using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    [SerializeField] private AudioMixer _audioMixer;
    [Header("Master")]
    public Slider _masterSlider;
    [Header("BGM")]
    public Slider _bgmSlider;
    [Header("SFX")]
    public Slider _sfxSlider;

    public void Start () {
        if (PlayerPrefs.HasKey("masterVolume") && PlayerPrefs.HasKey("bgmVolume") && PlayerPrefs.HasKey("sfxVolume")) {
            LoadVolume();
        } else {
            MasterVolume();
            BgmVolume();
            SfxVolume();
        }
    }

    public void MasterVolume() {
        float volume = _masterSlider.value;
        _audioMixer.SetFloat("MasterParams", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void BgmVolume() {
        float volume = _bgmSlider.value;
        _audioMixer.SetFloat("BGMParams", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    public void SfxVolume() {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFXParams", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume() {
        _masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        _bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        MasterVolume();
        BgmVolume();
        SfxVolume();
    }
}
