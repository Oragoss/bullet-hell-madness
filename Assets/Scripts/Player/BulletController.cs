using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BulletController : MonoBehaviour
    {
        [Tooltip("Speed of the bullet.")]
        [SerializeField]
        float speed = 15;

        public GunController gun;

        private ParticleSystem part;
        private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        private void Start()
        {
            part = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Instantiates the bullets
        /// </summary>
        /// <param name="shots">How many bullets are fired off at once</param>
        public void Fire(int shots)
        {
            gun.shotsFired++;
            part.Emit(shots);

            var partShape = part.shape;
            partShape.angle = (100 - gun.accuracy); //Prob don't need this

            var partM = part.main;
            partM.startSpeed = speed;
        }


        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit: " + other.gameObject);
            }
        }
    }
}