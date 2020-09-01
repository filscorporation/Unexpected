using System;
using UnityEngine;

namespace Source
{
    public class Ship : MonoBehaviour
    {
        private Trajectory trajectory;
        private bool dragBegan = false;

        private void Start()
        {
            trajectory = GetComponent<Trajectory>();
        }

        public void OnMouseUp()
        {
            dragBegan = false;
        }

        public void OnMouseDrag()
        {
            if (!dragBegan)
            {
                dragBegan = true;
                trajectory.Clear();
            }
            
            trajectory.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}