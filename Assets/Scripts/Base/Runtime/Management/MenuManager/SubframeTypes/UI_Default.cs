using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.UI
{
    public class UI_Default : B_UI_MenuSubFrame
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