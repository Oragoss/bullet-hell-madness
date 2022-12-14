using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager spawnManager;

        [SerializeField]
        List<EnemyWave> wave = new List<EnemyWave>();

        void Awake()
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
            InvokeRepeating("SpawnWaves", 0, 2.5f);
        }

        private void SpawnWaves()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            int next = random.Next(0, wave.Count);
            var currentWave = wave[next];

            for (var y = 0; y < currentWave.enemyBehavior.Count; y++)
            {
                var newEnemy = currentWave.enemyBehavior[y];
                var go = Instantiate(newEnemy.enemy, new Vector3(newEnemy.spawnPoint.x, newEnemy.spawnPoint.y, 0), Quaternion.identity);
                go.GetComponent<EnemyMovement>().destination = newEnemy.destination;
                go.GetComponent<EnemyMovement>().lookDirection = (float)newEnemy.lookDirection;
            }
        }
    }
}