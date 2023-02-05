using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public delegate void OnFailedActionDelegate(string failMessage);

public class MessageWindow : SubWindow
{
    [SerializeField]
    private float timeToFadeMessage = 2.0f;
    [SerializeField]
    private TextMeshProUGUI textMesh;

    private const int maxMessageSpam = 2;

    private Queue<string> messagesToShow = new Queue<string>();

    private Coroutine showMessageRoutine = null;

    private void Awake()
    {
        windowType = WindowActionType.Message;
    }

    public override void CallWindowAction()
    {
        base.CallWindowAction();

        if(showMessageRoutine == null)
        {
            showMessageRoutine = StartCoroutine(ShowMessageRoutine());
        }
    }

    public override void BackAction()
    {
        base.BackAction();

        showMessageRoutine = null;
    }

    public void QueueMessage(string newMessage)
    {
        messagesToShow.Enqueue(newMessage);
        CallWindowAction();
    }

    private IEnumerator ShowMessageRoutine()
    {
        string previousMessage = "";
        int messageSpam = 0;

        while(messagesToShow.TryDequeue(out var message))
        {
            if(previousMessage == message)
            {
                messageSpam++;
            }
            else
            {
                messageSpam = 0;
            }

            if (messageSpam < maxMessageSpam)
            {
                previousMessage = message;
                textMesh.text = message;
                yield return new WaitForSeconds(timeToFadeMessage);
            }
        }

        BackAction();
    }
}