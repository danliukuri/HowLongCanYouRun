﻿using UnityEngine;
using Utilities;

namespace UI
{
    public class CanvasButtons : MonoBehaviour
    {
        #region Fields
        [Header("Camera move and rotate to target behaviours")]
        [SerializeField] MoveAndRotateToTargetBehaviour cameraMoveAndRotateToStartPositionBehaviour;
        #endregion

        #region Methods
        public void Play(GateController gateToOpen)
        {
            cameraMoveAndRotateToStartPositionBehaviour.enabled = true;
            gateToOpen.OpenTheGate(0.4f);
            AudioManager.FadeOutAndStop("MenuTheme", 0.5f);
        }
        public void Quit(MoveAndRotateToTargetBehaviour behaviourToEnable)
        {
            behaviourToEnable.enabled = true;
            StartCoroutine(StaticFunctions.Invoke(() => SceneTransitionManager.FadeOut(() => ApplicationQuit()), 0.2f));
        }
        void ApplicationQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.OpenURL("https://yuriy-danyliuk.itch.io/");
#else
            Application.Quit();
#endif
        }

        public void GoToMainMenu(MoveAndRotateToTargetBehaviour behaviourToEnable) => GoToScene(0, 0.4f, behaviourToEnable);
        public void GoShop(MoveAndRotateToTargetBehaviour behaviourToEnable) => GoToScene(2, 0.2f, behaviourToEnable);
        public void GoToSettings(MoveAndRotateToTargetBehaviour behaviourToEnable) => GoToScene(3, 0.4f, behaviourToEnable);

        public void TryToGoToTheAwardsScene()
        {
            int loadSceneIndex = CoinController.AwardCoinsCount > 0 ? 1 : 0;
            StartCoroutine(StaticFunctions.Invoke(() => SceneTransitionManager.LoadScene(loadSceneIndex), 2f));
        }
        void GoToScene(int index, float delayTime, MoveAndRotateToTargetBehaviour behaviourToEnable)
        {
            behaviourToEnable.enabled = true;
            StartCoroutine(StaticFunctions.Invoke(() => SceneTransitionManager.LoadScene(index), delayTime));
        }

        public void PlayButtonClickSound() => AudioManager.Play("UIButtonClick");
        #endregion
    }
}