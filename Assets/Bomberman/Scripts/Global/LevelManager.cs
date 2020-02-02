using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is responsible for  player spawning
/// later this can can be use to load the level from data file 
/// also this class check if player is death and trigger the game over event 
/// </summary>
public class LevelManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Character playerPrefab;

    Character currentPlayer;

    private void OnEnable()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
        currentPlayer.OnDeath += OnPlayerDeath;
        // we egister player death event 
    }

    void OnPlayerDeath(Character obj)
    {
        // we player is dead we whould trigger the gameover event 
        obj.OnDeath -= OnPlayerDeath;
        GUIPanelController.Instance.ChangeState(PanelType.GAMEOVERPANEL);
    }
}
