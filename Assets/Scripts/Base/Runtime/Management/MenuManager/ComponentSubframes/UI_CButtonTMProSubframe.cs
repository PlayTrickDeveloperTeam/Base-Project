using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Base.UI {
    public class UI_CButtonTMProSubframe : UI_TComponentsSubframe {
        #region Standart Functions

        [HideInInspector] public Button Button;
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            Button = GetComponent<Button>();
            return base.SetupComponentSubframe(Manager);
        }

        public override Task FlushData() {
            return base.FlushData();
        }

        public void AddFunction(UnityAction function) {
            Button.onClick.AddListener(function);
        }

        #endregion
    }
}