using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
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

        Rigidbody2D rb;


        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            speed = speed / 10;
            acceleration = acceleration / 10;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            speed += acceleration;
            transform.eulerAngles = new Vector3(0, 0, lookDirection);
            rb.position = Vector2.MoveTowards(transform.position, new Vector2(destination.x, destination.y), speed * Time.deltaTime);
        }
    }
}