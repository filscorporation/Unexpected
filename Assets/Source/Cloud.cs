using System;
using UnityEngine;

namespace Source
{
    public class Cloud : MonoBehaviour
    {
        [SerializeField] private float speed = 0.3f;
        
        private Vector2 target = Vector2.zero;
        
        private void Start()
        {
            target = -1 * transform.position;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Vector2.Distance(target, transform.position) < Mathf.Epsilon)
                Destroy(gameObject);
        }
    }
}