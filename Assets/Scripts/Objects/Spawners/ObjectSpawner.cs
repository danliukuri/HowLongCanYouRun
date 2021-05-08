using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    #region Fields
    [Tooltip("A transform that will be the parent of all spawned objects")]
    [SerializeField] protected Transform objectsParent;
    [Tooltip("Gameobject prefab that will spawn")]
    [SerializeField] protected GameObject objectToSpawn;
    [SerializeField] protected int numberOfObjectsOnOneTrack;

    protected Vector3 spawnPosition;
    #endregion

    #region Methods
    public virtual void Spawn(Transform currentTrackPart)
    {
        spawnPosition = new Vector3(currentTrackPart.lossyScale.x - 1f, 1f, currentTrackPart.lossyScale.z - 1f); // Set spawn position
        for (int i = 0; i < numberOfObjectsOnOneTrack; i++)
            Instantiate(objectToSpawn, GetRandomPositionXZ() + currentTrackPart.position, GetRandomRotationY(), objectsParent);
    }
    protected virtual Vector3 GetRandomPositionXZ() => new Vector3(spawnPosition.x * (Random.value - 0.5f),
        spawnPosition.y, spawnPosition.z * (Random.value - 0.5f));
    protected virtual Quaternion GetRandomRotationY() => Quaternion.Euler(0f, Random.Range(0f, 90f), 0f);
    #endregion
}