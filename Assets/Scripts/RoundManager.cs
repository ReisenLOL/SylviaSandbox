using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    #region Statication

    public static RoundManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    public Player player;
    [Header("Enemies")]
    public Enemy[] enemyList;
    public List<Enemy> currentEnemies;
    public Transform enemyFolder;
    [Header("Spawning")]
    public Transform[] spawnLocations;
    public float spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnWave(2));
    }

    public void UpdateEnemyCount()
    {
        if (currentEnemies.Count == 0)
        {
            StartCoroutine(SpawnWave(2)); //i'll have to figure out the wave spawning amount later.
        }
    }

    IEnumerator SpawnWave(int amount)
    {
        int spawned = 0;
        while (spawned < amount)
        {
            int randomEnemy = Random.Range(0, enemyList.Length);
            Enemy newEnemy = Instantiate(enemyList[randomEnemy], enemyFolder);
            currentEnemies.Add(newEnemy);
            newEnemy.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;
            spawned++;
            yield return new WaitForSeconds(spawnDelay);
        }
        yield break;
    }
}
