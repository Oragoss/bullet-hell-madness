using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] int shots = 1;
        //[SerializeField] float fireRate;
        float fireRate;
        [SerializeField] float fireRateCounter;
        [SerializeField] BulletController bulletController;

        public int shotsFired;
        public int accuracy = 100;

        [SerializeField] float fireRatekCooldown = 0.5f;

        private void Start()
        {
            if (!bulletController) bulletController = GetComponentInChildren<BulletController>();
            if (bulletController) bulletController.gun = this;
        }

        private void Update()
        {
            fireRate += Time.deltaTime;
            if (Input.GetButton("Fire1") && fireRate > fireRatekCooldown)
            {
                fireRate = 0;
                bulletController.Fire(shots);
                //if(Time.time >= fireRateCounter)
                //{
                //    bulletController.Fire(shots);
                //    fireRateCounter = Time.time + 1 / fireRate; //fireRate is how many bullets can be shot for every 1 second. Change the 1 to a 2 and it's the same number of shots in 2 seconds.
                //}
            }
        }


    }
}