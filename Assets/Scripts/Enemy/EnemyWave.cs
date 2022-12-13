using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 1)]
    public class EnemyWave : ScriptableObject
    {
        //[Tooltip("The desinations that will be determined for each wave. The number of destinations determines how many enemies will be spawned.")]
        //public List<Vector2> destinationLine = new List<Vector2>();
        //[Tooltip("Which type of enemies are going to be in this wave.")]
        //public List<GameObject> enemies = new List<GameObject>();
        //[Tooltip("Will add additional enemies per destinationLine.")]
        //public int additionalEnemies;

        public List<EnemyBehavior> enemyBehavior = new List<EnemyBehavior>();
    }
}