using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Base.UI {
    public class UI_CToggleSubframe : UI_TComponentsSubframe {
        #region StandartFunctions

        [HideInInspector] public Toggle ToggleComponent;

        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            ToggleComponent = GetComponent<Toggle>();
            return base.SetupComponentSubframe(Manager);
        }

        public void AddFunction(UnityAction<bool> function) {
            ToggleComponent.onValueChanged.AddListener(function);
        }

        public void ChangeText(string text) {
            ToggleComponent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        }

        #endregion
    }
}