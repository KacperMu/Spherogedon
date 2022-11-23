using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Auth_BackButton : MonoBehaviour
{
    public void BackScene()
    {
        SceneManager.LoadScene(0);
    }
}
