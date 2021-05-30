using UnityEngine;
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
        void GoToScene(int index, float delayTime, MoveAndRotateToTargetBehaviour behaviourToEnable)
        {
            behaviourToEnable.enabled = true;
            StartCoroutine(StaticFunctions.Invoke(() => SceneTransitionManager.LoadScene(index), delayTime));
        }
        #endregion
    }
}