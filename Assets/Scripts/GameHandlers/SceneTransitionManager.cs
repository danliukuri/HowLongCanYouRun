using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    #region Fields
    Animator animator;
    static SceneTransitionManager instance;
    static int sceneIndex;
    #endregion

    #region Methods
    private void Awake()
    {
        if (instance == null)
            instance = this;
        animator = GetComponent<Animator>();
    }
    void FadeInFinished()
    {
        gameObject.SetActive(false);
    }
    void FadeOutFinished()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public static void LoadScene(int index)
    {
        instance.gameObject.SetActive(true);
        instance.animator.SetTrigger("FadeOut");
        sceneIndex = index;
    }
    public static void LoadSceneWithDelay(int index, float delayTime) => instance.StartCoroutine(
        Utilities.StaticFunctions.Invoke(() => LoadScene(index), delayTime));
    #endregion
}