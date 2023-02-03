using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class SubMenuWindow : MonoBehaviour
{
    private CanvasGroup cG;
    private float transitionSpeed = 2.5f;

    private Coroutine actionRoutine;

    private void Start()
    {
        cG = GetComponent<CanvasGroup>();
    }

    public void Initialize(float givenTransitionSpeed)
    {
        transitionSpeed = givenTransitionSpeed;
    }

    public void WindowAction(bool bIsOpening)
    {
        if (actionRoutine == null)
        {
            actionRoutine = StartCoroutine(WindowActionRoutine(bIsOpening));
        }
    }

    private IEnumerator WindowActionRoutine(bool bIsOpening)
    {
        if (bIsOpening)
        {
            cG.gameObject.SetActive(true);
        }

        while(true)
        {
            if(bIsOpening)
            {
                cG.alpha += Time.deltaTime * transitionSpeed;

                if(cG.alpha >= 1)
                {
                    break;
                }
            }
            else
            {
                cG.alpha -= Time.deltaTime * transitionSpeed;

                if(cG.alpha <= 0)
                {
                    break;
                }
            }

            yield return null;
        }

        if (!bIsOpening)
        {
            cG.gameObject.SetActive(false);
        }
    }
}