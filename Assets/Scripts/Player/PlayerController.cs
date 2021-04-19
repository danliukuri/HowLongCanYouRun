using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] GameObject playerBurst;

    PlayerMovementController movementController;
    MeshRenderer meshRenderer;

    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            playerBurst.SetActive(true);
            meshRenderer.enabled = movementController.enabled = false;
            //Game over
        }
    }
}
