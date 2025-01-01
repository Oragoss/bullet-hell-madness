using Player;
using System;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Managers
{
    public class Quit : MonoBehaviour
    {
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
