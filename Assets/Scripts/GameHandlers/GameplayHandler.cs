using UnityEngine;
using Utilities;

public class GameplayHandler : MonoBehaviour
{
    #region Fields
    [Tooltip("Components that need to be enabled when starting gameplay")]
    [SerializeField] Behaviour[] componentsToEnableOnStart;
    [Tooltip("Components that need to be enabled when finishing gameplay")]
    [SerializeField] Behaviour[] componentsToEnableOnFinish;
    [Tooltip("GameObjects that need to be enabled when finishing gameplay")]
    [SerializeField] GameObject[] gameObjectsToSetActiveOnFinish;

    static GameplayHandler instance;
    #endregion

    #region Methods
    void Awake()
    {
        if (instance == null)
            instance = this;
        componentsToEnableOnStart.ForAll(e => e.enabled = false);
        gameObjectsToSetActiveOnFinish.ForAll(e => e.SetActive(false));
        componentsToEnableOnFinish.ForAll(e => e.enabled = false);
    }

    public static void StartGameplay()
    {
        instance.componentsToEnableOnStart.ForAll(e => e.enabled = true);
    }
    public static void FinishGameplay()
    {
        instance.componentsToEnableOnStart.ForAll(e => e.enabled = false);
        instance.StartCoroutine(StaticFunctions.Invoke(() => 
        {
            instance.gameObjectsToSetActiveOnFinish.ForAll(e => e.SetActive(true));
            instance.componentsToEnableOnFinish.ForAll(e => e.enabled = true);
        }, 1f));
    }
    #endregion
}