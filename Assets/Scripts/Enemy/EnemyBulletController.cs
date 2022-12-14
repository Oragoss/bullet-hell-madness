using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyBulletController : MonoBehaviour
    {
        [SerializeField, Tooltip("Speed of the bullet.")]
        float speed = 20;

        public EnemyGunController enemyGun;

        private ParticleSystem part;
        private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        private void Awake()
        {
            part = GetComponent<ParticleSystem>();
            var partM = part.main;
            partM.startSpeed = speed;
        }

        private void Start()
        {            
            var partShape = part.shape;
            partShape.scale = new Vector3(0.1f, 1, 1);

            var partM = part.main;
            partM.playOnAwake = false;
        }

        /// <summary>
        /// Instantiates the bullets
        /// </summary>
        /// <param name="shots">How many bullets are fired off at once</param>
        public void Fire(int shots)
        {
            part.Emit(shots);

            var partShape = part.shape;
            partShape.angle = (100 - enemyGun.accuracy);

            var partM = part.main;
            partM.startSpeed = speed;
        }


        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Hit: " + other.gameObject);
                //TODO: Damage player?
            }
        }
    }
}