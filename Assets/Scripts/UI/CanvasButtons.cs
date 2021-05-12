using UnityEngine;
using Camera;

namespace UI
{
    public class CanvasButtons : Utilities.SingletonMonoBehaviour<CanvasButtons>
    {
        #region Fields
        [SerializeField] GameObject mainMenu;
        [SerializeField] GateController gate;

        CameraMovementToObject cameraMovementToObject;
        const float gateOpenDelayTime = 0.4f;
        #endregion

        #region Methods
        void Awake()
        {
            cameraMovementToObject = UnityEngine.Camera.main.GetComponent<CameraMovementToObject>();
        }
        public void Play()
        {
            cameraMovementToObject.enabled = true;
            mainMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        #endregion
    }
}