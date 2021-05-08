using UnityEngine;

public class ShieldSpawner : ObjectSpawner
{
    [SerializeField] GameObject player;
    private void Awake()
    {
        objectToSpawn.GetComponent<ShieldController>().Player = player;
    }
}