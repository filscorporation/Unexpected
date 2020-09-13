using System;
using UnityEngine;

namespace Source
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float sceneWidth = 14F;

        public void Update()
        {
            Scale();
        }

        private void Scale()
        {
            float ratio = Screen.width / (float)Screen.height;

            Camera.main.orthographicSize = sceneWidth / ratio;
        }
    }
}