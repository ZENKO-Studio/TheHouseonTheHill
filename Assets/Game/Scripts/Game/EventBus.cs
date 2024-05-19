using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    private static Dictionary<Type, List<Delegate>> eventListeners = new Dictionary<Type, List<Delegate>>();

    public static void Subscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Delegate>();
        }
        eventListeners[eventType].Add(listener);
    }

    public static void Unsubscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(listener);
        }
    }

    public static void Publish<T>(T publishedEvent)
    {
        Type eventType = typeof(T);
        if (eventListeners.ContainsKey(eventType))
        {
            foreach (Delegate listener in eventListeners[eventType])
            {
                ((Action<T>)listener)(publishedEvent);
            }
        }
    }

    public class ItemAddedEvent
    {
        public InventoryItem Item { get; private set; }

        public ItemAddedEvent(InventoryItem item)
        {
            Item = item;
        }
    }

    public class ItemRemovedEvent
    {
        public InventoryItem Item { get; private set; }

        public ItemRemovedEvent(InventoryItem item)
        {
            Item = item;
        }
    }

    public class ItemInspectedEvent
    {
        public InventoryItem Item { get; private set; }

        public ItemInspectedEvent(InventoryItem item)
        {
            Item = item;
        }
    }
    public class ToggleInventoryEvent
    {
        public bool IsOpen { get; private set; }

        public ToggleInventoryEvent(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}
