using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    #region Fields
    [SerializeField] GameObject playerBurst;

    PlayerMovementController movementController;
    MeshRenderer meshRenderer;
    Material material;
    #endregion

    #region Methods
    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = GetComponent<Renderer>().material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            GameObject gameObject = Instantiate(playerBurst, transform.position, transform.rotation, transform);
            gameObject.GetComponent<Renderer>().material = material;
            meshRenderer.enabled = movementController.enabled = false;
            //Game over
        }
    }
    #endregion
}