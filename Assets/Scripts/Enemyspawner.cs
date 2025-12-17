using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemyspawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float[] limitX = {-10f, 10f};
    [SerializeField] private float[] limitY = {-10f, 10f};
    [SerializeField] private float poolSize = 10;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private Transform turretPos;
    private float timer = 0f;
    private Stack<GameObject> pool = new Stack<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pool.Push(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        if (pool.Count > 0)
        {
            GameObject enemy = pool.Pop();
            float randomX = Random.Range(limitX[0], limitX[1]);
            float randomY = Random.Range(limitY[0], limitY[1]);
            enemy.SetActive(true);
            enemy.transform.position = new Vector3(randomX, randomY, 0f);
            Vector3 velocity = (turretPos.position - enemy.transform.position).normalized * enemySpeed;
            Debug.Log(velocity);
            enemy.GetComponent<Rigidbody2D>().linearVelocity = velocity;
            
        }

    }

}
