using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanityMeterWindow : HUDWindow
{
    [SerializeField]
    private Slider sanityMeter;

    public override void InitHUDWindow(IHUDInteractor interactor)
    {
        base.InitHUDWindow(interactor);

        if(interactor != null)
        {
            PlayerSanityComponent sanityComp = (PlayerSanityComponent)interactor.GetOwner();
            sanityComp.OnSanityChange += OnSanityChanged;
        }
    }

    private void OnSanityChanged(int newValue)
    {
        sanityMeter.value = newValue;
    }
}