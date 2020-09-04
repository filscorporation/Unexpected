using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source
{
    public class Trajectory : MonoBehaviour
    {
        private const float MIN_DISTANCE = 0.6f;

        private int index = 1;
        
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
        
        public void Push(Vector2 point)
        {
            if (line.Count == 0)
            {
                line.Add(new Point(point, null));
                return;
            }

            Point last = line.Last();
            float dist = Vector2.Distance(point, last.position);
            float step = MIN_DISTANCE;

            while (dist > MIN_DISTANCE)
            {
                GameObject go = new GameObject("Point " + line.Count, typeof(SpriteRenderer));
                go.transform.SetParent(ResourcesManager.Instance.LineParent);
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = ResourcesManager.Instance.Line;
                Vector2 position = last.position + (point - last.position).normalized * step;
                go.transform.position = position;
                float angle = Mathf.Rad2Deg * Mathf.Atan2(
                    point.y - last.position.y, point.x - last.position.x);
                go.transform.eulerAngles = new Vector3(0, 0, angle);
                line.Add(new Point(position, sr));

                dist -= MIN_DISTANCE;
                step += MIN_DISTANCE;
            }
        }

        public Vector2? Peek()
        {
            if (!line.Any())
                return null;

            if (index >= line.Count)
            {
                return null;
            }

            return line[index].position;
        }

        public Vector2? Pop()
        {
            if (!line.Any())
                return null;

            if (index >= line.Count)
            {
                line.Clear();
                return null;
            }

            Vector2 result = line[index].position;
            if (line[index].sprite != null)
                Destroy(line[index].sprite.gameObject);
            index++;

            return result;
        }

        public void Clear()
        {
            foreach (Point point in line.Where(point => point.sprite != null))
            {
                Destroy(point.sprite.gameObject);
            }

            line.Clear();
            index = 1;
        }
    }
}