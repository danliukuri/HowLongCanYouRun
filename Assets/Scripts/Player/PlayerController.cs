using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    #region Fields
    [SerializeField] GameObject playerBurst;

    PlayerMovementController movementController;
    MeshRenderer meshRenderer;
    Material material;
    Rigidbody rgdbody;
    float minCenterOfMassInYforBalance;
    #endregion

    #region Methods
    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = GetComponent<Renderer>().material;
        rgdbody = GetComponent<Rigidbody>();
        minCenterOfMassInYforBalance = rgdbody.worldCenterOfMass.y - 0.05f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (PlayerBonuses.HasShield)
            PlayerBonuses.ShieldController.OnCollisionEnterOnPlayer(collision);
        else if (collision.collider.CompareTag("Obstacle"))
        {
            GameObject gameObject = Instantiate(playerBurst, transform.position, transform.rotation, transform);
            gameObject.GetComponent<Renderer>().material = material;
            meshRenderer.enabled = movementController.enabled = false;
            //Game over
        }
    }

    void Update()
    {
        // Check if the player falls
        if (rgdbody.worldCenterOfMass.y < minCenterOfMassInYforBalance)
            movementController.enabled = false;
    }
    #endregion
}