using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager uiManager;

        [Tooltip("This parent object holds all of the health blips. The images that count as the player's health (think hearts from Legend of Zelda).")]
        [SerializeField]
        GameObject Health;

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
            healthBlips[lastIndex].SetActive(false);
            healthBlips.Remove(healthBlips[lastIndex]);
        }

        private void Reset()
        {
            if (Health == null)
                Health = GameObject.Find("Health");
            else
                Debug.LogError("Could not find a 'Health' game object.");
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