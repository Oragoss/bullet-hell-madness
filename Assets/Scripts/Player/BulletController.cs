using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BulletController : MonoBehaviour
    {
        [Tooltip("Speed of the bullet.")]
        [SerializeField]
        float speed;

        [SerializeField]
        GunController gun;

        private ParticleSystem part;
        private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        private void Awake()
        {
            part = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            Fire(100);
        }

        /// <summary>
        /// Instantiates the bullets
        /// </summary>
        /// <param name="shots">How many bullets are fired off at once</param>
        public void Fire(int shots)
        {
            part.Emit(shots);
        }
    }
}