using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    [System.Serializable]
    public class Mfloat : ISerializationCallbackReceiver
    {
        [SerializeField] private string Name = "Enter Name";
        [SerializeField] private float Value;
        [HideInInspector] public string ConnectedName;
        [HideInInspector] public float ConnectedValue;
        Dictionary<int, Mfloat> testingDic;
        bool InGame = false;

        public void PrepareUI()
        {
            ConnectedName = Name;
            ConnectedValue = Value;
            ModifiableValueManager.instance.AddToList(this);
            InGame = true;
        }


        public void OnBeforeSerialize()
        {
            if (InGame) return;

            ConnectedName = testingDic[1].Name;
            ConnectedValue = testingDic[1].Value;
            Name = testingDic[1].Name;
            Value = testingDic[1].Value;
        }
        public void OnAfterDeserialize()
        {
            if (InGame) return;
            testingDic = new Dictionary<int, Mfloat>();
            testingDic.Add(1, this);
        }


    }
}