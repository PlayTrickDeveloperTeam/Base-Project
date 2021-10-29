using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System.Threading.Tasks;
using UnityEngine.UI;
using Base;
public class Tester : MonoBehaviour
{
    private void Start()
    {
        string x = B_UI_SMF_MainFrame.GameOver.__test;
        B_UI_SMF_MainFrame.Paused.GetText(Enum_ComponentsMenu_Paused.Example_Name_0).ChangeText(x);
        B_UI_SMF_MainFrame.Main.GetText(Enum_ComponentsMenu_Main.Hello_World_1).GetGUIObject().text = x + x;
        B_UI_SMF_MainFrame.Loading.GetSlider(Enum_ComponentsMenu_Loading.SliderA).AddFunctionToSlider(changeTextValue);
    }

    void changeTextValue(float f)
    {
        f = f.Remap(0f, 1f, 100f, 1000f);
        B_UI_SMF_MainFrame.Main.GetText(Enum_ComponentsMenu_Main.Hello_World_0).ChangeText(f.ToString("0"));
    }
}
