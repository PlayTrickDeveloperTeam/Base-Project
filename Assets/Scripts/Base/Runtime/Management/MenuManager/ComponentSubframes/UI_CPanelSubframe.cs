using System.Threading.Tasks;
namespace Base.UI {
    public class UI_CPanelSubframe : UI_TComponentsSubframe {
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            // Debug.Log("Panel");
            return base.SetupComponentSubframe(Manager);
        }
    }
}