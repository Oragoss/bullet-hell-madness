using Assets.Scripts.Managers;
using UnityEngine;

namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]
        int health = 5;

        [SerializeField]
        float speed = 4f;

        Rigidbody2D rb;

        public void SetPlayerHealth(int newHealth)
        {
            health = newHealth;
        }

        public void DamagePlayer()
        {
            health--;
            UIManager.uiManager.DecreaseHealth();
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            HideCursor();
        }

        private void FixedUpdate()
        {
            if(health > 0)
            {
                Movement();
            } 
            else
            {
                GameManager.gameManager.GameOver();
                rb.velocity = new Vector2(0, 0);
                DeathAnimation();
            }
        }

        private void Movement()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticallMovement = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(horizontalMovement * speed, verticallMovement * speed);

            movement *= Time.deltaTime * 100; //This makes movement smoother
            rb.velocity = movement;

            //Super important. This will make the player face in the direction of the inputs. It also normalizes them so you can go diagonally.
            //if (horizontalMovement != 0 || verticallMovement != 0)
            //    transform.up = rb.velocity.normalized;

            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            transform.up = dir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject.gameObject);
                DamagePlayer();
            }
        }

        private void DeathAnimation()
        {
            transform.Rotate(new Vector3(0, 0, 5));
        }
    }

}