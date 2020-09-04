using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float size;

        private bool canControl = true;
        private Trajectory trajectory;
        private bool dragBegan = false;
        private Vector2? nextPoint;
        private Animator animator;

        #region Event functions

        private void Start()
        {
            trajectory = GetComponent<Trajectory>();
            animator = GetComponent<Animator>();

            MoveToRandom();
        }

        private void Update()
        {
            if (GameManager.IsPaused)
                return;

            Move();
        }

        private void FixedUpdate()
        {
            List<Collider2D> hits = new List<Collider2D>();
            if (Physics2D.OverlapCircle(transform.position, size, new ContactFilter2D(), hits) > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject == gameObject)
                        continue;

                    if (hit.gameObject.CompareTag("Island"))
                    {
                        Debug.Log("Ship crashed");
                        Crash();
                    }
                    
                    Harbor harbor;
                    if ((harbor = hit.gameObject.GetComponent<Harbor>()) != null)
                    {
                        harbor.OnShipEntered(this);
                        Park();
                    }

                    Ship ship;
                    if ((ship = hit.gameObject.GetComponent<Ship>()) != null)
                    {
                        Debug.Log("Ships crashed");
                        Crash();
                        ship.Crash();
                    }

                    Whirlpool whirlpool;
                    if ((whirlpool = hit.gameObject.GetComponent<Whirlpool>()) != null)
                    {
                        Debug.Log("Ship sinked");
                        Crash();
                    }
                }
            }
        }

        private void OnMouseUp()
        {
            dragBegan = false;
        }

        private void OnMouseDrag()
        {
            if (!canControl)
                return;
            
            if (!dragBegan)
            {
                dragBegan = true;
                nextPoint = null;
                trajectory.Clear();
            }
            
            trajectory.Push(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        #endregion

        public void MoveToRandom()
        {
            trajectory = GetComponent<Trajectory>();
            trajectory.Push(transform.position);
            trajectory.Push(Utility.RandomPointInside());
        }
        
        public void Crash()
        {
            nextPoint = null;
            canControl = false;
            trajectory.Clear();
            animator.SetTrigger("Shipwreck");
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 3f);
        }

        private void Park()
        {
            nextPoint = null;
            canControl = false;
            trajectory.Clear();
            animator.SetTrigger("Park");
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 3f);
        }

        private void Move()
        {
            if (!nextPoint.HasValue)
            {
                nextPoint = trajectory.Peek();
                if (!nextPoint.HasValue)
                    return;
            }

            transform.position = Vector3.MoveTowards(transform.position, nextPoint.Value, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPoint.Value) < 0.01f)
            {
                trajectory.Pop();
                nextPoint = null;
            }
        }
    }
}