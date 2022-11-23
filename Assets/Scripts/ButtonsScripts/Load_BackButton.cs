using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_BackButton : MonoBehaviour
{
public void BackFun()
    {
        SceneManager.LoadScene(1);
    }
}
