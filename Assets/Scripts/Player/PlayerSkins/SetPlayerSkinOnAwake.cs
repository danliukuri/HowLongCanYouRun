using UnityEngine;
using Utilities;

public class SetPlayerSkinOnAwake : MonoBehaviour
{
    void Start()
    {
        if (FileManager.DoesTheFileExist("CurrentPlayerSkinIndex"))
        {
            int currentSkinIndex = int.Parse(FileManager.LoadStringFromFile("CurrentPlayerSkinIndex"));
            PlayerSkin currentPlayerSkin = PlayerSkins.Get()[currentSkinIndex];
            GetComponent<MeshRenderer>().material = currentPlayerSkin.Material;
        }
    }
}