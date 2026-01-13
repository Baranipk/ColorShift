using System.Collections.Generic;
using UnityEngine;

public static class EventBus<T> where T : IEvent
{
    private static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

    public static void Subscribe(IEventBinding<T> binding) => bindings.Add(binding);
    public static void UnSubscribe(IEventBinding<T> binding) => bindings.Remove(binding);

    public static void Publish(T eventToPublish)
    {
        foreach (var binding in bindings)
        {
            binding.OnEvent.Invoke(eventToPublish);
            binding.OnEventNoArgs.Invoke();
        }
    }

    private static void Clear()
    {
        bindings.Clear();
        Debug.Log("Clearing" + typeof(T).Name + " Bindings");
    }
}
