using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class CopyMaterialColor : MonoBehaviour
    {
        [SerializeField] Graphic[] graphicsForChangingColors;
        [Space]
        [SerializeField] Material material;
        [SerializeField] string colorParameterName = "_Color";
        [Header("Brightness")]
        [SerializeField] bool useCustomBrightness;
        [Range(0, 1)]
        [SerializeField] float brightness;

        // Update is called once per frame
        void Update()
        {
            Color.RGBToHSV(material.GetColor(colorParameterName), out float hue, out float saturation, out float value);
            if (useCustomBrightness)
                value = brightness;
            for (int i = 0; i < graphicsForChangingColors.Length; i++)
                graphicsForChangingColors[i].color = Color.HSVToRGB(hue, saturation, value);
        }
    }
}