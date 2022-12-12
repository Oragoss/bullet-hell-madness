using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        float speed = 3.5f;

        Rigidbody2D rigidbody;
        BoxCollider2D collider;

        private void Awake()
        {
            rigidbody = gameObject.GetComponent<Rigidbody2D>();
            collider = gameObject.GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            rigidbody.isKinematic = true;
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticallMovement = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(horizontalMovement * speed, verticallMovement * speed);

            movement *= Time.deltaTime * 100; //This makes movement smoother
            rigidbody.velocity = movement;
        }
    }

}