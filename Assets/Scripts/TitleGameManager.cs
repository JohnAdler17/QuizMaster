using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGameManager : MonoBehaviour
{
    public void OnLevelSelected(int levelIndex) {
      SceneManager.LoadScene(levelIndex);
    }
}
