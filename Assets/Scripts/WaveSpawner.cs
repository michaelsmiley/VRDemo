using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenWaves = 5f;

    private int nextWave = 0;
    private float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    AudioManager audioManager;
    LevelController levelController; //

    private void Start()
    {
        //Check if spawn points have been placed
        if (spawnPoints.Length == 0)
        {
            print("No spawn points referenced");
        }

        audioManager = FindObjectOfType<AudioManager>();
        levelController = FindObjectOfType<LevelController>(); //
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //Begin next round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        //if it's time to start spawning waves
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        print("Spawning Wave: " + wave.name);
        audioManager.Play("MoreComing");
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i = 0; i < wave.count; i++)
        {
            //Spawn an enemy, wait and then spawn another
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.WAITING;

        //yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        audioManager.Play("ZombieYell");

        //Choose Random spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Spawn Enemy
        Instantiate(enemy, randomSpawnPoint.position, randomSpawnPoint.rotation);

        print("Spawning Enemy: " + enemy.name);
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            //Reset searchCountdown
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        audioManager.Play("SafeForAMoment");
        print("Wave Complete");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            /*
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE, Looping");
            audioManager.Play("AllWavesComplete");
            */
            levelController.NextLevel();
        }
        else
        {
            nextWave++;
        }
    }
}
