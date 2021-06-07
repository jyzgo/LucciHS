using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Has to AddListener in awake
/// </summary>
public interface INotifyListener
{
    void OnNotifying(string eventName,params object[] data);

}

namespace NotifyManagerNS
{
    public class NotifyManager
    {

        private Dictionary<string, List<INotifyListener>> _eventDictionary = new Dictionary<string, List<INotifyListener>>(20);
        private static NotifyManager instance = null;

        private NotifyManager()
        {
        }

        public static NotifyManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotifyManager();
                }
                return instance;
            }
        }
        public static void AddListener(string eventName, INotifyListener listener)
        {
            List<INotifyListener> listenerList = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out listenerList))
            {
                listenerList.Add(listener);
            }
            else
            {
                listenerList = new List<INotifyListener>();
                listenerList.Add(listener);
                Instance._eventDictionary.Add(eventName, listenerList);
            }
        }

        public static void RemoveListener(string eventName, INotifyListener listener)
        {
            if (Instance == null) return;
            List<INotifyListener> thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Remove(listener);
            }
        }



        public static void Post(string eventName, params object[] x)
        {
            List<INotifyListener> thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                foreach (var n in thisEvent)
                {
                    n.OnNotifying(eventName,x);
                }
            }
        }


    }

}