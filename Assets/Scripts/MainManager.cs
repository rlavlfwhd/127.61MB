using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuittBtn()
    {
        Application.Quit();
    }
}
