using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyGunController : MonoBehaviour
    {
        [SerializeField] int shots = 1;
        [SerializeField] float fireRate = 1;
        [SerializeField] float delayBeforeFiring = 2.5f;
        [SerializeField] EnemyBulletController enemyBulletController;

        public int numberOfBullets = 3;
        public int accuracy = 100;
        AudioSource gunSound;

        public void StopFireSequence()
        {
            CancelInvoke("FireSequence");
        }

        private void Start()
        {
            gunSound = GetComponent<AudioSource>();

            if (!enemyBulletController) enemyBulletController = GetComponent<EnemyBulletController>();
            if (enemyBulletController) enemyBulletController.enemyGun = this;

            InvokeRepeating("FireSequence", delayBeforeFiring, fireRate);
        }

        private void FireSequence()
        {
            if (numberOfBullets > 0)
            {
                enemyBulletController.Fire(shots);
                gunSound.Play();
                numberOfBullets--;
            }
        }
    }
}