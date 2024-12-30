using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager musicManager;

        [Header("Intro Music Clips.")]
        [SerializeField] List<AudioClip> mainMenuAudioClips;
        [Header("In Game Music Clips.")]
        [SerializeField] List<AudioClip> gameAudioClips;
        [Header("Game Over Music Speaker.")]
        [SerializeField] GameObject gameOverSpeaker;

        AudioSource audioSource;

        bool isTransitionToGame;
        bool isTransitionToMainMenu;
        int timeBetweenTransitions = 2; //TODO: Put this magic number somewhere.. Otherwise make sure to change LoadLevel and the FadeOut animation when you want to change how long it takes to transition between scenes.

        private void Awake()
        {
            if (musicManager == null)
            {
                DontDestroyOnLoad(gameObject);
                musicManager = this;
            }
            else if (musicManager != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            SelectMainMenuBackgroundMusic();
        }

        private void FixedUpdate()
        {
            if(isTransitionToGame)
            {
                TransitionFromMainMenuToMainScene(timeBetweenTransitions);
                isTransitionToGame = false;
            }
            //TODO: Is this the best way to do this?
            else if (isTransitionToMainMenu)
            {
                TransitionFromMainSceneToMainMenu(0.5f);
                isTransitionToMainMenu = false;
            }
        }

        public void TransitionToGameMusic()
        {
            isTransitionToGame = true;
        }

        public void TransitionToMainMenuMusic()
        {
            isTransitionToMainMenu = true;
        }

        public void TransitionFromMainMenuToMainScene(float numberOfSecondsToTakeToTurnDownVolumeToZero)
        {
            audioSource.Stop();
            Invoke("SelectGameMusic", numberOfSecondsToTakeToTurnDownVolumeToZero);
        }

        public void TransitionFromMainSceneToMainMenu(float numberOfSecondsToTakeToTurnDownVolumeToZero)
        {
            audioSource.Stop();
            Invoke("SelectMainMenuBackgroundMusic", numberOfSecondsToTakeToTurnDownVolumeToZero);
        }

        public void PlayGameOverMusic()
        {
            //TODO: Set up some kind of explosion or failure sound to happen instanstly
            //TODO: Maybe select better game over music
            audioSource.Stop();
            Invoke("ActivateGameOverObject", 0.5f);
        }

        public void StopGameOverMusic()
        {

        }

        //Do to the nature of how the Game Over Menu appears, it will constantly make calls. This way we only activate the object instead of adding an audio clip every second.
        private void ActivateGameOverObject()
        {
            gameOverSpeaker.SetActive(true);
        }

        private void SelectMainMenuBackgroundMusic()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            int selectedAudioClip = random.Next(0, mainMenuAudioClips.Count);
            audioSource.clip = mainMenuAudioClips[selectedAudioClip];

            audioSource.loop = true;
            audioSource.Play();
        }

        public void SelectGameMusic()
        {
            var random = new System.Random(Mathf.Abs(Guid.NewGuid().GetHashCode()));
            int selectedAudioClip = random.Next(0, mainMenuAudioClips.Count);
            audioSource.clip = gameAudioClips[selectedAudioClip];

            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
