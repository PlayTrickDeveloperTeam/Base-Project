using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Base {
    public static class B_CES_CentralEventSystem {
        public static List<B_CES_Events> EventsList;

        public static B_CES_Events OnGameStateChange;

        public static B_CES_Events OnAfterAwake;

        public static B_CES_Events OnBeforeLevelLoaded;
        public static B_CES_Events OnAfterLevelLoaded;
        public static B_CES_Events OnLevelActivation;

        public static B_CES_Events OnBeforeLevelEnd;
        public static B_CES_Events OnBeforeLevelDisableNegative;
        public static B_CES_Events OnBeforeLevelDisablePositive;
        public static B_CES_Events OnAfterLevelDisableNegative;
        public static B_CES_Events OnAfterLevelDisablePositive;
        public static B_CES_Events OnAfterLevelEnd;

        public static B_CES_Events BTN_OnStartPressed;
        public static B_CES_Events BTN_OnPausePressed;
        public static B_CES_Events BTN_OnMenuPressed;
        public static B_CES_Events BTN_OnRestartPressed;
        public static B_CES_Events BTN_OnEndGamePressed;

        public static B_CES_Events OnLevelDisable;

        public static Task CentralEventSystemStrapping() {
            EventsList = new List<B_CES_Events>();

            OnGameStateChange = new B_CES_Events(EventsList);

            OnBeforeLevelLoaded = new B_CES_Events(EventsList);
            OnAfterLevelLoaded = new B_CES_Events(EventsList);
            OnLevelActivation = new B_CES_Events(EventsList);

            BTN_OnStartPressed = new B_CES_Events(EventsList);
            BTN_OnPausePressed = new B_CES_Events(EventsList);
            BTN_OnMenuPressed = new B_CES_Events(EventsList);
            BTN_OnRestartPressed = new B_CES_Events(EventsList);

            OnBeforeLevelEnd = new B_CES_Events(EventsList);
            OnBeforeLevelDisableNegative = new B_CES_Events(EventsList);
            OnBeforeLevelDisablePositive = new B_CES_Events(EventsList);
            OnAfterLevelDisableNegative = new B_CES_Events(EventsList);
            OnAfterLevelDisablePositive = new B_CES_Events(EventsList);
            OnAfterLevelEnd = new B_CES_Events(EventsList);

            OnLevelDisable = new B_CES_Events(EventsList);
            BTN_OnEndGamePressed = new B_CES_Events(EventsList);

            BTN_OnStartPressed = new B_CES_Events(EventsList);

            OnLevelDisable.AddFunction(DeactiveEvents, true);
            return Task.CompletedTask;
        }

        public static void DeactiveEvents() {
            for (var i = 0; i < EventsList.Count; i++) EventsList[i].DeleteNonConstantFunctions();
        }
    }

    public class B_CES_Events {
        public Action AnyAction;
        public Dictionary<Action, bool> SubscribedFunctions;

        public B_CES_Events(List<B_CES_Events> eventList) {
            SubscribedFunctions = new Dictionary<Action, bool>();
            eventList.Add(this);
        }

        public void InvokeEvent() {
            AnyAction?.Invoke();
        }

        public void AddFunction(Action actionToAdd, bool isConstantEvent) {
            AnyAction += actionToAdd;
            SubscribedFunctions.Add(actionToAdd, isConstantEvent);
        }

        public void DeleteFunction(Action actionToRemove) {
            if (!SubscribedFunctions.ContainsKey(actionToRemove)) return;
            AnyAction -= actionToRemove;
            SubscribedFunctions.Remove(actionToRemove);
        }

        public void DeleteNonConstantFunctions() {
            foreach (var item in SubscribedFunctions) {
                if (item.Value) return;
                AnyAction -= item.Key;
            }
            SubscribedFunctions = SubscribedFunctions.Where(t => t.Value).ToDictionary(d => d.Key, d => d.Value);
        }
    }
}