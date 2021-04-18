using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] Transform player;
    [SerializeField] Transform track;

    [SerializeField] GameObject startingTrackPart;
    [SerializeField] Transform startingObstacleTrack;
    [SerializeField] GameObject trackPart;

    Quaternion trackPartRotation;
    Vector3 trackPartPosition;
    float trackPartLengthZ;

    float previousTrackPartPositionZ;
    #endregion

    #region
    // Start is called before the first frame update
    void Start()
    {
        trackPartRotation = Quaternion.identity;
        Transform previousTrackPart = startingTrackPart.transform;
        trackPartLengthZ = previousTrackPart.localScale.z;
        trackPartPosition = new Vector3(0f, 0f, previousTrackPart.position.z);
        
        obstacleSpawner.Spawn(startingObstacleTrack);
        Spawn();
        Spawn();
    }
    // Update is called once per frame
    void Update()
    {
        if (player.position.z > previousTrackPartPositionZ)
          Spawn();
    }

    void Spawn()
    {
        previousTrackPartPositionZ = trackPartPosition.z;
        trackPartPosition.z += trackPartLengthZ;
        Transform currentTrackPart = Instantiate(trackPart, trackPartPosition, trackPartRotation, track).transform;
        obstacleSpawner.Spawn(currentTrackPart);
    }
    #endregion
}