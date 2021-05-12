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

    MeshRenderer meshRenderer;
    BoxCollider playerCollider;
    Transform playerTransform;
    #endregion

    #region Methods
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        playerTransform = player.transform;
        playerCollider = player.GetComponent<BoxCollider>();

        foldIntoOneWhole.GetComponent<Utilities.ParticleAttractor>().Target = playerTransform;
        foldIntoOneWhole.GetComponent<ParticleSystem>().trigger.SetCollider(0, playerTransform);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            Destroy(gameObject);
        else if (collision.collider.CompareTag(player.tag))
        {
            if (PlayerBonuses.HasShield)
                CauseBurst();
            else
                PlaceShieldOnPlayer();
        }
        else if (collision.collider.CompareTag("Shield"))
            CauseBurst();
    }
    public void OnCollisionEnterOnPlayer(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
            CauseBurstOnPlayer(collision.transform);
    }

    public void ActivateShield()
    {            
        PlayerBonuses.ShieldController = this;
        playerCollider.center = new Vector3(0f, 0.025f, 0f);
        playerCollider.size = new Vector3(1.1f, 1.05f, 1.1f);

        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        burstOnPlayer.transform.localScale = Vector3.one; 
        transform.localPosition = Vector3.zero;
        transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);

        meshRenderer.enabled = PlayerBonuses.HasShield = true;
        gameObject.tag = player.tag;
        gameObject.layer = player.layer;
        
        Destroy(burst, 1f);

        if (!player.GetComponent<MeshRenderer>().enabled)
            CauseBurst();
    }
    void CauseBurst()
    {
        burstOnPlayer.transform.SetParent(transform.parent);
        burstOnPlayer.SetActive(true);
        Destroy(gameObject);
    }
    void PlaceShieldOnPlayer()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.SetParent(playerTransform);

        burst.transform.SetParent(playerTransform.parent);
        burst.transform.localScale = Vector3.one;
        burst.SetActive(true);
        meshRenderer.enabled = false;
    }
    void CauseBurstOnPlayer(Transform obstacle)
    {
        burstOnPlayer.transform.SetParent(playerTransform);
        burstOnPlayer.SetActive(true);
        
        PlayerBonuses.HasShield = false;
        playerCollider.center = Vector3.zero;
        playerCollider.size = Vector3.one;

        Instantiate(obstacleBurst, obstacle.position, obstacle.rotation);
        Destroy(obstacle.gameObject);
        Destroy(gameObject);
    }
    #endregion
}