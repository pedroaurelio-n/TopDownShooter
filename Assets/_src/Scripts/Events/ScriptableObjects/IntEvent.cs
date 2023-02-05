using UnityEngine;
using UnityEngine.Events;

namespace PedroAurelio.EventSystem
{
    [System.Serializable]
    public class IntUnityEvent : UnityEvent<int>
    {
    }
    
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Events/Int Event")]
    public class IntEvent : GameEvent
    {
        public void RaiseEvent(int value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                var listener = listeners[i] as IntUnityEventListener;
                listener.OnEventRaised(value);
            }
        }
    }
}