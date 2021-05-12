using UnityEngine;

public class DestructionBoundaryOfObjects : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform player;
    Vector3 offset;
    #endregion

    #region Methods
    void Start()
    {
        offset = transform.position - player.position;
    }
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
    #endregion
}