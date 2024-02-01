using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 3f;
    public GameObject player;
    public PlayerTest playerTest;

    void Awake()
    {
        // playerTest = player.GetComponent<PlayerTest>();

    }
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        Debug.Log("in restart");

        playerTest.Respawn();
        gameHasEnded = false;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
