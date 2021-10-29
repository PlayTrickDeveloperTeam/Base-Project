using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

namespace Base.UI
{
    public class B_UI_CTMProGUISubframe : B_UI_ComponentsSubframe
    {
        TextMeshProUGUI TextComponent;
        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            TextComponent = GetComponent<TextMeshProUGUI>();
            return base.SetupComponentSubframe(Manager);
        }

        public void ChangeText(object newText, string stringParameter = "")
        {
            TextComponent.text = newText.ToString();
        }
    }
}