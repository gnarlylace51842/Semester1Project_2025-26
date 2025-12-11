using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //<- VERY IMPORTANT LOCK IN!!!

public class RestartScript : MonoBehaviour
{
    public void OnButtonClick(){
        SceneManager.LoadScene("SampleScene");
    }
    public void OnExit(){
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
