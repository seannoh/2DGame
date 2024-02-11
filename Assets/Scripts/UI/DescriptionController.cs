using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    private Image descriptionImage;
    private float alpha;
    public float fadeSpeed = 0.2f;

    public float fadeDelay = 4f;
    // Start is called before the first frame update
    void Start()
    {
        descriptionImage = GetComponent<Image>();
        alpha = 0;
        descriptionImage.color = new Color(1, 1, 1, alpha);
        descriptionText.alpha = alpha;
        
    }

    public void updateDescription(string description)
    {
        Debug.Log(descriptionText.name);
        descriptionText.SetText(description);
        alpha = 1;
        FadeOutDescriptionPanel();
    }

    public void FadeOutDescriptionPanel()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        descriptionImage.color = new Color(1, 1, 1, alpha);
        descriptionText.alpha = alpha;
        yield return new WaitForSeconds(fadeDelay);

        while(alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            descriptionImage.color = new Color(1, 1, 1, alpha);
            descriptionText.alpha = alpha;
            yield return new WaitForSeconds(0);
        }

    }
    
}
