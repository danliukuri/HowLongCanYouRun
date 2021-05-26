using UnityEngine;

public class CoinSpawnController : MonoBehaviour
{
    CoinSpawner coinSpawner;
    void Awake()
    {
        coinSpawner = GetComponent<CoinSpawner>();
    }
    public void SpawnAwardCoins() => coinSpawner.StartCoroutineSpawn(CoinController.AwardCoinsCount);
}
