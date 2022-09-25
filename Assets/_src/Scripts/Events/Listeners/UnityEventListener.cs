using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
namespace TopDownShooter
{
    public class UnityEventListener : BaseEventListener
    {
        [SerializeField] private UnityEvent response;

        protected override void InvokeEvent()
        {
            response?.Invoke();
            
            base.InvokeEvent();
        }
    }
}