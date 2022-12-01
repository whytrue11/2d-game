using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (++gameManager.roomsSpawned == gameManager.roomsSpawnLimit || gameManager.rooms.Count == 0)
        {
            Instantiate(gameManager.endRoom, transform.position, gameManager.endRoom.transform.rotation);
        }
        else 
        {
            int random = UnityEngine.Random.Range(0, gameManager.rooms.Count - 1);
            Instantiate(gameManager.rooms[random], transform.position, gameManager.rooms[random].transform.rotation);
            gameManager.rooms.RemoveAt(random);
        }
    }
}
