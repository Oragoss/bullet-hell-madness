using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager difficultyManager;

        public enum Difficulty
        {
            Easy = 0,
            Normal = 1,
            Hard = 2
        }

        public Difficulty difficultyChosen = 0;

        public void SelectDifficulty(int difficulty)
        {
            SpawnManager.spawnManager.StopSpawnWaves();
            difficultyChosen = (Difficulty)difficulty;
            SpawnManager.spawnManager.StartSpawnWaves();
        }

        private void Awake()
        {
            if (difficultyManager == null)
            {
                DontDestroyOnLoad(gameObject);
                difficultyManager = this;
            }
            else if (difficultyManager != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
