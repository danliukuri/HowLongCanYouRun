using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Fields
    [Header("Award menu")]
    [SerializeField] TextMeshProUGUI awardTMP;
    [Header("Main menu")]
    [SerializeField] TextMeshProUGUI numberOfCoinsTMP;
    [SerializeField] GameObject coinsDecoration;
    [Header("Gameplay menu")]
    [SerializeField] TextMeshProUGUI coinCount;
    static UIManager instance;
    #endregion

    #region Methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OutputAward()
    {
        // ran 5000 cubes and
        awardTMP.text = "You earned " + (CoinController.AwardCoinsCount).ToString() + " coins, keep it up!!!";
    }

    public void TryToOutputTheNumberOfCoins()
    {
        if(PlayerPrefs.GetInt("NumberOfCoins") > 0)
        {
            numberOfCoinsTMP.text = PlayerPrefs.GetInt("NumberOfCoins").ToString();
            numberOfCoinsTMP.gameObject.SetActive(true);
            coinsDecoration.SetActive(true);
        }
    }

    public static void CoinPick()
    {
        instance.coinCount.text = (CoinController.AwardCoinsCount).ToString("000");
    }
    #endregion
}