using UnityEngine;

namespace Source
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsPaused { get; private set; } = false;
    }
}