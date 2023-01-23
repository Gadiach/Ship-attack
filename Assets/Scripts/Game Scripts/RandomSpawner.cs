using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject shipPrefab;    
    public bool canSpawn;
    
    private void Update()
    {
        SpawnShip(); 
    }

    void SpawnShip()
    {
        if (canSpawn)
        {
            Instantiate(shipPrefab, transform.position, Quaternion.identity);
            canSpawn = false;
        }
    }
}
