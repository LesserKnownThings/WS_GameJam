using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private List<HUDWindow> windows = new List<HUDWindow>();

    public void AddSubWindow(HUDWindow windowToAdd, IHUDInteractor interactor)
    {
        HUDWindow internalWindow = Instantiate(windowToAdd, transform, false);
        internalWindow.InitHUDWindow(interactor);
        windows.Add(internalWindow);
    }
}