using TMPro;
using UnityEngine;
using Utilities;

namespace UI
{
    public class CoinsUIManager : MonoBehaviour
    {
        #region Fields
        [Header("Award menu")]
        [SerializeField] TextMeshProUGUI awardTMP;
        [Header("Number of coins")]
        [SerializeField] TextMeshProUGUI numberOfCoinsTMP;
        [SerializeField] GameObject coinsDecoration;
        [Header("Gameplay menu")]
        [SerializeField] TextMeshProUGUI coinCount;
        static CoinsUIManager instance;
        #endregion

        #region Methods
        void Awake()
        {
            if (instance == null)
                instance = this;
            if (numberOfCoinsTMP && coinsDecoration)
                TryToOutputTheNumberOfCoins();
        }

        public void OutputAward()
        {
            // Ran 5000 cubes and...
            awardTMP.text = "You earned " + CoinController.AwardCoinsCount.ToString() + " coins, keep it up!!!";
            SaveAwardCoins();
        }
        void SaveAwardCoins()
        {
            int previousNumberOfCoins = FileManager.DoesTheFileExist("NumberOfCoins") ?
                int.Parse(FileManager.LoadStringFromFile("NumberOfCoins")) : 0;
            FileManager.SaveStringToFile((previousNumberOfCoins + CoinController.AwardCoinsCount).ToString(), "NumberOfCoins");
            CoinController.ResetAwardCoinsCount();
        }

        public void TryToOutputTheNumberOfCoins()
        {
            int numberOfCoins = FileManager.DoesTheFileExist("NumberOfCoins") ?
                int.Parse(FileManager.LoadStringFromFile("NumberOfCoins")) : 0;
            bool isNumberOfCoinsGreaterThanZero = numberOfCoins > 0;

            numberOfCoinsTMP.text = numberOfCoins.ToString();
            numberOfCoinsTMP.gameObject.SetActive(isNumberOfCoinsGreaterThanZero);
            coinsDecoration.SetActive(isNumberOfCoinsGreaterThanZero);
        }

        public static void UpdateOutputTheNumberOfCoins() => instance.TryToOutputTheNumberOfCoins();
        public static void CoinPick()
        {
            instance.coinCount.text = CoinController.AwardCoinsCount.ToString("000");
        }
        #endregion
    }
}