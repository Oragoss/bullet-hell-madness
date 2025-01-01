using Assets.Scripts.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        public static DifficultyManager difficultyManager;

        public enum Difficulty
        {
            Easy = 0,
            Normal = 1,
            Hard = 2
        }

        public Difficulty difficultyChosen;

        public void SelectDifficulty(int difficulty)
        {
            difficultyChosen = (Difficulty)difficulty;
        }
    }
}
