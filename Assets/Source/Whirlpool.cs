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
            StartCoroutine(Finish());
            StartCoroutine(Activate());
        }

        private void Update()
        {
            if (GameManager.IsPaused)
            {
                GetComponent<Animator>()?.StopPlayback();
            }
        }

        private IEnumerator Finish()
        {
            yield return new WaitForSeconds(LIFETIME);
            yield return new WaitWhile(() => GameManager.IsPaused);
            
            Destroy(gameObject);
        }

        private IEnumerator Activate()
        {
            yield return new WaitForSeconds(UNACTIVE_TIME);

            GetComponent<Collider2D>().enabled = true;
        }
    }
}