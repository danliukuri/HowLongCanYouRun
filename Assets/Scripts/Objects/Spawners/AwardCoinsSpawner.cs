using UnityEngine;

public class AwardCoinsSpawner : MonoBehaviour
{
    CoinSpawner coinSpawner;
    void Awake()
    {
        coinSpawner = GetComponent<CoinSpawner>();
    }
    public void SpawnAwardCoins() => coinSpawner.StartCoroutineSpawn(CoinController.AwardCoinsCount);
}