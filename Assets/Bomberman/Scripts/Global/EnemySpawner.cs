using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelManager levelmanager;
    public int numberOfEnemiesToSpawn;
    public Character[] enemies;
    public float minimumDstBetweenPlayerStartPointAndEnemy;

    int currentEnemiesCount;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies ()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemies.Length);
            Vector2 randomPos = GetValidPoint();
            randomPos.x += 0.5f;
            randomPos.y += 0.5f;
            Character character = Instantiate(enemies[randomEnemyIndex], randomPos, Quaternion.identity);
            character.transform.SetParent(transform);
            // we keep track of number of enemies and registers to its death event so that number can be reduced 
            // and win condition can be decided 
            character.OnDeath += OnEnemyDeath;
            currentEnemiesCount++;
        }
    }

    Vector3 GetValidPoint()
    {
        Vector3 randomPos = Vector2.zero;
        int numberOfLoopCheck = 100;
        int count = 0;
        while (count < numberOfLoopCheck)
        {
            // we make sure that enemy is not spawned next toplayer 
            randomPos = NavMesh.GetRandomPosition();
            if(Vector3.Distance(randomPos,levelmanager.playerSpawnPoint.position) > minimumDstBetweenPlayerStartPointAndEnemy )
            {
                return randomPos;
            }
            count++;
        }
        return randomPos;
    }

    void OnEnemyDeath(Character character)
    {
        // we reduce the enemy count
        currentEnemiesCount--;
        character.OnDeath -= OnEnemyDeath;

        // game win event can be trigger here 
        if(currentEnemiesCount <= 0)
        {
            GUIPanelController.Instance.ChangeState(PanelType.GAMEWINPANEL);
        }
    }
}
