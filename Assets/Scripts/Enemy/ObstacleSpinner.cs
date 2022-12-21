using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class ObstacleSpinner : MonoBehaviour
    {
        [Range(1, 4)]
        [Tooltip("How much you want to slow down the spin of an obstacle. The higher the number the slower it will spin."), SerializeField]
        int amountToSlowDownSpin = 1;

        private int minSpinAmount = -1;
        private int maxSpinAmount = 1;

        private float spinAmount;

        private void Start()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            spinAmount = random.Next(minSpinAmount, maxSpinAmount);

            if (spinAmount == 0)
                spinAmount = 1;
        }


        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 0, spinAmount / amountToSlowDownSpin));
        }
    }
}