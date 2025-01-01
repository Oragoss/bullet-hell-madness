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
        int playerScore = 0;

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
            //TODO: What if we didn't do this but instead chose something to be the game over music??
            //Then whenever the player dies something can be set active and play the music on awake. 
            //Then when the game is reset we deactivate the game over music object...
            //MusicManager.musicManager.TransitionToMainMenuMusic();
            MusicManager.musicManager.PlayGameOverMusic();
        }

        public void Restart()
        {
            MusicManager.musicManager.StopGameOverMusic();
            MusicManager.musicManager.SelectGameMusic();
            playerScore = 0;
            UIManager.uiManager.HideGameOverUI();
            Player.GetComponent<PlayerControl>().SetPlayerHealth(playerHealth);
            UIManager.uiManager.SetHealth(playerHealth);
            UIManager.uiManager.SetScore(0);
            Player.GetComponent<PlayerControl>().HideCursor();
            Player.transform.position = playerStartPosition;
            Player.transform.eulerAngles = new Vector3(0, 0, 0);

            AIManager.gameObject.GetComponent<SpawnManager>().StartSpawnWaves();
        }

        public void AddToScore(int points)
        {
            playerScore += points;
            UIManager.uiManager.SetScore(playerScore);
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