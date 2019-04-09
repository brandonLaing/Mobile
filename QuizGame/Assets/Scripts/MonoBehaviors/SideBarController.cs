using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all info on the side bar
/// By: Brandon Laing
/// </summary>
public class SideBarController : MonoBehaviour
{
  public GameObject optionsCanvas;
  public AudioSource musicSource, soundsSource;

  public event System.Action OnGameUnpaused = delegate { };
  public event System.Action OnGamePause = delegate { };
  public event System.Action OnBackToMenu = delegate { };

  private void Awake()
  {

  }

  private void OnDestroy()
  {

  }

  public void ToggleMusic() => musicSource.mute = !musicSource.mute;
  public void ToggleSounds() => soundsSource.mute = !soundsSource.mute;

  /// <summary>
  /// Opens options menu and pauses game
  /// </summary>
  public void OpenOptions()
  {
    OnGamePause();
    optionsCanvas.SetActive(true);
  }

  /// <summary>
  /// Closes options menu and unpauses game
  /// </summary>
  public void CloseOptions()
  {
    OnGameUnpaused();
    optionsCanvas.SetActive(false);
  }


  /// <summary>
  /// Puts game back to main menu
  /// </summary>
  public void GoBackToMainMenu()
  {
    OnBackToMenu();
  }
}
