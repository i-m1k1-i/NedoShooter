using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Characters.Neon
{
    public class NeonEnergyView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetValue(float sliderValue)
        {
            _slider.value = sliderValue;
        }
    }
}