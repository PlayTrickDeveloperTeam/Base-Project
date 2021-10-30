using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Base.UI
{
    public class B_UI_CTMProGUIButton_Subframe : B_UI_ComponentsSubframe
    {
        #region Standart Functions

        Button Button;
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            Button = GetComponent<Button>();
            return base.SetupComponentSubframe(Manager);
        }

        public override Task FlushData()
        {
            return base.FlushData();
        }

        public void AddFunction(UnityAction function)
        {
            Button.onClick.AddListener(function);
        }
        #endregion
    }
}