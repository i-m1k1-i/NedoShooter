using UnityEngine;
using UnityEngine.UI;

namespace Nedoshooter.Agents.UI
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