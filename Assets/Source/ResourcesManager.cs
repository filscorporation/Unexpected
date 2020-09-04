using System;
using UnityEngine;

namespace Source
{
    public class ResourcesManager : MonoBehaviour
    {
        public static ResourcesManager Instance;

        [SerializeField] public Sprite Line;
        [SerializeField] public Transform LineParent;

        private void Awake()
        {
            Instance = this;
        }
    }
}