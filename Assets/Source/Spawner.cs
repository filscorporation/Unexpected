using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public enum SpawnType
    {
        Position,
        Inside,
        Outside,
    }
    
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnType spawnType;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float periodMin;
        [SerializeField] private float periodMax;
        [SerializeField] private float step;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return Spawn();
            }
        }

        private IEnumerator Spawn()
        {
            float timeout = Random.Range(periodMin, periodMax);
            periodMin -= step;
            if (periodMin < 0)
                periodMin = 0.5f;
            periodMax -= step;
            if (periodMax < 0)
                periodMax = 1f;
            yield return new WaitForSeconds(timeout);
            yield return new WaitWhile(() => GameManager.IsPaused);

            Vector2 position;
            switch (spawnType)
            {
                case SpawnType.Position:
                    position = transform.position;
                    break;
                case SpawnType.Inside:
                    position = Utility.RandomPointInside();
                    break;
                case SpawnType.Outside:
                    position = Utility.RandomPointOutside();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}