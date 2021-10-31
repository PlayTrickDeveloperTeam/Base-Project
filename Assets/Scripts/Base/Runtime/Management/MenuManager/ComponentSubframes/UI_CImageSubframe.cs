using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace Base.UI
{
    public class UI_CImageSubframe : UI_TComponentsSubframe
    {
        [HideInInspector] public Image ThisImage;

        public override Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            ThisImage = GetComponent<Image>();
            return base.SetupComponentSubframe(Manager);
        }

        public override Task FlushData()
        {
            return base.FlushData();
        }

        public void ChangeAlpha(float f)
        {
            Color col = ThisImage.color;
            col.a = f;
            ThisImage.color = col;
        }

    }
}