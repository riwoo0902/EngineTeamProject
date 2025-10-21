using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lrw_EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<string, UnityEvent> Events = new();
        public static void ClearEvent(string type)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.RemoveAllListeners();
            }
            else
            {
                Debug.Log("존재하지 않는 이벤트 입니다.");
            }
        }

        public static UnityEvent GetEvent(string type)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                return thisEvent;
            }
            else
            {
                thisEvent = new UnityEvent();
                Events.Add(type, thisEvent);
                return thisEvent;
            }
        }

        public static void AddEvent(string type, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                Debug.Log("새로운 키를 생성합니다");
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(type, thisEvent);
            }
        }
        public static void RemoveEvent(string type, UnityAction listener)
        {
            if (Events.TryGetValue(type, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
            else
            {
                Debug.Log("존재하지 않는 이벤트 입니다.");
            }
        }

        public static void Invoke(string type)
        {
            if (Events.TryGetValue(type, out UnityEvent thisEvent))
            {
                thisEvent?.Invoke();
            }
        }
    }
}
