using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public event System.Action OnNewCategorySet = delegate { };
    public event System.Action OnNewGameStarted = delegate { };

    public void StartGame()
    {
        //onNewGameStarted
    }

    public void SetCategory(string category)
    {
        //onNewCategorySet
    }
}
