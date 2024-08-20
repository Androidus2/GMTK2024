using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite[] images;

    [SerializeField]
    private string[] texts;

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private float timeBetweenImages = 2f;

    [SerializeField]
    private TextMeshProUGUI clickToAdvanceText;

    private int currentImage = 0;
    private int currentText = 0;

    private bool isLoading = true;

    private void Start()
    {
        StartCoroutine(LoadText());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isLoading)
        {
            clickToAdvanceText.gameObject.SetActive(false);
            if(currentText < texts.Length)
            {
                StartCoroutine(LoadImage());
            }
            else
            {
                SceneManager.LoadScene("StartingScene");
            }

        }
        if(Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("SecondScene");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("ThirdScene");
        }
    }

    IEnumerator LoadText()
    {
        image.sprite = null;
        isLoading = true;
        text.text = "";
        source.clip = audioClips[currentText];
        source.Play();
        float timeBetweenCharacters = audioClips[currentText].length / texts[currentText].Length;
        for(int i = 0; i < texts[currentText].Length; i++)
        {
            text.text += texts[currentText][i];
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        currentText++;
        //isText = false;
        isLoading = false;
        clickToAdvanceText.gameObject.SetActive(true);
        if(currentText >= texts.Length)
        {
            clickToAdvanceText.text = "Click to start";
        }
    }

    IEnumerator LoadImage()
    {
        text.text = "";
        //isLoading = true;
        image.sprite = images[currentImage];
        float timeToWait = timeBetweenImages / 255;
        for(int i = 0; i < 255; i++)
        {
            image.color = new Color(1, 1, 1, i / 255f);
            yield return new WaitForSeconds(timeToWait);
        }
        yield return new WaitForSeconds(timeBetweenImages);
        for(int i = 255; i > 0; i--)
        {
            image.color = new Color(1, 1, 1, i / 255f);
            yield return new WaitForSeconds(timeToWait);
        }
        currentImage++;
        //isText = true;
        //isLoading = false;
        //clickToAdvanceText.gameObject.SetActive(true);
        StartCoroutine(LoadText());
    }

}
