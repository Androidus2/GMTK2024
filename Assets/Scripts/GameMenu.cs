using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private Slider volumeSlider;

    public static bool isPaused = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            isPaused = menu.activeSelf;
            volumeSlider.value = SoundManager.GetInstance().GetGlobalVolume();
            Cursor.visible = menu.activeSelf;
            Cursor.lockState = menu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetVolume(float volume)
    {
        SoundManager.GetInstance().SetGlobalVolume(volume);
    }

}
