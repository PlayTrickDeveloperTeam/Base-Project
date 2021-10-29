using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System.Threading.Tasks;

public class Tester : MonoBehaviour
{
    private void Start()
    {
        string x = B_UI_ManagerMainFrame.instance.GameOverMenu().__test;
        B_UI_ManagerMainFrame.instance.MenuMain().GetText(Enum_UIComponentFrames.Hello_World_0).ChangeText("Testing_1");
        B_UI_ManagerMainFrame.instance.MenuMain().GetText(Enum_UIComponentFrames.Hello_World_1).ChangeText("Testing_2");
        B_UI_ManagerMainFrame.instance.MenuPaused().GetText(Enum_UIComponentFrames.Example_Name_0).ChangeText(x);
    }

    async void TestIt()
    {

    }
}
