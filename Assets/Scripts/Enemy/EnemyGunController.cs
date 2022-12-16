using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyGunController : MonoBehaviour
    {

        [SerializeField] int shots = 1;
        [SerializeField] float fireRate = 1;
        [SerializeField] float delayBeforeFiring = 0.5f;
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

        private void Update()
        {
            //if (Input.GetButton("Fire1"))
            //{
            //    if (Time.time >= fireRateCounter)
            //    {
            //        bulletController.Fire(shots);
            //        fireRateCounter = Time.time + 1 / fireRate; //fireRate is how many bullets can be shot for every 1 second. Change the 1 to a 2 and it's the same number of shots in 2 seconds.
            //    }
            //}
        }
    }
}