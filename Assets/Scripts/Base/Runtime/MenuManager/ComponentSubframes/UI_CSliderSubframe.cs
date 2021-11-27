using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Base.UI {
    public class UI_CSliderSubframe : UI_TComponentsSubframe {
        #region Standart Functions

        [HideInInspector] public Slider HandleSlider;

        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            HandleSlider = GetComponent<Slider>();
            return base.SetupComponentSubframe(Manager);
        }

        public void AddFunctionToSlider(UnityAction<float> func) {
            HandleSlider.onValueChanged.AddListener(func);
        }

        public void ChangeSliderValue(float f) {
            HandleSlider.value = f;
        }

        public override Task FlushData() {
            return base.FlushData();
        }

        #endregion
    }
}