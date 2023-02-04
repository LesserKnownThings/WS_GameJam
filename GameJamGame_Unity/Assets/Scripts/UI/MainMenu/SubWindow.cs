using System.Collections;
using UnityEngine;


public class SubWindow : MonoBehaviour
{
    [SerializeField]
    protected WindowActionType windowType;
    [SerializeField]
    private bool isMainWindow = false;

    public WindowActionType GetWindowType() { return windowType; }
    public bool IsMainWindow() { return isMainWindow; }

    public virtual void CallWindowAction()
    {
        gameObject.SetActive(true);
    }

    public virtual void BackAction()
    {
        gameObject.SetActive(false);
    }
}