using UnityEngine;

public delegate void OnActionFinishedDelegate();

public class UIManager : MonoBehaviour
{
    public OnActionFinishedDelegate OnActionFinished;

    

    private void Start()
    {
        
    }
}
