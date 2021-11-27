using System;
using System.Collections.Generic;
using UnityEngine;
namespace Base {
    public class ModifiableValueManager : MonoBehaviour {
        public static ModifiableValueManager instance;
        public ValueHolder ValueHolder;
        public GameObject UIPrefab;
        public GameObject UIAddMenu;
        private List<Mfloat> ValueList;
        private void Awake() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            ValueList = new List<Mfloat>();
        }

        private void Start() {
            ValueHolder.PrepareNumbers();
            foreach (var item in ValueList) {
                var obj = Instantiate(UIPrefab, UIAddMenu.transform).GetComponent<RectTransform>();
                obj.GetComponent<ValueInputField>().SetupValueInputField(item);
            }
        }

        private void OnDisable() {
            instance = null;
        }

        public void AddToList(Mfloat value) {
            ValueList.Add(value);
        }
    }

    [Serializable]
    public class ValueHolder {
        //You must instantiate these values in the code, otherwise it wont work
        public Mfloat Health;
        public Mfloat Stamina;
        public Mfloat RunSpeed;
        public Mfloat JumpForce;
        public Mfloat SidewaySpeed;
        public Mfloat AttackPower;
        public Mfloat AttackSpeed;

        public void PrepareNumbers() {
            Health.PrepareUI();
            Stamina.PrepareUI();
            RunSpeed.PrepareUI();
            JumpForce.PrepareUI();
            SidewaySpeed.PrepareUI();
            AttackPower.PrepareUI();
            AttackSpeed.PrepareUI();
        }
    }
}