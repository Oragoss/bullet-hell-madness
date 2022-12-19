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

        public int accuracy = 100;

        public void StopFireSequence()
        {
            CancelInvoke("FireSequence");
        }

        private void Start()
        {
            if (!enemyBulletController) enemyBulletController = GetComponent<EnemyBulletController>();
            if (enemyBulletController) enemyBulletController.enemyGun = this;

            InvokeRepeating("FireSequence", delayBeforeFiring, fireRate);
        }

        private void FireSequence()
        {
            enemyBulletController.Fire(shots);
        }
    }
}