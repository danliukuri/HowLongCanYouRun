using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerSkinsController : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject player;
    static MeshRenderer playerRenderer;

    static IReadOnlyList<PlayerSkin> playerSkins;
    static List<int> indexesOfPurchasedPlayerSkins;

    static PlayerSkin currentPlayerSkin;
    static int currentSkinIndex;
    #endregion

    #region Methods
    void Awake()
    {
        playerRenderer = player.GetComponent<MeshRenderer>();

        if (playerSkins is null)
            playerSkins = PlayerSkins.Get();
        if (indexesOfPurchasedPlayerSkins is null)
        {
            indexesOfPurchasedPlayerSkins = !FileManager.DoesTheFileExist("IndexesOfPurchasedPlayerSkins") ? new List<int>() :
                new List<int>(JsonHelper.FromJson<int>(FileManager.LoadStringFromFile("IndexesOfPurchasedPlayerSkins")));
            for (int i = 0; i < indexesOfPurchasedPlayerSkins.Count; i++)
                playerSkins[indexesOfPurchasedPlayerSkins[i]].IsItPurchased = true;
        }

        if (FileManager.DoesTheFileExist("CurrentPlayerSkinIndex"))
            currentSkinIndex = int.Parse(FileManager.LoadStringFromFile("CurrentPlayerSkinIndex"));

        currentPlayerSkin = playerSkins[currentSkinIndex];
    }

    public static void NextSkin() => currentSkinIndex = (currentSkinIndex == playerSkins.Count - 1) ? 0 : currentSkinIndex + 1;
    public static void PreviousSkin() => currentSkinIndex = (currentSkinIndex == 0) ? playerSkins.Count - 1 : currentSkinIndex - 1;
    public static void DisplayCurrentSkin() => playerRenderer.material = playerSkins[currentSkinIndex].Material;

    public static PlayerSkin GetCurrentlyDisplayedSkin() => playerSkins[currentSkinIndex];
    public static bool IsSelectedCurrentlyDisplayedSkin() => playerSkins[currentSkinIndex].Material == currentPlayerSkin.Material;

    public static void BuySkin()
    {
        GetCurrentlyDisplayedSkin().IsItPurchased = true;
        indexesOfPurchasedPlayerSkins.Add(currentSkinIndex);
        SavePlayerSkins();
    }
    static void SavePlayerSkins() => FileManager.SaveStringToFile(JsonHelper.ToJson(indexesOfPurchasedPlayerSkins.ToArray()),
        "IndexesOfPurchasedPlayerSkins");
    
    public static void SetCurrentSkin()
    {
        currentPlayerSkin = playerSkins[currentSkinIndex];
        FileManager.SaveStringToFile(currentSkinIndex.ToString(), "CurrentPlayerSkinIndex");
    }
    #endregion
}