using UnityEngine;

class ObstacleSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform obstacles;
    [SerializeField] GameObject obstacle;
    [SerializeField] int obstaclesNumberOnOneTrack;

    Transform obstacleTransform;
    Vector3 spawnPosition;
    #endregion

    #region Methods
    void Awake()
    {
        obstacleTransform = obstacle.transform;
    }

    public void Spawn(Transform currentTrackPart)
    {
        SetSpawnPosition(currentTrackPart);
        for (int i = 0; i < obstaclesNumberOnOneTrack; i++)
        {
            Instantiate(obstacle, GetRandomObstaclePosition() + currentTrackPart.position, GetRandomObstacleRotation(), obstacles);
        }
        if (Random.value > 0.5f)
            obstaclesNumberOnOneTrack++;
    }

    void SetSpawnPosition(Transform currentTrackPart)
    {
        spawnPosition = Vector3.zero;
        spawnPosition.x += currentTrackPart.lossyScale.x - 1;
        spawnPosition.z += currentTrackPart.lossyScale.z - 1;
        spawnPosition.y += 1f;
    }

    Vector3 GetRandomObstaclePosition() => new Vector3(spawnPosition.x * (Random.value - 0.5f), spawnPosition.y, spawnPosition.z * (Random.value - 0.5f));    
    Quaternion GetRandomObstacleRotation() => Quaternion.Euler(0f, Random.Range(0f, 90f), 0f);
    #endregion
}