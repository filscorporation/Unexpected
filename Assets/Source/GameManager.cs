using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Source
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [SerializeField] private Text shipsText;
        [SerializeField] private Text shipwrecksText;
        [SerializeField] private GameObject restartScreen;
        [SerializeField] private GameObject helpScreen;
        
        public static bool IsPaused { get; private set; } = false;
        private int ships = 0;
        private int shipwreksLeft;
        private int shipwreksMax = 3;

        private void Start()
        {
            Instance = this;
            shipwreksLeft = shipwreksMax;
        }

        private void Update()
        {
            shipsText.text = ships.ToString();
            shipwrecksText.text = $"{shipwreksLeft}/{shipwreksMax}";
        }

        public void AddShip()
        {
            ships++;
        }

        public void AddShipwreck()
        {
            shipwreksLeft--;
            if (shipwreksLeft <= 0)
            {
                IsPaused = true;
                Debug.Log("Lost");
                ShowRestartScreen();
            }
        }

        private void ShowRestartScreen()
        {
            restartScreen.SetActive(true);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Help()
        {
            helpScreen.SetActive(!helpScreen.activeSelf);
        }
    }
}