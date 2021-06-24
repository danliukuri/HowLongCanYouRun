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
        [SerializeField] GameObject coinsCounter;
        [SerializeField] TextMeshProUGUI coinsCounterTMP;
        static CoinsUIManager instance;
        #endregion

        #region Methods
        void Awake()
        {
            instance = this; // No more than one instance of the class in the scene
            
            if (numberOfCoinsTMP && coinsDecoration)
                TryToOutputTheNumberOfCoins();
        }

        public void OutputAward()
        {
            awardTMP.text = "You ran " + RunDistanceController.CurrentRunDistance + " cubes! " +
                "And you also earned " + CoinController.AwardCoinsCount + " coins!\nKeep it up)";
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

        public static void CoinPickUp()
        {
            if (!instance.coinsCounter.activeSelf)
                instance.coinsCounter.SetActive(true);
            instance.coinsCounterTMP.text = CoinController.AwardCoinsCount.ToString("000");
        }
        #endregion
    }
}