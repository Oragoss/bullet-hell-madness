using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class SpecialEnemyMovement : MonoBehaviour
    {
        [HideInInspector]
        public float lookDirection = 270;

        [HideInInspector]
        public Vector2 spawnLocation = new Vector2(-3, 0);

        [HideInInspector]
        public List<Vector2> stopPoints = new List<Vector2>();

        [HideInInspector]
        public int timeSpentAtStopPoints = 10;

        public bool stopAttacking;

        [SerializeField]
        List<SpecialEnemyAttack> weapons = new List<SpecialEnemyAttack>();

        [SerializeField]
        float speed = 0.4f;
        [SerializeField]
        float acceleration = 0.3f;

        private float timer;
        private int currentStopPoint;
        private bool isMoving;

        [Header("Layers colliders should ignore.")]
        [SerializeField]
        int enemyLayer = 6;
        [SerializeField]
        int deadEnemyLayer = 9;

        private void Awake()
        {
            if (stopPoints.Count == 0)
            {
                stopPoints.AddRange(new List<Vector2>
                {
                    new Vector2(0, 0),
                    new Vector2(5, 0)
                });
            }
        }

        void Start()
        {
            speed = speed / 10;
            acceleration = acceleration / 10;
            isMoving = true;
            Physics.IgnoreLayerCollision(enemyLayer, deadEnemyLayer);   //doesn't seem to be working
        }

        private void FixedUpdate()
        {
            if (!isMoving)
            {
                timer += Time.time;
                if (!stopAttacking)
                {
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        weapons[i].Fire();
                    }
                }
                else
                {
                    for (int i = 0; i < weapons.Count; i++)
                    {
                        weapons[i].StopFiring();
                    }
                }
            }

            if (timer >= (timeSpentAtStopPoints * 1000) && currentStopPoint < stopPoints.Count)
            {
                isMoving = true;
                for (int i = 0; i < weapons.Count; i++)
                {
                    weapons[i].StopFiring();
                }
            }

            if (transform.position.x == stopPoints[currentStopPoint].x && transform.position.y == stopPoints[currentStopPoint].y)
            {
                if (currentStopPoint < (stopPoints.Count - 1))
                    currentStopPoint++;

                timer = 0;
                isMoving = false;
            }

            Movement(stopPoints[currentStopPoint]);
        }

        private void Movement(Vector2 newSpot)
        {
            if (isMoving)
            {
                speed += acceleration;
                transform.eulerAngles = new Vector3(0, 0, lookDirection);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(newSpot.x, newSpot.y), speed * Time.deltaTime);
            }
        }
    }
}