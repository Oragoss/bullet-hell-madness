using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyBehavior", menuName = "ScriptableObjects/EnemyBehavior", order = 1)]
    public class EnemyBehavior : ScriptableObject
    {
        public GameObject enemy;
        public Vector2 spawnPoint;
        public Vector2 destination;

        public enum Direction
        {
            Up = 0,
            Down = 180,
            Left = 90,
            Right = 270,
            DiagonalRightTop = 315,
            DiagonalRightBottom = 225,
            DiagonalLeftTop = 45,
            DiagonalLeftBottom = 135
        };

        [Tooltip("Which direction do you want this enemy facing?")]
        public Direction lookDirection = Direction.Up;
    }
}