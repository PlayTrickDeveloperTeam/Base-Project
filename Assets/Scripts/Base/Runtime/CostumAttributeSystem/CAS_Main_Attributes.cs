using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Base
{
    public enum Cas_AttributeType { Health, Stamina, Sanity, Hunger, Thirst }
    public class CAS_Main_Attributes
    {
        public Dictionary<Cas_AttributeType, Cas_Attribute> Attributes = new Dictionary<Cas_AttributeType, Cas_Attribute>()
        {
            {Cas_AttributeType.Health, new Cas_Attribute(false, 0, 100) },
            {Cas_AttributeType.Stamina, new Cas_Attribute(false, 0, 100) },
            {Cas_AttributeType.Sanity, new Cas_Attribute(false, 0, 100) },
            {Cas_AttributeType.Hunger, new Cas_Attribute(false, 0, 100) },
            {Cas_AttributeType.Thirst, new Cas_Attribute(false, 0, 100) }
        };


        public Action Attribute_Change_Decrease(Cas_AttributeType type, float value)
        {
            if (Attributes[type].MinReached) { return null; }
            Attributes[type].Value -= value;
            if (Attributes[type].Value <= Attributes[type].MinValue) Attributes[type].MinReached = true;
            if (Attributes[type].MinReached) { Attributes[type].OnValueDecreaseAction?.Invoke(); return Attributes[type].OnMinReachedAction; }
            return Attributes[type].OnValueDecreaseAction;
        }

        public Action Attribute_Change_Increase(Cas_AttributeType type, float value)
        {
            if (Attributes[type].MaxReached) { return null; }
            Attributes[type].Value += value;
            if (Attributes[type].Value >= Attributes[type].MaxValue) Attributes[type].MaxReached = true;
            if (Attributes[type].MinReached) { Attributes[type].OnValueIncreaseAction?.Invoke(); return Attributes[type].OnMaxReachedAction; }
            return Attributes[type].OnMaxReachedAction;
        }
    }

    [System.Serializable]
    public class Cas_Attribute
    {
        public bool MinReached = false;
        public bool MaxReached = true;

        public float MinValue;
        public float MaxValue;
        public float Value;

        public Action OnValueDecreaseAction;
        public Action OnValueIncreaseAction;
        public Action OnMinReachedAction;
        public Action OnMaxReachedAction;

        public Cas_Attribute(bool depleted, float minValue, float maxValue)
        {
            this.MinReached = depleted;
            this.MaxValue = maxValue;
            this.MinValue = minValue;
            this.Value = maxValue;
        }
    }
}

