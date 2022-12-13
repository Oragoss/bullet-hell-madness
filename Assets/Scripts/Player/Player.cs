using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        float speed = 3.5f;

        Rigidbody2D rb;
        BoxCollider2D boxCollider;

        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
            HideCursor();
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
            rb.velocity = movement;
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ShowCursoer()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}