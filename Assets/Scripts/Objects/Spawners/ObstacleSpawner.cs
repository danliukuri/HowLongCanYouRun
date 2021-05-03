using UnityEngine;

class ObstacleSpawner : Spawner
{
    public override void Spawn(Transform currentTrackPart)
    {
        base.Spawn(currentTrackPart);
        if (Random.value > 0.5f)
            numberOfObjectsOnOneTrack++;
    }
}