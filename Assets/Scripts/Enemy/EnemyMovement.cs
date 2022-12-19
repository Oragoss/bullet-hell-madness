using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    //[RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [HideInInspector]
        public float lookDirection = 0;
        
        [HideInInspector]
        public Vector2 spawnLocation;
        
        [HideInInspector]
        public Vector2 destination;

        [SerializeField]
        float speed = 1f;
        [SerializeField]
        float acceleration = 0.5f;

        [Header("Layers colliders should ignore.")]
        [SerializeField]
        int enemyLayer = 6;
        [SerializeField]
        int deadEnemyLayer = 9;

        private void Awake()
        {
            speed = speed / 10;
            acceleration = acceleration / 10;

            Physics.IgnoreLayerCollision(enemyLayer, deadEnemyLayer);   //doesn't seem to be working
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            speed += acceleration;
            transform.eulerAngles = new Vector3(0, 0, lookDirection);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(destination.x, destination.y), speed * Time.deltaTime);
        }
    }
}