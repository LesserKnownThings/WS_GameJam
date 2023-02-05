using System.Collections;
using UnityEngine;


public class SubWindow : MonoBehaviour
{
    [SerializeField]
    protected WindowActionType windowType;
    [SerializeField]
    private bool isMainWindow = false;

    [SerializeField]
    [Tooltip("This will be used for widgets that don't belong to the UI Manager flow, but can be still called from it\nBe careful, if your widget is orphaned it will have to have its own flow to show/hide")]
    private bool isOrphanWidget = false;

    public WindowActionType GetWindowType() { return windowType; }
    public bool IsMainWindow() { return isMainWindow; }
    public bool IsOrphanWidget() { return isOrphanWidget; }

    private void OnValidate()
    {
        if(isOrphanWidget)
        {
            isMainWindow = false;
        }

        if(isMainWindow)
        {
            isOrphanWidget = false;
        }
    }


    public virtual void CallWindowAction()
    {
        gameObject.SetActive(true);
    }

    public virtual void BackAction()
    {
        gameObject.SetActive(false);
    }
}