using UnityEngine;

public class FoldingShieldIntoOneWhole : MonoBehaviour
{
    [SerializeField] ShieldController shieldController;
    private void OnParticleSystemStopped()
    {
        if (PlayerBonuses.HasShield)
            Destroy(gameObject);
        else
            shieldController.ActivateShield();
    }
}