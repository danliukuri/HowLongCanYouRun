using UnityEngine;

public class ShieldSpawner : Spawner
{
    [SerializeField] GameObject player;
    private void Awake()
    {
        objectToSpawn.GetComponent<ShieldController>().Player = player;
    }
    public override void Spawn(Transform currentTrackPart)
    {
        spawnPosition = new Vector3(currentTrackPart.lossyScale.x - 1f, 1f, currentTrackPart.lossyScale.z - 1f); // Set spawn position
        for (int i = 0; i < numberOfObjectsOnOneTrack; i++)
            Instantiate(objectToSpawn, GetRandomPositionXZ() + currentTrackPart.position,GetRandomRotationY(), objectsParent);
    }
}