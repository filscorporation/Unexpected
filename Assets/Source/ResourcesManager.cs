using System;
using UnityEngine;

namespace Source
{
    public class ResourcesManager : MonoBehaviour
    {
        public static ResourcesManager Instance;

        [SerializeField] public Sprite Line;

        private void Awake()
        {
            Instance = this;
        }
    }
}