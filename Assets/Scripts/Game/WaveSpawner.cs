using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TO-DO:
 * Make each wave hold their own delay value to start spawning after the last wave and make the timer use this value */

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int enemy_count;
        public float enemy_spawn_delay = 1.0f;
        public float enemy_spawn_timer = 0.0f;
        public int enemy_index;
    }

    public Wave[] waves;
    private int wave_index = 0;
    public float time_between_waves = 10.0f;
    public float waves_timer = 0.0f;

    // Start is called before the first frame update
    private float tutorial_time;
    private float tutorial_timer;
    public static bool chat_is_done;

    void Start()
    {
        chat_is_done = false;
        waves_timer = time_between_waves;
    }

    // Update is called once per frame
    void Update()
    {
        if (chat_is_done == true)
        {
            if (waves_timer <= 0.0f && wave_index < waves.Length)
            {
                spawnWave(waves[wave_index]);
            }
            else if (wave_index < waves.Length)
            {
                waves_timer -= Time.deltaTime;
            }
        }
    }

    void spawnWave(Wave wave)
    {
        if (wave.enemy_spawn_timer <= 0.0f)
        {
            Instantiate(wave.enemy, transform.position, transform.rotation);
            wave.enemy_spawn_timer = wave.enemy_spawn_delay;
            wave.enemy_index++;
        }
        else
        {
            wave.enemy_spawn_timer -= Time.deltaTime;
        }

        if (wave.enemy_index >= wave.enemy_count)
        {
            waves_timer = time_between_waves;
            wave_index++;
        }
    }

    //void spawnEnemy (Transform enemy)
    //{
    //    //Spawn enemy
    //    Debug.Log("Spawning enemy: " + enemy.name);
    //}
}
