using UnityEngine;
using UnityEngine.Events;
 
namespace PedroAurelio.EventSystem
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