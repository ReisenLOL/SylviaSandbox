using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    [SerializeField] private TextMeshProUGUI roundText;
    [Header("Enemies")]
    public Enemy[] enemyList;
    public List<Enemy> currentEnemies;
    public Transform enemyFolder;
    [Header("Spawning")]
    public float spawnDelay;
    public int roundNumber = 1;
    public float amountToSpawn;
    public int maxClusterSize;
    public float baseSpawnAmount;
    public float exponentIncrease;
    [Header("Spawn Locations")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private void Start()
    {
        StartCoroutine(SpawnWave(1));
    }

    public void UpdateEnemyCount()
    {
        GameManager.instance.UpdateScore();
        if (currentEnemies.Count == 0)
        {
            amountToSpawn = Mathf.FloorToInt(baseSpawnAmount * Mathf.Pow(roundNumber, exponentIncrease));
            StartCoroutine(SpawnWave((int)amountToSpawn)); //i'll have to figure out the wave spawning amount later.
            roundNumber++;
            roundText.text = $"Round {roundNumber}";
        }
    }

    IEnumerator SpawnWave(int amount)
    {
        int spawned = 0;
        int cluster;
        if (amount > maxClusterSize)
        {
            cluster = Random.Range(1, maxClusterSize);
        }
        else
        {
            cluster = Random.Range(1, amount); //this gets pointless calculation after like. 2 waves. but like it's the only way i know, and it's literally like a single frame.
        }
        while (spawned < amount) 
        {
            for (int i = 0; i < cluster; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                int randomEnemy = Random.Range(0, enemyList.Length);
                Enemy newEnemy = Instantiate(enemyList[randomEnemy], enemyFolder);
                currentEnemies.Add(newEnemy);
                newEnemy.transform.position = spawnPosition;
                spawned++;
            }
            yield return new WaitForSeconds(spawnDelay); //heh, double loop
        }
        yield break;
    }
}
