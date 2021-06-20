using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Utilities
{
    public class VolumeController : MonoBehaviour
    {
        #region Properties
        public string ExposedVolumeParameter => exposedVolumeParameter;
        public AudioMixer AudioMixer => audioMixer; 
        public float VolumeChangeMultiplier => volumeChangeMultiplier;
        #endregion

        #region Fields
        [SerializeField] string exposedVolumeParameter;
        [SerializeField] AudioMixer audioMixer;
        [SerializeField] Slider slider;
        [SerializeField] float volumeChangeMultiplier = 40f;
        [SerializeField] Toggle toggle;

        bool disableToggleValueChangedEvent;
        float sliderDefaultValue;
        #endregion

        #region Methods
        void Awake()
        {
            sliderDefaultValue = slider.value;
            slider.onValueChanged.AddListener(SliderValueChangedHandler);
            toggle.onValueChanged.AddListener(ToggleValueChangedHandler);
        }
        void Start()
        {
            slider.value = PlayerPrefs.GetFloat(exposedVolumeParameter, sliderDefaultValue);
            SliderValueChangedHandler(slider.value);
        }
        void OnDisable()
        {
            PlayerPrefs.SetFloat(exposedVolumeParameter, slider.value);
        }

        void SliderValueChangedHandler(float value)
        {
            audioMixer.SetFloat(exposedVolumeParameter, VolumeChangeFunction(value, volumeChangeMultiplier));

            disableToggleValueChangedEvent = true;
            toggle.isOn = slider.value == slider.minValue;
            disableToggleValueChangedEvent = false;
        }
        void ToggleValueChangedHandler(bool muteSound)
        {
            if(!disableToggleValueChangedEvent)
            {
                if (muteSound)
                    slider.value = slider.minValue;
                else
                    slider.value = sliderDefaultValue;
            }
        }

        public static float VolumeChangeFunction(float value, float volumeChangeMultiplier) => Mathf.Log10(value) * volumeChangeMultiplier;
        #endregion
    }
}