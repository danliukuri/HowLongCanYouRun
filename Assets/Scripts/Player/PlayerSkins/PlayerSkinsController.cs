using UnityEngine;
using Utilities;

public class PlayerSkinsController : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject player;
    static MeshRenderer playerRenderer;

    static PlayerSkin[] playerSkins;

    static PlayerSkin currentPlayerSkin;
    static int currentSkinIndex;
    #endregion

    #region Methods
    private void Awake()
    {
        playerRenderer = player.GetComponent<MeshRenderer>();

        playerSkins = GetPlayerSkins();
        GetCurrentPlayerSkinAndIndex();
    }
    PlayerSkin[] GetPlayerSkins() => !PlayerPrefs.HasKey("PlayerSkinsJson") ? GetComponents<PlayerSkin>() :
        JsonHelper.FromJsonToArray<PlayerSkin>(PlayerPrefs.GetString("PlayerSkinsJson"));
    void GetCurrentPlayerSkinAndIndex()
    {
        currentPlayerSkin = new PlayerSkin();
        if (PlayerPrefs.HasKey("CurrentPlayerSkinJson"))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("CurrentPlayerSkinJson"), currentPlayerSkin);
            for (int i = 0; i < playerSkins.Length; i++)
                if (playerSkins[i].Material == currentPlayerSkin.Material)
                    currentSkinIndex = i;
        }
        else
        {
            currentPlayerSkin = playerSkins[0];
            currentSkinIndex = 0;
        }
    }

    public static void NextSkin() => currentSkinIndex = (currentSkinIndex == playerSkins.Length - 1) ? 0 : currentSkinIndex + 1;
    public static void PreviousSkin() => currentSkinIndex = (currentSkinIndex == 0) ? playerSkins.Length - 1 : currentSkinIndex - 1;
    public static void DisplayCurrentSkin() => playerRenderer.material = playerSkins[currentSkinIndex].Material;
    
    public static PlayerSkin GetCurrentlyDisplayedSkin() => playerSkins[currentSkinIndex];
    public static bool IsSelectedCurrentlyDisplayedSkin() => playerSkins[currentSkinIndex].Material == currentPlayerSkin.Material;

    public static void BuySkin()
    {
        GetCurrentlyDisplayedSkin().IsItPurchased = true;
        SavePlayerSkins();
    }
    static void SavePlayerSkins() => PlayerPrefs.SetString("PlayerSkinsJson", JsonHelper.ArrayToJson(playerSkins));
    public static void SetCurrentSkin()
    {
        currentPlayerSkin = playerSkins[currentSkinIndex];
        PlayerPrefs.SetString("CurrentPlayerSkinJson", JsonUtility.ToJson(currentPlayerSkin));
    }
    #endregion
}