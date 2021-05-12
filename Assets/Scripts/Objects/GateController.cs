using System.Collections;
using UnityEngine;

public class GateController : MonoBehaviour
{
    #region Fields
    Animator animator;
    #endregion

    #region Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    public void OpenTheGate(float delayTime) => StartCoroutine(AnimatorEnabled(true, delayTime));
    IEnumerator AnimatorEnabled(bool value, float delayTime)
    {
        yield return new WaitForSeconds(delayTime); //Wait for the specified delay time before continuing.
        animator.enabled = value;
    }
    #endregion
}