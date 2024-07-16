using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
  [SerializeField] AudioSource buttonPressed;
  [SerializeField] AudioSource correctAnswer;
  [SerializeField] AudioSource wrongAnswer;

  public void PlayButtonPressAudio() {
    buttonPressed.Play();
  }

  public void PlayCorrectAudio() {
    correctAnswer.Play();
  }

  public void PlayIncorrectAudio() {
    wrongAnswer.Play();
  }
}
