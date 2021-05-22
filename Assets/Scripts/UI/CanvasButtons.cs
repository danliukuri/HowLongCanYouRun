using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace UI
{
    public class CanvasButtons : MonoBehaviour
    {
        #region Fields
        [SerializeField] CanvasGroup mainMenuCanvas;
        [SerializeField] GateController gate;

        [Header("Camera move and rotate to target behaviours")]
        [SerializeField] MoveAndRotateToTargetBehaviour cameraMoveAndRotateToStartPositionBehaviour;
        [SerializeField] MoveAndRotateToTargetBehaviour cameraMoveAndRotateToFloorBehaviour;

        const float gateOpenDelayTime = 0.4f;
        #endregion

        #region Methods
        public void Play()
        {
            Camera.main.GetComponents<MoveAndRotateToTargetBehaviour>().ForAll(e => e.enabled = false);
            cameraMoveAndRotateToStartPositionBehaviour.enabled = true;

            mainMenuCanvas.blocksRaycasts = false;
            gate.OpenTheGate(gateOpenDelayTime);
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.OpenURL("https://yuriy-danyliuk.itch.io/");
#else
            Application.Quit();
#endif
        }

        public void GoToMainMenu()
        {
            Camera.main.GetComponents<MoveAndRotateToTargetBehaviour>().ForAll(e => e.enabled = false);
            cameraMoveAndRotateToFloorBehaviour.enabled = true;
        }
        public void LoadScene(int index) => SceneManager.LoadScene(index); 
        #endregion
    }
}