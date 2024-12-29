using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyCleanup : MonoBehaviour
    {
        [SerializeField] int delayInSecondsBeforeDestory = 6;

        private void Start()
        {
            Destroy(gameObject, delayInSecondsBeforeDestory);
        }
    }
}
