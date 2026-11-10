using System.Collections;
using UnityEngine;
using static EnemyAI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Players")]
    public Transform[] playerTargets;

    [Header("Wave Settings")]
    public int startingEnemiesPerWave = 5;
    public int additionalEnemiesPerWave = 1;
    public float timeBetweenWaves = 3f;
    public float timeBetweenSpawns = 0.35f;
    public float minSpawnRadius = 6f;
    public float maxSpawnRadius = 10f;

    private int currentWave = 0;
    private int aliveEnemies = 0;

    [SerializeField] private int maxWave = 2;

    [SerializeField] private RoomManager roomManager;

    public void BeginOnslaught()
    {
        roomManager = GetComponent<RoomManager>();
        AutoFindPlayers();

        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: enemyPrefab is not assigned.");
            return;
        }

        if (playerTargets == null || playerTargets.Length == 0)
        {
            Debug.LogError("EnemySpawner: no playerTargets found or assigned.");
            return;
        }

        StartCoroutine(WaveLoop());
    }
    private void EnemyAI_OnEnemyDied(GameObject thatGuy)
    {
        aliveEnemies = Mathf.Max(0, aliveEnemies - 1);
        thatGuy.GetComponent<EnemyAI>().OnEnemyDied -= EnemyAI_OnEnemyDied;
        Debug.Log("Enemy died. Remaining alive: " + aliveEnemies);
        Destroy(thatGuy);
        if (aliveEnemies <= 0 &&currentWave>=maxWave)
        {
            if (roomManager != null)
            {
                roomManager.DoStuffAfterBattle();
            }
        }
    }

    void AutoFindPlayers()
    {
        if (playerTargets != null && playerTargets.Length > 0)
            return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            playerTargets = new Transform[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                playerTargets[i] = players[i].transform;
            }
        }
    }

    IEnumerator WaveLoop()
    {
        while (currentWave<maxWave)
        {
            while (aliveEnemies > 0)
                yield return null;

            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            int enemiesThisWave = startingEnemiesPerWave + (currentWave - 1) * additionalEnemiesPerWave;

            Debug.Log("Starting wave " + currentWave + " with " + enemiesThisWave + " enemies.");

            for (int i = 0; i < enemiesThisWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }

    }

    void SpawnEnemy()
    {
        Transform centerTarget = GetRandomValidPlayer();
        if (centerTarget == null)
        {
            Debug.LogWarning("EnemySpawner: no valid player target found.");
            return;
        }

        Vector2 circle = Random.insideUnitCircle.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 spawnPos = centerTarget.position + new Vector3(circle.x, 0f, circle.y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        aliveEnemies++;

        Debug.Log("Spawned enemy at " + spawnPos);

        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        enemyAI.OnEnemyDied += EnemyAI_OnEnemyDied;
        if (enemyAI == null)
        {
            Debug.LogWarning("Spawned enemy does not have EnemyAI on it.");
        }
    }


    Transform GetRandomValidPlayer()
    {
        if (playerTargets == null || playerTargets.Length == 0)
            return null;

        int tries = 10;

        while (tries-- > 0)
        {
            Transform candidate = playerTargets[Random.Range(0, playerTargets.Length)];
            if (candidate != null && candidate.gameObject.activeInHierarchy)
                return candidate;
        }

        return null;
    }


    private void OnDisable()
    {
        //EnemyAI.OnEnemyDied -= EnemyAI_OnEnemyDied;
    }
}