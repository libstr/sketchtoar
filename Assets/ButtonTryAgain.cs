using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTryAgain : MonoBehaviour {


    public void OnClickTryAgain()
    {

        new WaitForSeconds(5);

        SceneManager.LoadScene("sc1");
        Debug.Log("Button Click");
    }
}
