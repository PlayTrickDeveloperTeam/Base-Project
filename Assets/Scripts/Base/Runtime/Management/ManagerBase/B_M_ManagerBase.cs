using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Base
{
    public abstract class B_M_ManagerBase : MonoBehaviour
    {
        public virtual Task ManagerStrapping() => Task.CompletedTask;
        public virtual Task ManagerDataFlush() => Task.CompletedTask;
    }
}