using UnityEngine;
using UnityEngine.Audio;
using Utilities;
using System;

public class AudioMixerVolumeSetter : MonoBehaviour
{
    #region Fields
    [SerializeField] AudioMixer[] audioMixers;
    [SerializeField] string[] exposedVolumeParameters;
    [SerializeField] float[] volumeChangeMultipliers;
    #endregion

    #region Methods
    void Start()
    {
        SetAudioMixerVolume();
    }
    public void SetAudioMixerVolume()
    {
        if (audioMixers.Length != exposedVolumeParameters.Length || exposedVolumeParameters.Length != volumeChangeMultipliers.Length)
            throw new InvalidOperationException("Parameter arrays are filled incorrectly.\n" +
                "For correct filling, use the FindVolumeControllersAndGetTheirParameters() method.");

        for (int i = 0; i < audioMixers.Length; i++)
            audioMixers[i].SetFloat(exposedVolumeParameters[i],
                VolumeController.VolumeChangeFunction(PlayerPrefs.GetFloat(exposedVolumeParameters[i]), volumeChangeMultipliers[i]));
    }

    [ContextMenu("Find volume controllers and get their parameters")]
    void FindVolumeControllersAndGetTheirParameters()
    {
        VolumeController[] volumeControllers = FindObjectsOfType<VolumeController>();
        audioMixers = new AudioMixer[volumeControllers.Length];
        exposedVolumeParameters = new string[volumeControllers.Length];
        volumeChangeMultipliers = new float[volumeControllers.Length];

        for (int i = 0; i < volumeControllers.Length; i++)
        {
            audioMixers[i] = volumeControllers[i].AudioMixer;
            exposedVolumeParameters[i] = volumeControllers[i].ExposedVolumeParameter;
            volumeChangeMultipliers[i] = volumeControllers[i].VolumeChangeMultiplier;
        }
    }
    #endregion
}