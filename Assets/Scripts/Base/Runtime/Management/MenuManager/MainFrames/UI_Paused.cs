﻿using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Base;
using DG.Tweening;

namespace Base.UI
{
    public class UI_Paused : B_UI_MenuSubFrame
    {
        public override Task SetupFrame(B_UI_ManagerMainFrame Mainframe)
        {
            return base.SetupFrame(Mainframe);
        }

        public override Tween EnableUI(float Time = 0, bool Snap = true)
        {
            return base.EnableUI(Time, Snap);
        }

        public override Tween DisableUI(float Time = 0, bool Snap = true)
        {
            return base.DisableUI(Time, Snap);
        }
    }
}