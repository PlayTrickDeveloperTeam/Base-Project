using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base {
    public class B_CR_CoroutineQueue {
        private readonly Queue<IEnumerator> actions = new Queue<IEnumerator>();
        private Coroutine m_InternalCoroutine;
        private readonly MonoBehaviour m_Owner;

        public B_CR_CoroutineQueue(MonoBehaviour aCoroutineOwner) {
            m_Owner = aCoroutineOwner;
        }

        public void StartLoop() {
            m_InternalCoroutine = m_Owner.StartCoroutine(Process());
        }

        public void StopLoop() {
            m_Owner.StopCoroutine(m_InternalCoroutine);
            m_InternalCoroutine = null;
        }

        public void EnqueueAction(IEnumerator aAction) {
            actions.Enqueue(aAction);
        }

        public void EnqueueWait(float aWaitTime) {
            actions.Enqueue(Wait(aWaitTime));
        }

        private IEnumerator Wait(float aWaitTime) {
            yield return new WaitForSeconds(aWaitTime);
        }

        private IEnumerator Process() {
            while (true)
                if (actions.Count > 0)
                    yield return m_Owner.StartCoroutine(actions.Dequeue());
                else
                    yield return null;
        }

        public void RunCoroutine(Coroutine coroutine, IEnumerator enumerator) {
            if (coroutine == null) {
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
            else {
                m_Owner.StopCoroutine(coroutine);
                coroutine = null;
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
        }

        public void RunCoroutineWithDelay(Coroutine coroutine, IEnumerator enumator, float waitTime) {
            m_Owner.StartCoroutine(Ienum_DelayStartIenum(coroutine, enumator, waitTime));
        }

        private IEnumerator Ienum_DelayStartIenum(Coroutine coroutine, IEnumerator enumerator, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            if (coroutine == null) {
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
            else {
                m_Owner.StopCoroutine(coroutine);
                coroutine = null;
                coroutine = m_Owner.StartCoroutine(enumerator);
            }
        }

        public void RunFunctionWithDelay(Action method, float waitTime) {
            m_Owner.StartCoroutine(Ienum_DelayStartFunction(method, waitTime));
        }

        private IEnumerator Ienum_DelayStartFunction(Action method, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            method?.Invoke();
        }
    }
}