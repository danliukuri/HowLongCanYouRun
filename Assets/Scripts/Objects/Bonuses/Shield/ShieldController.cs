using UnityEngine;

public class ShieldController : MonoBehaviour
{
    #region Properties
    public GameObject Player { get => player; set => player = value; }
    #endregion

    #region Fields
    [SerializeField] GameObject burst;
    [SerializeField] GameObject burstOnPlayer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject obstacleBurst;
    [SerializeField] GameObject foldIntoOneWhole;

    bool isShieldOnPlayer;
    MeshRenderer meshRenderer;
    Rigidbody rgdbody;

    Rigidbody playerRigidbody;
    Transform playerTransform;
    PlayerBonuses playerBonuses;
    #endregion

    #region Methods
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rgdbody = GetComponent<Rigidbody>();
        playerTransform = player.transform;
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerBonuses = player.GetComponent<PlayerBonuses>();

        foldIntoOneWhole.GetComponent<ParticleAttractor>().Target = playerTransform;
        foldIntoOneWhole.GetComponent<ParticleSystem>().trigger.SetCollider(0, playerTransform);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            CauseBurstOnPlayer(collision.transform);
        else if (collision.collider.CompareTag(player.tag))
        {
            if (playerBonuses.HasShield)
                CauseBurst();
            else
                PlaceShieldOnPlayer();
        }
        else if (collision.collider.CompareTag("Shield") && !isShieldOnPlayer)
            CauseBurst();
    }

    public void ActivateShield()
    {
        playerRigidbody.isKinematic = true;
        playerRigidbody.detectCollisions = false;

        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        transform.localPosition = Vector3.zero;
        transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
        burstOnPlayer.transform.SetParent(playerTransform);

        meshRenderer.enabled = true;
        Destroy(burst, 1f);
        
        rgdbody.detectCollisions = true;
        rgdbody.isKinematic = false;
    }
    void CauseBurst()
    {
        burstOnPlayer.transform.SetParent(transform.parent);
        burstOnPlayer.SetActive(true);
        Destroy(gameObject);
    }
    void PlaceShieldOnPlayer()
    {
        rgdbody.isKinematic = true;
        rgdbody.detectCollisions = false;

        transform.SetParent(playerTransform);
        burst.transform.SetParent(playerTransform.parent);
        burst.transform.localScale = Vector3.one;

        burst.SetActive(true);
        meshRenderer.enabled = false;
        isShieldOnPlayer = playerBonuses.HasShield = true;
    }
    void CauseBurstOnPlayer(Transform obstacle)
    {
        burstOnPlayer.SetActive(true);
        playerBonuses.HasShield = false;

        Instantiate(obstacleBurst, obstacle.position, obstacle.rotation);
        Destroy(obstacle.gameObject);
        Destroy(gameObject);

        playerRigidbody.detectCollisions = true;
        playerRigidbody.isKinematic = false;
    }
    #endregion
}