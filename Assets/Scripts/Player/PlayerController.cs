using UnityEngine;
using Utilities;

public class PlayerController : MonoBehaviour
{   
    #region Fields
    [SerializeField] GameObject playerBurst;

    PlayerMovementController movementController;
    MeshRenderer meshRenderer;
    Rigidbody rgdbody;
    float minCenterOfMassInYforBalance;
    #endregion

    #region Methods
    void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        meshRenderer = GetComponent<MeshRenderer>();
        rgdbody = GetComponent<Rigidbody>();
        minCenterOfMassInYforBalance = rgdbody.worldCenterOfMass.y - 0.05f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (PlayerBonuses.HasShield)
            PlayerBonuses.ShieldController.OnCollisionEnterOnPlayer(collision);
        else if (collision.collider.CompareTag("Obstacle"))
            PlayerCrashDeath();
    }

    void Update()
    {
        // Check if the player falls
        if (rgdbody.worldCenterOfMass.y < minCenterOfMassInYforBalance)
            PlayerFallsDeath();
    }

    void PlayerCrashDeath()
    {
        GameObject gameObject = Instantiate(playerBurst, transform.position, transform.rotation, transform);
        gameObject.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        meshRenderer.enabled = false;

        StartCoroutine(StaticFunctions.Invoke(() =>
            Camera.main.GetComponent<CameraController>().MoveAndRotateToTheFloor(), 1f));

        AudioManager.PlayInPosition("ObstacleCrash", transform.position);
        PlayerDeath();
    }
    void PlayerFallsDeath()
    {
        if (PlayerBonuses.HasShield)
            PlayerBonuses.HasShield = false;
        PlayerDeath();
    }
    void PlayerDeath()
    {
        movementController.enabled = this.enabled = false;
        AudioManager.PlayInPosition("PlayerDeath", transform.position);
        GameplayHandler.FinishGameplay();
    }
    #endregion
}