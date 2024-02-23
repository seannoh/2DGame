using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{

    private Button startButton;
    public float charactersPerSecond = 4;
    public string title = "Make a Friend";
    private TextMeshProUGUI titleText;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;
    private float alpha = 0;
    private float fadeSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GetComponentsInChildren<Button>()[0];
        titleText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        buttonImage = startButton.GetComponent<Image>();
        buttonText = startButton.GetComponentInChildren<TextMeshProUGUI>();

        startButton.onClick.AddListener(StartGame);
        titleText.text = "";
        buttonImage.color = new Color(1, 1, 1, 0);
        buttonText.alpha = 0;
        startButton.interactable = false;

        StartCoroutine(DisplayStartWrapper());
    }


    private void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator DisplayStartWrapper() {
        yield return TypeTextUncapped(title);
        yield return FadeInStartButton();
        startButton.interactable = true;
    }


    private IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                titleText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

    private IEnumerator FadeInStartButton() {
        buttonImage.color = new Color(1, 1, 1, alpha);
        buttonText.alpha = alpha;

        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            buttonImage.color = new Color(1, 1, 1, alpha);
            buttonText.alpha = alpha;
            yield return new WaitForSeconds(0);
        }
    }
}
