using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerups;
    private bool _stopSpawning = false;
    public int _spawnCounter;

    private void Start()
    {
        //StartSpawning();

        _spawnCounter = 0;
    }

    private void Update()
    {
        PowerUpCount();
    }


    IEnumerator SpawnPowerupRoutine()
    {
        _spawnCounter++;
        if (_spawnCounter == 0) {
            yield return new WaitForSeconds(6);
        }
        yield return new WaitForSeconds(Random.Range(3,8));

        while (_stopSpawning == false)
        {  
            Vector3 posToSpawn = new Vector3(0,Random.Range(-5f, 5f), 0);
            int randomPowerUp = Random.Range(0, 2);
            GameObject PowerUp = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            //GameObject PowerUp = Instantiate(powerups[0], posToSpawn, Quaternion.identity);
            PowerUp.transform.parent = gameObject.transform;
            StopSpawning();
        }
        
    }
    public void StartSpawning()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnPowerupRoutine());
        //Debug.Log("spawneando");
    }

    public void StopSpawning()
    {
        _stopSpawning = true;
        StopCoroutine(SpawnPowerupRoutine());
    }

    public void PowerUpCount()
    {
        if (transform.childCount > 0)
        {
            StopSpawning();
        }
    }
    public void DestroyPowerUp()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
