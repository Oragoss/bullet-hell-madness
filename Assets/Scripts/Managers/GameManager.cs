using Player;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager;

        [SerializeField]
        GameObject AIManager;

        [SerializeField]
        GameObject Player;

        [SerializeField]
        int playerHealth = 5;

        [SerializeField]
        Vector2 playerStartPosition;

        public void GameOver()
        {
            UIManager.uiManager.ShowGameOverUI();
            AIManager.gameObject.GetComponent<SpawnManager>().StopSpawnWaves();
            Player.GetComponent<PlayerControl>().ShowCursor();
        }

        public void Restart()
        {
            UIManager.uiManager.HideGameOverUI();
            Player.GetComponent<PlayerControl>().SetPlayerHealth(playerHealth);
            UIManager.uiManager.SetHealth(playerHealth);
            Player.GetComponent<PlayerControl>().HideCursor();
            Player.transform.position = playerStartPosition;
            Player.transform.eulerAngles = new Vector3(0, 0, 0);

            AIManager.gameObject.GetComponent<SpawnManager>().StartSpawnWaves();
        }

        private void Reset()
        {
            if (AIManager == null)
                AIManager = GameObject.Find("AI Manager");
            else
                Debug.LogError("Could not find a 'AI Manager' game object.");

            if (Player == null)
                Player = GameObject.FindGameObjectWithTag("Player");
            else
                Debug.LogError("Could not find a 'Player' game object.");
        }

        private void Awake()
        {
            if (gameManager == null)
            {
                DontDestroyOnLoad(gameObject);
                gameManager = this;
            }
            else if (gameManager != this)
            {
                Destroy(gameObject);
            }
        }
    }
}