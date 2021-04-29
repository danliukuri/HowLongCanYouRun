using UnityEngine;

public class FoldingShieldIntoOneWhole : MonoBehaviour
{
    [SerializeField] ShieldController shieldController;
    private void OnParticleSystemStopped()
    {
        shieldController.ActivateShield();
    }
}