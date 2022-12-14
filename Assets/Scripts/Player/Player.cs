using Assets.Scripts.Managers;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        int health = 5;

        [SerializeField]
        float speed = 4f;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject.gameObject);
                DamagePlayer();
                Debug.Log($"PlayerHealth: {health}");
            }
        }

        public void DamagePlayer()
        {
            health--;
            UIManager.uiManager.DecreaseHealth();
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