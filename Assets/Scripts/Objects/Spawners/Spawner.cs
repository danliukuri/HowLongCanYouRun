using UnityEngine;

public abstract class Spawner : MonoBehaviour
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
    public abstract void Spawn(Transform currentTrackPart);
    protected virtual Vector3 GetRandomPositionXZ() => new Vector3(spawnPosition.x * (Random.value - 0.5f),
        spawnPosition.y, spawnPosition.z * (Random.value - 0.5f));
    protected virtual Quaternion GetRandomRotationY() => Quaternion.Euler(0f, Random.Range(0f, 90f), 0f);
    #endregion
}