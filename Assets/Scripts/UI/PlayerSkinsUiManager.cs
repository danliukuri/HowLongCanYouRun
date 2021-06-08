using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PlayerSkinsUiManager : MonoBehaviour
{
    #region Fields
    [SerializeField] CanvasGroup shopCanvasGroup;
    [SerializeField] TextMeshProUGUI skinPrice;

    [Header("Buttons")]
    [SerializeField] Button buyButton;
    GameObject buyButtonGameObject;
    [SerializeField] Button selectButton;
    GameObject selectButtonGameObject;

    [Header("Skin purchase")]
    [SerializeField] MoveAndRotateToTargetBehaviour cameraToViewThePurchase;
    [SerializeField] MoveAndRotateToTargetBehaviour cameraToTradingPosition;
    [SerializeField] GameObject skinPurchaseEffect;
    [SerializeField] CoinSpawner coinSpawner;

    int numberOfCoinsAfterPurchase;
    const string sold = "sold";
    #endregion

    #region Methods
    void Awake()
    {
        skinPrice.text = sold;

        buyButtonGameObject = buyButton.gameObject;
        buyButtonGameObject.SetActive(false);

        selectButtonGameObject = selectButton.gameObject;
        selectButtonGameObject.SetActive(true);
        selectButtonGameObject.GetComponent<Button>().interactable = false;
    }
 
    public void NextSkin()
    {
        PlayerSkinsController.NextSkin();
        DisplaySkinAndButtons();
    }
    public void PreviousSkin()
    {
        PlayerSkinsController.PreviousSkin();
        DisplaySkinAndButtons();
    }
    void DisplaySkinAndButtons()
    {
        shopCanvasGroup.blocksRaycasts = false;
        StartCoroutine(StaticFunctions.Invoke(() =>
        {
            PlayerSkinsController.DisplayCurrentSkin();
            DisplaySkinPrice();
            if (PlayerSkinsController.GetCurrentlyDisplayedSkin().IsItPurchased)
                DisplaySelectButton();
            else
                DisplayBuyButton();
        }, 0.7f));
        StartCoroutine(StaticFunctions.Invoke(() => shopCanvasGroup.blocksRaycasts = true, 1.1f));
    }
    void DisplaySkinPrice() => skinPrice.text = PlayerSkinsController.GetCurrentlyDisplayedSkin().IsItPurchased ? sold :
                                                PlayerSkinsController.GetCurrentlyDisplayedSkin().Price.ToString();
    void DisplaySelectButton()
    {
        buyButtonGameObject.SetActive(false);
        selectButtonGameObject.SetActive(true);
        selectButton.interactable = !PlayerSkinsController.IsSelectedCurrentlyDisplayedSkin();
    }
    void DisplayBuyButton()
    {
        selectButtonGameObject.SetActive(false);
        buyButtonGameObject.SetActive(true);
        numberOfCoinsAfterPurchase = PlayerPrefs.GetInt("NumberOfCoins") - PlayerSkinsController.GetCurrentlyDisplayedSkin().Price;
        buyButton.interactable = numberOfCoinsAfterPurchase >= 0;
    }

    public void BuySkin()
    {
        shopCanvasGroup.blocksRaycasts = false;
        coinSpawner.StartCoroutineSpawn(40);
        cameraToViewThePurchase.enabled = true;

        PlayerSkinsController.BuySkin();
        PlayerPrefs.SetInt("NumberOfCoins", numberOfCoinsAfterPurchase);
        StartCoroutine(StaticFunctions.Invoke(() =>
        {
            cameraToTradingPosition.enabled = true;

            skinPrice.text = sold;
            DisplaySelectButton();
            UI.CoinsUIManager.UpdateOutputTheNumberOfCoins();
        }, 4f));
    }
    public void SetCurrentlyDisplayedSkinMaterial(Renderer renderer) => 
        renderer.material = PlayerSkinsController.GetCurrentlyDisplayedSkin().Material;
    public void SelectSkin()
    {
        PlayerSkinsController.SetCurrentSkin();
        selectButton.interactable = false;
    }
    #endregion
}