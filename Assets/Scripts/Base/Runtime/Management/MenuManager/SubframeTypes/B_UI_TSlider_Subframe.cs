using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Base.UI
{
    public abstract class B_UI_TSlider_Subframe : B_UI_ComponentsSubframe
    {
        public enum SliderType { Handle, Image }

        [SerializeField] SliderType ThisSliderType = SliderType.Handle;
        [SerializeField] Slider Slider;

    }
}