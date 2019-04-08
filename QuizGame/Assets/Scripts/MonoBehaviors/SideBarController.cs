using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarController : MonoBehaviour
{
  public event System.Action OnPausedGame = delegate { };
  public event System.Action OnGamePause = delegate { };
  public GameObject optionsCanvas;

  public AudioSource musicSource, soundsSource;

  public void ToggleMusic() => musicSource.mute = !musicSource.mute;
  public void ToggleSounds() => soundsSource.mute = !soundsSource.mute;
  public void OpenOptions() => optionsCanvas.SetActive(true);
  public void CloseOptions() => optionsCanvas.SetActive(false);

  public void GoBackToMainMenu()
  {
    throw new System.NotImplementedException();
  }
}
