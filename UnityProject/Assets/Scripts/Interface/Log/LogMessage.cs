using UnityEngine;
using UnityEngine.UI;

public class LogMessage : MonoBehaviour
{
    public string Message
    {
        set
        {
            GetComponent<Text>().text = value;
        }
    }

    float Timer = 0;



    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 5)
            Destroy(gameObject);
    }
}