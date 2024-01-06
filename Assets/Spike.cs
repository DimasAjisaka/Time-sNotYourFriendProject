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
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _col = GetComponent<Collider2D>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (playerController.isMoving && !isChanging && spikeType == SpikeType.Active)
        {
            StartCoroutine(ToggleSpikesCoroutine());
        }
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
        if (isTrapActive)
        {
            TimeManager.instance.timer -= timeDamage;
            Debug.Log("HIT SPIKE WOY");
        }
    }
}
