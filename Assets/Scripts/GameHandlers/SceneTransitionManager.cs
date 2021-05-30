using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    #region Fields
    Animator animator;
    static SceneTransitionManager instance;
    static Action fadeOutCompletionAction;
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
    void FadeOutFinished() => fadeOutCompletionAction?.Invoke();
    public static void FadeOut(Action completionAction)
    {
        instance.gameObject.SetActive(true);
        instance.animator.SetTrigger("FadeOut");
        fadeOutCompletionAction = completionAction;
    }
    public static void LoadScene(int index) => FadeOut(() => SceneManager.LoadScene(index));
    #endregion
}