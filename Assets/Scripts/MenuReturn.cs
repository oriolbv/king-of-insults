using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturn : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
