using UnityEngine;

public class SetPlayerSkinOnAwake : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentPlayerSkinJson"))
        {
            PlayerSkin currentPlayerSkin = new PlayerSkin();
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("CurrentPlayerSkinJson"), currentPlayerSkin);
            GetComponent<MeshRenderer>().material = currentPlayerSkin.Material;
        }
    }
}