using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Buttons : MonoBehaviour
{
  public string gameScene;

  public void _PlayGame()
  {
    SceneManager.LoadScene(gameScene);

  }
}
