using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame
    public enum Type { Damage, AttackSpeed, Health, WideShot, Shield, Null };
    public enum Power { Minor, Medium, Strong };
    [System.Serializable]
    public class PowerUp
    {
        public string name;
        public Transform power_up;
        public Type type;
        public Type type_2;
        public Power power;
        public Power power_2;
        public float spawn_time;
        public int kills_required;
    }
    public PowerUp[] powerUps;
    public int kill_counter = 0;
    public int power_up_index = 0;
    public float timer = 0;

    void spawnPowerUp(PowerUp powerup)
    {
        if (kill_counter >= powerup.kills_required)
        {
            Instantiate(powerup.power_up, transform.position, transform.rotation);
            power_up_index++;
        }
    }
    void Start()
    {
        power_up_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (power_up_index < powerUps.Length)
        {
            for (int i = 0; i < powerUps.Length; i++)
            {
                if (timer >= powerUps[i].spawn_time)
                {
                    spawnPowerUp(powerUps[i]);
                }

            }
        }
        timer += Time.deltaTime;

    }
}
