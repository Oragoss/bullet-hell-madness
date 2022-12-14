using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager uiManager;

        [Tooltip("This parent object holds all of the health blips. The images that count as the player's health (think hearts from Legend of Zelda).")]
        [SerializeField]
        GameObject Health;

        [SerializeField]
        GameObject gameOver;

        [SerializeField]
        GameObject score;

        List<GameObject> healthBlips = new List<GameObject>();

        public void SetHealth(int newHealth)
        {
            GetChildren();

            for (int i = 0; i < healthBlips.Count; i++)
            {
                healthBlips[i].SetActive(false);
            }

            for (int i = 0; i < newHealth; i++)
            {
                healthBlips[i].SetActive(true);
            }
        }

        public void DecreaseHealth()
        {
            int lastIndex = healthBlips.Count - 1;
            if (lastIndex > -1)
            {
                healthBlips[lastIndex].SetActive(false);
                healthBlips.Remove(healthBlips[lastIndex]);
            }
        }

        internal void SetScore(int newScore)
        {
            score.GetComponent<TextMeshProUGUI>().text = $"Score: {newScore}";
        }

        public void ShowGameOverUI()
        {
            gameOver.SetActive(true);
        }

        public void HideGameOverUI()
        {
            //TODO: Reset the gameover objects position so it can always be on?
            //gameOver.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            gameOver.SetActive(false);
        }

        private void Reset()
        {
            if (Health == null)
                Health = GameObject.Find("Health");
            else
                Debug.LogError("Could not find a 'Health' game object.");

            if (gameOver == null)
                gameOver = GameObject.Find("Game Over");
            else
                Debug.LogError("Could not find a 'Game Over' game object.");

            if (score == null)
                score = GameObject.Find("Score");
            else
                Debug.LogError("Could not find a 'Score' game object.");
        }

        private void Awake()
        {
            if (uiManager == null)
            {
                DontDestroyOnLoad(gameObject);
                uiManager = this;
            }
            else if (uiManager != this)
            {
                Destroy(gameObject);
            }

            HideGameOverUI();
        }

        private void Start()
        {
            GetChildren();
        }

        private void GetChildren()
        {
            if (healthBlips.Count > 0) healthBlips.Clear();

            var childCount = Health.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                healthBlips.Add(Health.transform.GetChild(i).gameObject);
            }
        }
    }
}