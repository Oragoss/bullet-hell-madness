using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] int shots = 1;
        [SerializeField] float fireRate = 2.5f;
        [SerializeField] BulletController bulletController;

        public int shotsFired;
        public int accuracy = 100;
        float fireRateCounter;

        private void Start()
        {
            if (!bulletController) bulletController = GetComponent<BulletController>();
            if (bulletController) bulletController.gun = this;
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                if(Time.time >= fireRateCounter)
                {
                    bulletController.Fire(shots);
                    fireRateCounter = Time.time + 1 / fireRate; //fireRate is how many bullets can be shot for every 1 second. Change the 1 to a 2 and it's the same number of shots in 2 seconds.
                }
            }
        }
    }
}