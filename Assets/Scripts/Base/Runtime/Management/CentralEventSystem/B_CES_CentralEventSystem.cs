﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base
{
    public static class B_CES_CentralEventSystem
    {
        public static List<B_CES_Events> EventsList;

        public static B_CES_Events OnBeforeLevelLoaded;
        public static B_CES_Events OnAfterLevelLoaded;
        public static B_CES_Events OnLevelActivation;
        public static B_CES_Events OnLevelDisable;

        public static B_CES_Events OnBeforeLevelDisableNegative;
        public static B_CES_Events OnBeforeLevelDisablePositive;
        public static B_CES_Events OnAfterLevelDisableNegative;
        public static B_CES_Events OnAfterLevelDisablePositive;


        public static B_CES_Events BTN_OnStartPressed;
        public static B_CES_Events BTN_OnPausePressed;
        public static B_CES_Events BTN_OnEndPressed;
        public static B_CES_Events BTN_OnMenuPressed;
        public static B_CES_Events BTN_OnRestartPressed;

        public static B_CES_Events OnLevelEnded;



        public static void CentralEventSystemStrapping()
        {
            EventsList = new List<B_CES_Events>();

            OnBeforeLevelLoaded = new B_CES_Events(EventsList);
            OnAfterLevelLoaded = new B_CES_Events(EventsList);
            OnLevelActivation = new B_CES_Events(EventsList);
            OnLevelDisable = new B_CES_Events(EventsList);

            BTN_OnStartPressed = new B_CES_Events(EventsList);
            BTN_OnPausePressed = new B_CES_Events(EventsList);
            BTN_OnEndPressed = new B_CES_Events(EventsList);
            BTN_OnMenuPressed = new B_CES_Events(EventsList);
            BTN_OnRestartPressed = new B_CES_Events(EventsList);

            OnLevelEnded = new B_CES_Events(EventsList);

            BTN_OnStartPressed = new B_CES_Events(EventsList);

            OnLevelDisable.AddFunction(DeactiveEvents, true);
        }


        public static void DeactiveEvents()
        {
            for (int i = 0; i < EventsList.Count; i++)
            {
                EventsList[i].DeleteNonConstantFunctions();
            }
        }
    }

    public class B_CES_Events
    {
        public Action AnyAction;
        public Dictionary<Action, bool> SubscribedFunctions;

        public B_CES_Events(List<B_CES_Events> eventList)
        {
            this.SubscribedFunctions = new Dictionary<Action, bool>();
            eventList.Add(this);
        }

        public void InvokeEvent()
        {
            this.AnyAction?.Invoke();
        }

        public void AddFunction(Action actionToAdd, bool isConstantEvent)
        {
            this.AnyAction += actionToAdd;
            this.SubscribedFunctions.Add(actionToAdd, isConstantEvent);

        }

        public void DeleteFunction(Action actionToRemove)
        {
            if (!this.SubscribedFunctions.ContainsKey(actionToRemove)) return;
            this.AnyAction -= actionToRemove;
            this.SubscribedFunctions.Remove(actionToRemove);
        }

        public void DeleteNonConstantFunctions()
        {
            foreach (var item in SubscribedFunctions)
            {
                if (item.Value) return;
                AnyAction -= item.Key;
            }
            this.SubscribedFunctions = this.SubscribedFunctions.Where(t => t.Value == true).ToDictionary(d => d.Key, d => d.Value);
        }
    }

}