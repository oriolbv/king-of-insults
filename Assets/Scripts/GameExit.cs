using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame() 
    {
        Debug.Log("Exit pressed!");
        Application.Quit();
    }
}
