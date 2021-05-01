using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
    #region Properties
    public Transform Target { get => target; set => target = value; }
    #endregion

    #region Fields
    [Tooltip("The attractor target")]
    [SerializeField] Transform target;

    [Tooltip("Normalized threshold on the particle lifetime\n" + 
             "0: affect particles right after they are born\n" + 
             "1: never affect particles")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float activationThreshold;

    // The particle system to operate on
    ParticleSystem affectedParticles;
    Vector3 targetPosition;
    // Array to store particles info
    ParticleSystem.Particle[] particlesArray;
    // Is this particle system simulating in world space?
    bool isWolrdSpace;
    // Multiplier to normalize movement cursor after treshold is crossed
    float cursorMultiplier;
    // To store how many particles are active on each frame
    int activeParticlesCount;
    // A cursor for the movement interpolation
    float cursor;
    #endregion

    #region Methods
    void Awake()
    {
        // Cache the particle system
        affectedParticles = GetComponent<ParticleSystem>();

        // Setup particle system info
        ParticleSystem.MainModule mainModule = affectedParticles.main;

        // Prepare enough space to store particles info
        particlesArray = new ParticleSystem.Particle[mainModule.maxParticles];
        // Is the particle system working in world space? Let's store this info
        isWolrdSpace = mainModule.simulationSpace == ParticleSystemSimulationSpace.World;
        // This the ratio of the total lifetime cursor to the "over threshold" section
        cursorMultiplier = 1.0f / (1.0f - activationThreshold);
    }

    void LateUpdate()
    {
        // Let's fetch active particles info
        activeParticlesCount = affectedParticles.GetParticles(particlesArray);

        // The attractor's target is it's world space position
        targetPosition = target.position;
        // If the system is not simulating in world space, let's project the attractor's target in the system's local space
        if (!isWolrdSpace)
            targetPosition -= affectedParticles.transform.position;

        // For each active particle...
        for (int iParticle = 0; iParticle < activeParticlesCount; iParticle++)
        { 
            // The movement cursor is the opposite of the normalized particle's lifetime 
            cursor = 1.0f - (particlesArray[iParticle].remainingLifetime / particlesArray[iParticle].startLifetime); 
            // Are we over the activation treshold? 
            if (cursor >= activationThreshold)
            {
                // Let's project the overall cursor in the "over threshold" normalized space
                cursor -= activationThreshold;
                cursor *= cursorMultiplier;

                // Take over the particle system imposed velocity
                particlesArray[iParticle].velocity = Vector3.zero;
                // Interpolate the movement towards the target with a nice quadratic easing					
                particlesArray[iParticle].position = Vector3.Lerp(particlesArray[iParticle].position, targetPosition, cursor * cursor);
            }
        }

        // Let's update the active particles
        affectedParticles.SetParticles(particlesArray, activeParticlesCount);
    }
    #endregion
}