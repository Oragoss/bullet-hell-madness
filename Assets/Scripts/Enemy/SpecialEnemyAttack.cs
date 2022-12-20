using Player;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class SpecialEnemyAttack : MonoBehaviour
    {
        private ParticleSystem part;

        private void Awake()
        {
            part = GetComponent<ParticleSystem>();

            var partM = part.main;
            partM.playOnAwake = false;

            part.Pause();   //I have no idea why but if you don't do this you cannot start firing particles AT ALL
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