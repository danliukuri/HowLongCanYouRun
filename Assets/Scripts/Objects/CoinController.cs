using UnityEngine;

public class CoinController : MonoBehaviour
{
    #region Fields
    Animator animator;
    Collider coinCollider;
    #endregion

    #region Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        coinCollider = GetComponent<Collider>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Shield"))
            Destroy(gameObject);
        else if (collision.collider.CompareTag("Player"))
            CoinPick();
    }

    void CoinPick()
    {
        coinCollider.enabled = false;
        animator.SetTrigger("CoinPick");
        Destroy(gameObject, 1f);
    }
    #endregion
}