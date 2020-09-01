using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source
{
    public class Trajectory : MonoBehaviour
    {
        private static float minDistance = 0.4f;
        
        private struct Point
        {
            public Point(Vector2 position, SpriteRenderer sprite)
            {
                this.position = position;
                this.sprite = sprite;
            }

            public Vector2 position;
            public SpriteRenderer sprite;
        }
        private List<Point> line = new List<Point>();
        
        public void Add(Vector2 point)
        {
            if (line.Count == 0)
            {
                line.Add(new Point(point, null));
                return;
            }

            if (Vector2.Distance(point, line.Last().position) < minDistance)
                return;
            
            GameObject go = new GameObject("Point " + line.Count, typeof(SpriteRenderer));
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.sprite = ResourcesManager.Instance.Line;
            go.transform.position = (line.Last().position + point) / 2;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(
                point.y - line.Last().position.y, point.x - line.Last().position.x);
            go.transform.eulerAngles = new Vector3(0, 0, angle);
            line.Add(new Point(point, sr));
        }

        public void Clear()
        {
            foreach (Point point in line.Where(point => point.sprite != null))
            {
                Destroy(point.sprite.gameObject);
            }

            line.Clear();
        }
    }
}