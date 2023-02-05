using UnityEngine;
using UnityEngine.UI;

public class SanityMeterWindow : HUDWindow
{
    [SerializeField]
    private Image sanityMeter;

    public override void InitHUDWindow(IHUDInteractor interactor)
    {
        base.InitHUDWindow(interactor);

    }
}