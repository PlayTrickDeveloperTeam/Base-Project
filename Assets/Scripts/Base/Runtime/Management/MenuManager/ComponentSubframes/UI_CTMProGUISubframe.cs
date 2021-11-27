using System.Threading.Tasks;
using TMPro;
using UnityEngine;
namespace Base.UI {
    public class UI_CTMProGUISubframe : UI_TComponentsSubframe {
        #region Standart Functions

        [HideInInspector] public TextMeshProUGUI TextComponent;
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            TextComponent = GetComponent<TextMeshProUGUI>();
            return base.SetupComponentSubframe(Manager);
        }

        public void ChangeText(object newText, string stringParameter = "") {
            TextComponent.text = newText.ToString();
        }

        #endregion
    }
}