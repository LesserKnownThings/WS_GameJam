using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuButton
{
    private Button button;

    public SubMenuButton(Button givenButton, SubMenuWindow controlledWindow, bool bIsOpenAction)
    {
        button = givenButton;

        button.onClick.AddListener(() => controlledWindow.WindowAction(bIsOpenAction));
    }
}
