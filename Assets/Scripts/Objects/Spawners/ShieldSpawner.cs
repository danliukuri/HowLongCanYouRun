using UnityEngine;

public class ShieldSpawner : Spawner
{
    [SerializeField] GameObject player;
    private void Awake()
    {
        objectToSpawn.GetComponent<ShieldController>().Player = player;
    }
}