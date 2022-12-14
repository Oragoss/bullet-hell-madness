using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 1)]
    public class EnemyWave : ScriptableObject
    {
        public List<EnemyBehavior> enemyBehavior = new List<EnemyBehavior>();
    }
}