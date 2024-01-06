using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(updateGameState), 0.2f);
    }

    void updateGameState() {
        GameManager.instance.UpdateGameState(GameState.START);
    }

}
