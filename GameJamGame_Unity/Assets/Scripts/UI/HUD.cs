using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private List<HUDWindow> windows;

    public void AddSubWindow(HUDWindow windowToAdd, IHUDInteractor interactor)
    {
        windowToAdd.InitHUDWindow(interactor);
        windows.Add(windowToAdd);
    }
}