using UnityEngine;

public class CopManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject Cop;                // The cop prefab to be spawned.
    public float spawnTime;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public int numCops;



    void Start()
    {
        spawnTime = 10f;
         numCops = 0;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
       

    }


    void Spawn()
    {
        // conditions? 

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        if(numCops <= 3)
        {
            numCops++;
            Instantiate(Cop, spawnPoints[0].position, spawnPoints[0].rotation);
        }

    }
}
