using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose; //Array con las puertas a abrir
    [SerializeField] bool closeDoorOnPlayerEnter, openDoorsNoEnemies;  //Bool que necesito para las condiciones
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator animator;
    public Text waveName;
    [SerializeField] Wave currentWave;
    [SerializeField] int currentWaveNumber;
    private float nextSpawnTime;
    [SerializeField] bool canSpawn = true;
    private bool canAnimate = false;
    private bool startWaves = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (startWaves)
        {
            currentWave = waves[currentWaveNumber];
            SpawnWave();
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemies.Length == 0)
            {
                if (currentWaveNumber + 1 != waves.Length)
                {
                    if (canAnimate)
                    {
                        waveName.text = waves[currentWaveNumber + 1].waveName;
                        animator.SetTrigger("WaveComplete");
                        canAnimate = false;
                    }
                }
                else
                {
                    for (int i = 0; i < doorsToClose.Length; i++)
                    {
                        doorsToClose[i].SetActive(false);
                    }
                    Debug.Log("GameFinish");
                }
            }
        }
        
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {//Si lo que entra es un Player
        if (collision.CompareTag("Player"))
        {//Y bool=true
            startWaves = true;
            Debug.Log("Player ha entrado");
            if (closeDoorOnPlayerEnter)
            {//Cada puerta, cierrala
                foreach (GameObject door in doorsToClose)
                {
                    door.SetActive(true);
                }
            }
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}
