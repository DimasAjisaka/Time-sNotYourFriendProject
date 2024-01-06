using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class TimeManager : MonoBehaviour {
    public static TimeManager instance;
    public float timer;
    public float startShaking = 10f;
    private Camera cam;

    private bool isShaking = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        if (GameManager.instance.state == GameState.START || GameManager.instance.state == GameState.BATTLE) {
            timer -= Time.deltaTime;

            if (timer <= startShaking) {
                CameraShake();
                if (isShaking == false) {
                    StartCoroutine(StartHeartBeat());
                }
            } 

            if (timer <= 0) {
                GameManager.instance.UpdateGameState(GameState.GAMEOVER);
                AudioManager.instance.PlayPlayerVoice("PlayerDeath");
            }
        }
    }

    IEnumerator StartHeartBeat() {
        isShaking = true;
        AudioManager.instance.PlayHeartBeat("Slow");
        while (timer > 5) {
            AudioManager.instance.PlayHeartBeat("Slow");
            yield return new WaitForSeconds(1f);
        }
        AudioManager.instance.PlayHeartBeat("Fast");
        yield return new WaitForSeconds(5f);
        isShaking = false;
    }


    private void CameraShake() {
        // Parameters for the shake
        float duration = 0.5f;
        float strength = 0.1f;
        int vibrato = 1;
        float randomness = 0;

        // Trigger camera shake using DOTween
        cam.DOShakePosition(duration, strength, vibrato, randomness);
    }

    public void CameraShakeDamage() {
        // Parameters for the shake
        float duration = 0.3f;
        float strength = 0.3f;
        int vibrato = 10;
        float randomness = 0;

        // Trigger camera shake using DOTween
        cam.DOShakePosition(duration, strength, vibrato, randomness);
    }

}
