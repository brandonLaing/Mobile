using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Controls all info on the side bar
/// By: Brandon Laing
/// </summary>
public class SideBarController : MonoBehaviour
{
  #region variables
  [Tooltip("Options menu canvas")]
  public GameObject optionsCanvas;
  [Tooltip("Audio source for music or sound")]
  public AudioSource musicSource, soundsSource;

  public event Action OnGameUnpaused = delegate { };
  public event Action OnGamePause = delegate { };
  public event Action OnBackToMenu = delegate { };

  /// <summary>
  /// Tracks the status of sound effects
  /// </summary>
  private bool isPlayingMusic = true;
  #endregion

  #region Functions
  /// <summary>
  /// Pauses the music or unpauses it
  /// </summary>
  public void ToggleMusic()
  {
    if (isPlayingMusic)
      musicSource.Pause();
    else
      musicSource.UnPause();
    isPlayingMusic = !isPlayingMusic;
  }

  /// <summary>
  /// Mutes or unmutes the sounds
  /// </summary>
  public void ToggleSounds()
  {
    soundsSource.mute = !soundsSource.mute;
  }

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
    OnGamePause();
  }
  #endregion
}
