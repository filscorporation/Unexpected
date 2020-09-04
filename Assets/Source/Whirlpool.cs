using System;
using System.Collections;
using UnityEngine;

namespace Source
{
    public class Whirlpool : MonoBehaviour
    {
        private const float UNACTIVE_TIME = 2f;
        private const float LIFETIME = 8f;

        private void Start()
        {
            Destroy(gameObject, LIFETIME);
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            yield return new WaitForSeconds(UNACTIVE_TIME);

            GetComponent<Collider2D>().enabled = true;
        }
    }
}