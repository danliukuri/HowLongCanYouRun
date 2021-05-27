using UnityEngine;
using UnityEngine.Events;
using Utilities;

public class GameplayHandler : MonoBehaviour
{
    #region Fields
    [Header("Awake")]
    [SerializeField] UnityEvent eventsOnAwake;
    [Header("Start")]
    [Tooltip("Components that need to be enabled when starting gameplay")]
    [SerializeField] Behaviour[] componentsToEnableOnStart;
    [Tooltip("GameObjects that need to be enabled when starting gameplay")]
    [SerializeField] GameObject[] gameObjectsToSetActiveOnStart;
    [SerializeField] UnityEvent eventsOnStart;
    [Header("Finish")]
    [Tooltip("Components that need to be enabled when finishing gameplay")]
    [SerializeField] Behaviour[] componentsToEnableOnFinish;
    [Tooltip("GameObjects that need to be enabled when finishing gameplay")]
    [SerializeField] GameObject[] gameObjectsToSetActiveOnFinish;
    [SerializeField] UnityEvent eventsOnFinish;

    static GameplayHandler instance;
    #endregion

    #region Methods
    void Awake()
    {
        if (instance == null)
            instance = this;
        componentsToEnableOnStart.ForAll(e => e.enabled = false);
        gameObjectsToSetActiveOnStart.ForAll(e => e.SetActive(false));
        componentsToEnableOnFinish.ForAll(e => e.enabled = false);
        gameObjectsToSetActiveOnFinish.ForAll(e => e.SetActive(false));
        SaveAwardCoins();
        eventsOnAwake.Invoke();
    }

    public static void StartGameplay()
    {
        instance.componentsToEnableOnStart.ForAll(e => e.enabled = true);
        instance.gameObjectsToSetActiveOnStart.ForAll(e => e.SetActive(true));
        instance.eventsOnStart.Invoke();
    }
    public static void FinishGameplay()
    {
        instance.StartCoroutine(StaticFunctions.Invoke(() => 
        {
            instance.gameObjectsToSetActiveOnFinish.ForAll(e => e.SetActive(true));
            instance.componentsToEnableOnFinish.ForAll(e => e.enabled = true);
            instance.eventsOnFinish.Invoke();
            SceneTransitionManager.LoadSceneWithDelay(1, 2f);
        }, 0.5f));
    }

    void SaveAwardCoins()
    {
        PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins") + CoinController.AwardCoinsCount);
        CoinController.ResetAwardCoinsCount();
    }
    #endregion
}