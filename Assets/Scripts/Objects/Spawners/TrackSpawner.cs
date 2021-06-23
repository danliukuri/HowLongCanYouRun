using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform player;
    [Header("Parts of the track")]
    [Tooltip("A transform that will be the parent of all spawned objects")]
    [SerializeField] Transform partsOfTheTrack;
    [SerializeField] GameObject startingTrackPart;
    [SerializeField] GameObject trackPart;

    readonly List<ObjectSpawner> objectSpawners = new List<ObjectSpawner>();
    Quaternion trackPartRotation;
    Vector3 trackPartPosition;
    float trackPartLengthZ;

    /// <summary>
    /// The position, when the player reaches which you need to create a part of the track
    /// </summary>
    float positionToSpawn;
    #endregion

    #region
    void Awake()
    {
        trackPartRotation = Quaternion.identity;
        Transform previousTrackPart = startingTrackPart.transform;
        trackPartLengthZ = trackPart.transform.localScale.z;
        trackPartPosition = new Vector3(0f, 0f,
            previousTrackPart.position.z + previousTrackPart.localScale.z / 2 + trackPartLengthZ / 2);

        GetComponents(objectSpawners);
    }
   
    public void InitialTrackSpawn()
    {
        Spawn(3);
        positionToSpawn = trackPartLengthZ;
    }
    // Update is called once per frame
    void Update()
    {
        if (player.position.z > positionToSpawn)
        {
            Spawn();
            positionToSpawn += trackPartLengthZ;
        } 
    }

    void Spawn(int count)
    { 
        for (int i = 0; i < count; i++)
            Spawn();
    }
    void Spawn()
    {
        Transform currentTrackPart = Instantiate(trackPart, trackPartPosition, trackPartRotation, partsOfTheTrack).transform;
        for (int i = 0; i < objectSpawners.Count; i++)
            objectSpawners[i].Spawn(currentTrackPart);
        trackPartPosition.z += trackPartLengthZ;
    }
    #endregion
}