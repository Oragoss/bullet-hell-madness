using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager spawnManager;

        public float spawnWaveDelay = 0;
        public float waveRepeatRate = 2.5f;

        [Tooltip("Regular enemies to spawn."), SerializeField]
        private List<EnemyWave> wave = new List<EnemyWave>();

        [Tooltip("Special enemies that have more powerful attacks and stats."), SerializeField]
        private List<EnemyWave> specialWave = new List<EnemyWave>();

        [Tooltip("Waves that will spawn in player obstacles."), SerializeField]
        private List<EnemyWave> obstacleWave = new List<EnemyWave>();

        [SerializeField] private int nextSpecialSpawnTime;

        [Tooltip("The minimum number of seconds before a special is spawned."), SerializeField]
        private int timerMinRange = 25;
        [Tooltip("The maximum number of seconds before a special is spawned."), SerializeField]
        private int timerMaxRange = 120;

        private float timer;

        [Header("Testing Variables")]
        [SerializeField, Tooltip("Turns on the Spawn Manager's test mode which will allow you to test specific features. Make sure this is turned off for production.")]
        bool testMode;

        [SerializeField]
        int waveToTest;

        public void StopSpawnWaves()
        {
            CancelInvoke("SpawnWaves");
        }

        public void StartSpawnWaves()
        {
            InvokeRepeating("SpawnWaves", spawnWaveDelay, waveRepeatRate);
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
            StartSpawnWaves();
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            nextSpecialSpawnTime = random.Next(timerMinRange, timerMaxRange);
        }

        private void FixedUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= nextSpecialSpawnTime)
                SpawnSpecial();
        }

        private void SpawnWaves()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            int next;

            if (testMode)
            {
                next = waveToTest;
            }
            else
                next = random.Next(0, wave.Count);

            var currentWave = wave[next];

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
            nextSpecialSpawnTime = random.Next(timerMinRange, timerMaxRange);
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
            timer = 0;
        }
    }
}