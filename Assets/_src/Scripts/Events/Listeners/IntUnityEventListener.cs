using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class IntUnityEventListener : BaseEventListener
    {
        [SerializeField] private IntUnityEvent response;

        public void OnEventRaised(int value)
        {
            if (_WasRaised)
                return;

            if (delay <= 0)
                InvokeEvent(value);
            else
                StartCoroutine(OnEventDelayedCoroutine(value));
        }

        private IEnumerator OnEventDelayedCoroutine(int value)
        {
            yield return new WaitForSeconds(delay);
            InvokeEvent(value);
        }

        protected virtual void InvokeEvent(int value)
        {
            response.Invoke(value);
            
            if (isOneShot)
                _WasRaised = true;
        }
    }
}