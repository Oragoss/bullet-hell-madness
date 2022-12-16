using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class LoadLevel : MonoBehaviour
    {
        public Animator animator;

        private int levelToLoad;

        public void FadeToLevel(int levelIndex)
        {
            animator.SetTrigger("FadeOut");
            levelToLoad = levelIndex;
            Invoke("OnFadeComplete", 0.95f);
        }

        public void OnFadeComplete()
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}