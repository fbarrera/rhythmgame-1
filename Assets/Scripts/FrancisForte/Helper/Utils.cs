using UnityEngine.Events;

namespace FrancisForte.Utils
{
    [System.Serializable]
    public struct SerializedEvent
    {
        public string eventName;
        public UnityEvent eventInstance;
    }

}