using Assets.Scripts.Enemy;
using Assets.Scripts.Managers;
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
        
        [Header("Layers colliders should ignore.")]
        [SerializeField]
        int playerWeaponLayer = 8;
        [SerializeField]
        int deadEnemyLayer = 9;

        private void Awake()
        {
            part = GetComponent<ParticleSystem>();
            var partM = part.main;
            partM.startSpeed = speed;
        }

        private void Start()
        {
            part = GetComponent<ParticleSystem>();
            var partShape = part.shape;
            partShape.scale = new Vector3(0.1f, 1, 1);

            Physics.IgnoreLayerCollision(playerWeaponLayer, deadEnemyLayer);
        }

        /// <summary>
        /// Instantiates the bullets
        /// </summary>
        /// <param name="shots">How many bullets are fired off at once</param>
        public void Fire(int shots)
        {
            var partShape = part.shape;
            partShape.angle = (100 - gun.accuracy); 

            var partM = part.main;
            partM.startSpeed = speed;

            gun.shotsFired++;
            part.Emit(shots);
        }


        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.gameObject.GetComponent<EnemyStats>();
                GameManager.gameManager.AddToScore(enemy.points);

                int childCount = other.gameObject.transform.childCount;
                for(int i = 0; i < childCount; i++)
                {
                    if (other.gameObject.transform.GetChild(i).transform.GetComponent<SpriteRenderer>())
                        other.gameObject.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().enabled = false;
                    else if (other.gameObject.transform.GetChild(i).transform.GetComponent<EnemyGunController>())
                        other.gameObject.transform.GetChild(i).transform.GetComponent<EnemyGunController>().StopFireSequence();
                }

                if (other.gameObject.GetComponent<SpecialEnemyMovement>())
                    other.gameObject.GetComponent<SpecialEnemyMovement>().stopAttacking = true;

                other.gameObject.tag = "DeadEnemy";
                other.gameObject.layer = 9; //Layer 9 is DeadEnemy
            }
        }
    }
}