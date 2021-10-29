using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Base.UI
{
    public class B_UI_CSlider_Subframe : B_UI_ComponentsSubframe
    {
        public enum SliderType { Handle, Image }

        [SerializeField] SliderType ThisSliderType = SliderType.Handle;
        Slider HandleSlider;
        Image ImageSlider;

        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            switch (ThisSliderType)
            {
                case SliderType.Handle:
                    HandleSlider = GetComponent<Slider>();
                    break;
                case SliderType.Image:
                    ImageSlider = GetComponent<Image>();
                    break;
            }
            return base.SetupComponentSubframe(Manager);
        }

        public void AddFunctionToSlider(UnityAction<float> rnd)
        {
            HandleSlider.onValueChanged.AddListener(rnd);
        }

        public override Task FlushData()
        {
            return base.FlushData();
        }
    }
}