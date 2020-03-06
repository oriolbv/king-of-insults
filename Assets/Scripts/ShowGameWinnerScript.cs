using UnityEngine;
using UnityEngine.UI;

public class ShowGameWinnerScript : MonoBehaviour
{
    public GameObject WinnerText;

    void Start()
    {
        string message = "";
        switch (GameData.GameWinner)
        {
            case 1:
                message = "You Win!";
                break;
            case 2:
                message = "You lose!";
                break;
            default:
                message = "";
                break;

        }
        
        // Write message into GameObject
        WinnerText.GetComponentInChildren<Text>().text = message;
    }
}
