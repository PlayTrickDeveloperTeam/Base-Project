using System.Collections;
using UnityEngine;

namespace Base
{
    public class B_CR_CoroutineRunner : MonoBehaviour
    {
        public static B_CR_CoroutineRunner instance;
        public B_CR_CoroutineQueue CQ;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
        }

        public void CoroutineRunnerStrapping()
        {
            CQ = new B_CR_CoroutineQueue(this);
            CQ.StartLoop();
        }

        private void OnDisable()
        {
            instance = null;
        }
    }
}