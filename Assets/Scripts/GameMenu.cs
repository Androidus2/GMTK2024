using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private GameObject controls;

    [SerializeField]
    private GameObject tip;

    [SerializeField]
    private PlayerLook playerLook;

    [SerializeField]
    private TMP_InputField sensitivityInput;

    public static bool isPaused = false;

    private void Start()
    {
        sensitivityInput.text = playerLook.GetSensitivity().ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (tip && tip.activeSelf)
            {
                tip.SetActive(false);
            }
            else
            {

                menu.SetActive(!menu.activeSelf);
                isPaused = menu.activeSelf;
                volumeSlider.value = SoundManager.GetInstance().GetGlobalVolume();
                Cursor.visible = menu.activeSelf;
                Cursor.lockState = menu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            controls.SetActive(!controls.activeSelf);
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

    public void CloseTip(GameObject gameObject)
    {
        Debug.Log("fjdsbjkhfbsjdhbf");
        gameObject.SetActive(false);
    }

    public void ChangeSensitivity(string sensitivity)
    {
        float sens = float.Parse(sensitivity);
        playerLook.SetSensitivity(sens);
    }

}
