using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable] private struct Obstacles
    {
        public GameObject prefab;
        [Range(0f,1f)]
        public float spawnChance;
    }

    [SerializeField] private Obstacles[] obstacles;

    [SerializeField] private float minSpawnRate = 1f;
    [SerializeField] private float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obstacle in obstacles)
        {
            if (spawnChance < obstacle.spawnChance)
            {
                GameObject obj = Instantiate(obstacle.prefab);
                obj.transform.position += transform.position;
                break;
            }

            spawnChance -= obstacle.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
