using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform player;
    [Header("Inner spawners")]
    [SerializeField] ObstacleSpawner obstacleSpawner;
    [SerializeField] List<ShieldSpawner> bonusSpawners;
    [Header("Parts of the track")]
    [Tooltip("A transform that will be the parent of all spawned objects")]
    [SerializeField] Transform partsOfTheTrack;
    [SerializeField] GameObject startingTrackPart;
    [SerializeField] GameObject trackPart;
    [SerializeField] Transform startingObstacleTrack;

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
        Transform currentTrackPart = Instantiate(trackPart, trackPartPosition, trackPartRotation, partsOfTheTrack).transform;
        obstacleSpawner.Spawn(currentTrackPart);
        for (int i = 0; i < bonusSpawners.Count; i++)
            bonusSpawners[i].Spawn(currentTrackPart);
    }
    #endregion
}