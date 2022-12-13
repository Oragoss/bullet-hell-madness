﻿using System.Collections;
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
        float speed = 3.5f;

        Rigidbody2D rb;


        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            transform.eulerAngles = new Vector3(0, 0, lookDirection);
            rb.position = Vector2.MoveTowards(transform.position, new Vector2(destination.x, destination.y), speed * Time.deltaTime);
        }
    }
}