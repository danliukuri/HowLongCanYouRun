using UnityEngine;

namespace Utilities
{
    public class ChangeMaterialColor : MonoBehaviour
    {
        [SerializeField] Material material;
        [SerializeField] string colorParameterName = "_Color";
        [SerializeField] float speed = 1f;

        void Update()
        {
            Color.RGBToHSV(material.GetColor(colorParameterName), out float hue, out float saturation, out float value);
            material.SetColor(colorParameterName, Color.HSVToRGB(hue + Time.deltaTime * speed, saturation, value));
        }
    }
}