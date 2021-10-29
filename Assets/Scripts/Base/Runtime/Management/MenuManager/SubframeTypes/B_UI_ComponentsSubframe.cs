using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    public abstract class B_UI_ComponentsSubframe : MonoBehaviour
    {
        public string ComponentParticularName;
        [SerializeField] B_UI_MenuSubFrame Parent;
        public Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            this.Parent = Manager;
            Debug.Log(Parent.MenuType);
            return Task.CompletedTask;
        }

        public Task FlushData()
        {
            Parent = null;
            return Task.CompletedTask;
        }

    }
}