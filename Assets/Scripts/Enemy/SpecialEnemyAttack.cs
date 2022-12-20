using Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class SpecialEnemyAttack : MonoBehaviour
    {
        private ParticleSystem part;

        private void Start()
        {
            part = GetComponent<ParticleSystem>();

            var partM = part.main;
            partM.playOnAwake = false;
        }

        public void Fire()
        {
            part.Play();
        }

        public void StopFiring()
        {
            part.Stop();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerControl>().DamagePlayer();
            }
        }
    }
}