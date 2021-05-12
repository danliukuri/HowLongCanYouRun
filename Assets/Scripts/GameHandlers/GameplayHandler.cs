using UnityEngine;
using System.Collections.Generic;
using Utilities;

public class GameplayHandler : SingletonMonoBehaviour<GameplayHandler>
{
    #region Fields
    [Tooltip("Components that need to be enabled when starting gameplay")]
    [SerializeField] List<Behaviour> componentsToEnable;
    #endregion

    #region Methods
    void Awake()
    {
        componentsToEnable.Enabled(false);
    }

    public static void StartGameplay()
    {
       Instance.componentsToEnable.Enabled(true);
    }
    public static void FinishGameplay()
    {
       Instance.componentsToEnable.Enabled(false);
    }
    #endregion
}