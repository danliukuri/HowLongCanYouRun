using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class VolumeChangeAudioExamplesManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] string audioName;
        [SerializeField] float timeAfterWhichToStopAudio = 1f;
        [SerializeField] float fadeoutRateWhenStoppingAudio = 1f;
        AudioSource audioSource;
        #endregion

        #region Methods
        void Start()
        {
            audioSource = AudioManager.FindAudioSource(audioName);
            GetComponent<Slider>().onValueChanged.AddListener(PlayExampleAudio);
        }
        public void PlayExampleAudio(float sliderValue)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
            if (audioSource.isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(StaticFunctions.Invoke(() =>
                {
                    if(audioSource.isPlaying)
                        AudioManager.FadeOutAndStop(audioSource, fadeoutRateWhenStoppingAudio);
                }, timeAfterWhichToStopAudio));
            }
        }
        #endregion
    }
}