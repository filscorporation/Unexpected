using UnityEngine;

namespace Source
{
    public static class Utility
    {
        private const float DEFAULT_OFFSET = 2f;
        
        public static Vector2 RandomPointInside()
        {
            Vector2 min = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
        
        public static Vector2 RandomPointOutside()
        {
            bool xLocked = Random.Range(0, 2) == 1;
            bool flip = Random.Range(0, 2) == 1;
            Vector2 min = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            if (xLocked)
            {
                return new Vector2(flip ? min.x - DEFAULT_OFFSET : max.x + DEFAULT_OFFSET, Random.Range(min.y - DEFAULT_OFFSET, max.y + DEFAULT_OFFSET));
            }
            else
            {
                return new Vector2(Random.Range(min.x - DEFAULT_OFFSET, max.x + DEFAULT_OFFSET), flip ? min.y - DEFAULT_OFFSET : max.y + DEFAULT_OFFSET);
            }
        }
    }
}