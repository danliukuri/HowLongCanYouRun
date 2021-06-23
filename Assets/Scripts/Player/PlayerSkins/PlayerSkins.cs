using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public static IReadOnlyList<PlayerSkin> Get() => playerSkinsList;
    static IReadOnlyList<PlayerSkin> playerSkinsList;

    void Awake() 
    {
        if (playerSkinsList is null)
            playerSkinsList = GetComponents<PlayerSkin>();
    }
}