using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    #region Properties
    public Material Material => material;
    public int Price => price;
    public bool IsItPurchased { get => isItPurchased; set => isItPurchased = value; }
    #endregion

    #region Fields
    [SerializeField] Material material;
    [SerializeField] int price;
    [SerializeField] bool isItPurchased;
    #endregion
}