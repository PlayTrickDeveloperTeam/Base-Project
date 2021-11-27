using System.Threading.Tasks;
using UnityEngine;
namespace Base.UI {
    public class UI_CPanelSubframe : UI_TComponentsSubframe {
        [SerializeField] private bool SafeArea;
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            if (SafeArea) {
                this.AddSafeArea();
            }
            return base.SetupComponentSubframe(Manager);
        }
    }
}