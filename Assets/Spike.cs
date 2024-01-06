using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public enum SpikeType { Active, Static }

    [SerializeField] private SpikeType spikeType;
    [SerializeField] float timeDamage = 10f;
    [SerializeField] int moveToActive;
    private SpriteRenderer _renderer;
    private Collider2D _col;
    private PlayerController playerController;

    private bool isChanging = false;
    private bool isTrapActive = true;
    private int moveCount = 0;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        playerController = FindObjectOfType<PlayerController>();

        PlayerController.onPlayerMove += SpikeOnOff;
    }
    private void OnDestroy() {
        PlayerController.onPlayerMove -= SpikeOnOff;
    }

    private void Update()
    {
        //if (playerController.isMoving && !isChanging && spikeType == SpikeType.Active)
        //{
        //    StartCoroutine(ToggleSpikesCoroutine());
        //}
    }

    IEnumerator ToggleSpikesCoroutine()
    {
        isChanging = true;

        yield return new WaitForSeconds(0.2f);

        if (moveCount % moveToActive == 0)
        {
            isTrapActive = true;
        }
        else
        {
            isTrapActive = false;
        }

        _col.enabled = isTrapActive;
        _renderer.enabled = isTrapActive;

        isChanging = false;
        moveCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            AudioManager.instance.PlayPlayerVoice("PlayerHurt");
            TimeManager.instance.timer -= timeDamage;
            Debug.Log("HIT SPIKE WOY");
        TimeManager.instance.CameraShakeDamage();

    }


    private void SpikeOnOff() {
        if(spikeType == SpikeType.Active) {
            StartCoroutine(SpikeOnOffIenum());
        }
    }

    IEnumerator SpikeOnOffIenum() {
        if (moveCount == 0) {
            _col.enabled = false;
            _renderer.color = Color.white;
            moveCount++;
            yield break;
        } else {
            yield return new WaitForSeconds(playerController.timeToMove);
            _col.enabled = true;
            _renderer.color = Color.red;
            moveCount = 0;
        }
    }
}
