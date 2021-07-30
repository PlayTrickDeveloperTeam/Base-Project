using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Base
{
    public class CoroutineQueue
    {
        MonoBehaviour m_Owner = null;
        Coroutine m_InternalCoroutine = null;
        Queue<IEnumerator> actions = new Queue<IEnumerator>();
        public CoroutineQueue(MonoBehaviour aCoroutineOwner)
        {
            m_Owner = aCoroutineOwner;
        }
        public void StartLoop()
        {
            m_InternalCoroutine = m_Owner.StartCoroutine(Process());
        }
        public void StopLoop()
        {
            m_Owner.StopCoroutine(m_InternalCoroutine);
            m_InternalCoroutine = null;
        }
        public void EnqueueAction(IEnumerator aAction)
        {
            actions.Enqueue(aAction);
        }

        public void EnqueueWait(float aWaitTime)
        {
            actions.Enqueue(Wait(aWaitTime));
        }

        private IEnumerator Wait(float aWaitTime)
        {
            yield return new WaitForSeconds(aWaitTime);
        }

        private IEnumerator Process()
        {
            while (true)
            {
                if (actions.Count > 0)
                    yield return m_Owner.StartCoroutine(actions.Dequeue());
                else
                    yield return null;
            }
        }

        public void RunCoroutine(Coroutine coroutine, IEnumerator enumerator)
        {
            if (coroutine == null)
            {
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
            else
            {
                m_Owner.StopCoroutine(coroutine);
                coroutine = null;
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
        }
    }
}