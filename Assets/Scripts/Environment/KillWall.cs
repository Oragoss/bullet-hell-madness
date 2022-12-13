using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class KillWall : MonoBehaviour
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject != null)
                Destroy(collision.gameObject);
        }
    }
}