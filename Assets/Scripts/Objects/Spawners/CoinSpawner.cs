using System.Collections;
using UnityEngine;

public class CoinSpawner : ObjectSpawner
{
    WaitForSeconds randomDelayForSpawn;
    void Awake()
    {
        randomDelayForSpawn = new WaitForSeconds(Random.Range(0f, 0.2f));
    }
    public void StartCoroutineSpawn(int count = 1) => StartCoroutine(Spawn(count));
    IEnumerator Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return randomDelayForSpawn;
            Spawn(transform);
        }      
    }
    protected override Quaternion GetRandomRotationY() => 
        Quaternion.Euler(Random.Range(0f, 180f), Random.Range(0f, 180f), Random.Range(0f, 90f));
}