using System;
using System.Collections;
using System.Collections.Generic;
using Base.UI;
using UnityEngine;
using UnityEngine.UI;

public class UI_Options : MonoBehaviour
{
    private Button OptionsButton;

    private void Awake()
    {
        OptionsButton = GetComponent<Button>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        // GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.)
        OptionsButton.onClick.AddListener(OpenMenu);
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.CloseButton).
            AddFunction(OpenMenu);
        GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OptionsMenu).AddFunction(OpenMenu);
    }

    private void Fader()
    {
        foreach (var im in GetComponentsInChildren<Image>())
        {
            if (im.color.a == 0)
            {
                im.color = new Color(im.color.r, im.color.g, im.color.b, 1);
            }
            else
            {
                im.color = new Color(im.color.r, im.color.g, im.color.b, 0);
            }

        }
    }

    private void OpenMenu()
    {
        var menu = GUIManager.GetButton(Enum_Menu_PlayerOverlayComponent.OptionsMenu).transform;
        Fader();
        if (menu.gameObject.activeSelf)
        {
            menu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            menu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
