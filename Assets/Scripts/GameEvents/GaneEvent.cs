using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
   private List<GameEventListener> listeners = new List<GameEventListener>();

   public void Raise()
   {
      for (int i = listeners.Count - 1; i >= 0; i--)
      {
         listeners[i].OnEventRaised();
      }
   }
   
   public void Register(GameEventListener listener)
   {
      if (!listeners.Contains(listener))
      {
         listeners.Add(listener);
      }
   }
   
   public void UnRegister(GameEventListener listener)
   {
      if (listeners.Contains(listener))
      {
         listeners.Remove(listener);
      }
   }
}
