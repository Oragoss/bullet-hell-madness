using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager spawnManager;

        [Header("Enemy Variables")]
        public float spawnWaveDelay = 0;
        public float waveRepeatRate = 2.5f;
        [Tooltip("How long to delay before spawning the next wave when the game is on \"Easy\" difficulty."), SerializeField]
        float easyDifficultyWaveModifier = 4;
        [Tooltip("How long to delay before spawning the next wave when the game is on \"Normal\" difficulty."), SerializeField]
        float normalDifficultyWaveModifier = 2;
        [Tooltip("How long to delay before spawning the next wave when the game is on \"Hard\" difficulty."), SerializeField]
        float hardDifficultyWaveModifier = 0;

        [Tooltip("Regular enemies to spawn."), SerializeField]
        private List<EnemyWave> wave = new List<EnemyWave>();

        [Header("Obstacle Variables")]
        [Tooltip("Waves that will spawn in player obstacles."), SerializeField]
        private List<EnemyWave> obstacleWave = new List<EnemyWave>();
        
        [SerializeField]
        private int nextObjectSpawnTime;
        
        [SerializeField]
        private bool spawnObstacleWaveInsteadOfEnemyWave;

        [Tooltip("The minimum number of seconds before an obstacle is spawned."), SerializeField]
        private int objectTimerMinRange = 5;
        [Tooltip("The maximum number of seconds before an obstacle is spawned."), SerializeField]
        private int objectTimerMaxRange = 300;

        private float obstacleTimer;

        [Header("Special Enemy Variables")]
        [Tooltip("Special enemies that have more powerful attacks and stats."), SerializeField]
        private List<EnemyWave> specialWave = new List<EnemyWave>();

        [SerializeField] 
        private int nextSpecialSpawnTime;

        [Tooltip("The minimum number of seconds before a special is spawned."), SerializeField]
        private int specialEnemyTimerMinRange = 25;
        [Tooltip("The maximum number of seconds before a special is spawned."), SerializeField]
        private int specialEnemyTimerMaxRange = 120;

        private float specialEnemyTimer;

        [Header("Testing Variables")]
        [SerializeField, Tooltip("Turns on the Spawn Manager's test mode which will allow you to test specific features. Make sure this is turned off for production.")]
        bool testMode;

        [SerializeField]
        int waveToTest;

        int difficultyLevel;

        public void StopSpawnWaves()
        {
            CancelInvoke("SpawnWaves");
        }

        public void StartSpawnWaves()
        {

            switch (difficultyLevel)
            {
                case (int)DifficultyManager.Difficulty.Easy:
                    InvokeRepeating("SpawnWaves", spawnWaveDelay + easyDifficultyWaveModifier, waveRepeatRate + easyDifficultyWaveModifier);
                    break;
                case (int)DifficultyManager.Difficulty.Normal:
                    InvokeRepeating("SpawnWaves", spawnWaveDelay + normalDifficultyWaveModifier, waveRepeatRate + normalDifficultyWaveModifier);
                    break;
                case (int)DifficultyManager.Difficulty.Hard:
                    InvokeRepeating("SpawnWaves", spawnWaveDelay + hardDifficultyWaveModifier, waveRepeatRate + hardDifficultyWaveModifier);
                    break;
            }
        }

        private void Awake()
        {
            if (spawnManager == null)
            {
                DontDestroyOnLoad(gameObject);
                spawnManager = this;
            }
            else if (spawnManager != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //Get difficulty setting
            difficultyLevel = (int)DifficultyManager.difficultyManager.difficultyChosen;

            //TODO: Change the spawning when in the title screen vs the game

            StartSpawnWaves();
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            nextSpecialSpawnTime = random.Next(specialEnemyTimerMinRange, specialEnemyTimerMaxRange);
            nextObjectSpawnTime = random.Next(objectTimerMinRange, objectTimerMaxRange);
        }

        private void FixedUpdate()
        {
            specialEnemyTimer += Time.deltaTime;
            if (specialEnemyTimer >= nextSpecialSpawnTime)
                SpawnSpecial();

            obstacleTimer += Time.deltaTime;
            if (obstacleTimer >= nextObjectSpawnTime)
            {
                var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
                nextObjectSpawnTime = random.Next(objectTimerMinRange, objectTimerMaxRange);
                obstacleTimer = 0;
                spawnObstacleWaveInsteadOfEnemyWave = true;
            }
        }

        private void SpawnWaves()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            int next;
            EnemyWave currentWave;

            if (testMode)
            {
                next = waveToTest;
                currentWave = wave[next];
            }
            else if (spawnObstacleWaveInsteadOfEnemyWave)
            {
                next = random.Next(0, obstacleWave.Count);
                currentWave = obstacleWave[next];
                spawnObstacleWaveInsteadOfEnemyWave = false;
            } 
            else
            {
                next = random.Next(0, wave.Count);
                currentWave = wave[next];
            }

            for (var y = 0; y < currentWave.enemyBehavior.Count; y++)
            {
                var newEnemy = currentWave.enemyBehavior[y];
                var go = Instantiate(newEnemy.enemy, new Vector3(newEnemy.spawnPoint.x, newEnemy.spawnPoint.y, 0), Quaternion.identity);
                go.GetComponent<EnemyMovement>().destination = newEnemy.destination;
                go.GetComponent<EnemyMovement>().lookDirection = (float)newEnemy.lookDirection;
            }
        }

        private void SpawnSpecial()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            nextSpecialSpawnTime = random.Next(specialEnemyTimerMinRange, specialEnemyTimerMaxRange);
            int next = random.Next(0, specialWave.Count);

            StopSpawnWaves();

            var currentWave = specialWave[next];

            for (var y = 0; y < currentWave.enemyBehavior.Count; y++)
            {
                var newEnemy = currentWave.enemyBehavior[y];
                var go = Instantiate(newEnemy.enemy, new Vector3(newEnemy.spawnPoint.x, newEnemy.spawnPoint.y, 0), Quaternion.identity);
                go.GetComponent<SpecialEnemyMovement>().stopPoints = newEnemy.stopPoints;
                go.GetComponent<SpecialEnemyMovement>().lookDirection = (float)newEnemy.lookDirection;
                go.GetComponent<SpecialEnemyMovement>().timeSpentAtStopPoints = newEnemy.timeSpentAtStopPoints;
            }

            StartSpawnWaves();
            specialEnemyTimer = 0;
        }
    }
}