using Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] int shots = 1;
        [SerializeField] float fireRate = 2.5f;
        [SerializeField] BulletController bulletController;

        [SerializeField] PlayerControl playerControl;

        public int shotsFired;
        public int accuracy = 100;
        float fireRateCounter;
        AudioSource gunSound;

        private void Start()
        {
            if (!bulletController) bulletController = GetComponent<BulletController>();
            if (bulletController) bulletController.gun = this;
            gunSound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                if (playerControl.health > 0)
                {
                    if (Time.time >= fireRateCounter)
                    {
                        bulletController.Fire(shots);
                        fireRateCounter = Time.time + 1 / fireRate; //fireRate is how many bullets can be shot for every 1 second. Change the 1 to a 2 and it's the same number of shots in 2 seconds.
                        gunSound.Play();
                    }
                }
            }
        }
    }
}