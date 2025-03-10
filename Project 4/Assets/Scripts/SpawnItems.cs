using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float baseSpawnRate = 5f; 
    [SerializeField] private float minSpawnRate = 0.5f; 
    [SerializeField] private float difficultyScaling = 0.2f; 
    
    [Header("Score Tracking")]
    [SerializeField] private bool useGameManagerForScore = true; 
    [SerializeField] private Goal[] goalObjects; 
    
    //private float nextSpawnTime = 0f;
    private int cachedTotalScore = 0;
    
    private void Start()
    {
        // find goals
        if (!useGameManagerForScore && goalObjects.Length == 0)
        {
            goalObjects = FindObjectsByType<Goal>(FindObjectsSortMode.None);;
        }
        
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int totalScore = GetTotalScore();
            
            if (totalScore != cachedTotalScore)
            {
                cachedTotalScore = totalScore;
                Debug.Log("Total score updated: " + totalScore);
            }
            
            float currentSpawnRate = Mathf.Max(minSpawnRate, baseSpawnRate - (difficultyScaling * totalScore));
            
            yield return new WaitForSeconds(currentSpawnRate);
            
            SpawnRandomItem();
        }
    }
    
    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            //Debug.LogWarning("No item prefabs or spawn points assigned!");
            return;
        }
        
        GameObject selectedPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        Instantiate(selectedPrefab, selectedSpawnPoint.position, Quaternion.identity);
        
        //Debug.Log("Spawned item: " + selectedPrefab.name);
    }
    
    private int GetTotalScore()
    {
        if (useGameManagerForScore && GameManager.Instance != null)
        {
            // Get score from game manager
            return GameManager.Instance.GetTotalScore();
        }
        else
        {
            int total = 0;
            foreach (Goal goal in goalObjects)
            {
                if (goal != null)
                {
                    total += goal.getScore();
                }
            }
            return total;
        }
    }
    
    public void ForceSpawnItem()
    {
        SpawnRandomItem();
    }
}