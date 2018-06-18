using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Window
{
    public InputField UsernameInputField;
    public Button ButtonStart;



    private void Start()
    {
        ButtonListener(ButtonStart, ButtonStart_Click);
    }
    


    public void ButtonStart_Click()
    {
        if(UsernameInputField.text.Length <= 0)
        {
            Debug.Log("Username field is empty.");
            return;
        }

        GameObject player = Instantiate(GameData.PlayerPrefab);
        player.GetComponent<Player>().Username = UsernameInputField.text;
        Gui.CloseAllWindow();
    }
}