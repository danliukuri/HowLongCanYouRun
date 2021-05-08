using UnityEngine;

class ObstacleSpawner : ObjectSpawner
{
    public override void Spawn(Transform currentTrackPart)
    {
        base.Spawn(currentTrackPart);
        if (Random.value > 0.5f)
            numberOfObjectsOnOneTrack++;
    }
}