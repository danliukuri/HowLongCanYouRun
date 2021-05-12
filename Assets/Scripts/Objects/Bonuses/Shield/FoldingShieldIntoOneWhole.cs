using UnityEngine;

public class FoldingShieldIntoOneWhole : MonoBehaviour
{
    [SerializeField] ShieldController shieldController;
    private void OnParticleSystemStopped()
    {
        if (PlayerBonuses.HasShield)
            Destroy(shieldController.gameObject);
        else
            shieldController.ActivateShield();
    }
}