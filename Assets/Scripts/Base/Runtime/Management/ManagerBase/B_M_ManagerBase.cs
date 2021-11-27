using System.Threading.Tasks;
using UnityEngine;
namespace Base {
    public abstract class B_M_ManagerBase : MonoBehaviour {
        public virtual Task ManagerStrapping() {
            return Task.CompletedTask;
        }
        public virtual Task ManagerDataFlush() {
            return Task.CompletedTask;
        }
    }
}