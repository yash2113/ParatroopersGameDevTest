using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider2D spawnArea;

    public GameObject[] helicopterPrefab;
    private HelicopterMovement helicopterMovement;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    public bool rightSpawner;

    public float minForce = 10f;
    private float maxForce = 20f;

    public float maxLifeTime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
        
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while(enabled)
        {
            GameObject prefab = helicopterPrefab[Random.Range(0, helicopterPrefab.Length)];

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = 0;

            Quaternion rotation;

            if(rightSpawner)
            {
                rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                 rotation = Quaternion.Euler(0f, 180f, 0f);
            }

            GameObject helicopter = Instantiate(prefab, position, rotation);
            Destroy(helicopter,maxLifeTime);



            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }

    }

}
