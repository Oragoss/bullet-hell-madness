using System.Collections;
using System.Collections.Generic;
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

        [Header("Special Enemy Behaviors")]
        [Tooltip("Places this special enemy will stop before continuing on its destination.")]
        public List<Vector2> stopPoints = new List<Vector2>();
        [Tooltip("How long the special enemy will spend at each stop before moving on.")]
        public int timeSpentAtStopPoints;
        public int numberOfAttacks;
    }
}