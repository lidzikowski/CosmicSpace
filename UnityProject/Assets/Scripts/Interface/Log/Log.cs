using UnityEngine;
using System.Collections;

public delegate void CatchMessage(string message);

public class Log : MonoBehaviour
{
    public GameObject LogMessagePrefab;

    static event CatchMessage CatchMessageHandler;
    


    void Start()
    {
        CatchMessageHandler += Log_CatchMessageHandler;
    }



    private void Log_CatchMessageHandler(string message)
    {
        if (LogMessagePrefab == null)
            return;

        GameObject go = Instantiate(LogMessagePrefab, transform);
        go.GetComponent<LogMessage>().Message = message;
    }

    public static void OnCatchMessage(string message)
    {
        if (CatchMessageHandler != null)
            CatchMessageHandler(message);
    }
}