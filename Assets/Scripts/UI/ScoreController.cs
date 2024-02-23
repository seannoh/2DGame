using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    public TextMeshProUGUI flowerScoreText, bushScoreText, smellScoreText;
    private PlayerInfo playerInfo;
    private int smellScore;
    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInstance>().playerInfo;
    }

    // Update is called once per frame
    void Update()
    {
        // this is very bad to check every frame
        // should instead use events to update score and have a listener to update text
        updateScoreText();
    }

    private void updateScoreText(){
        flowerScoreText.text = playerInfo.getNumFlowers().ToString();
        bushScoreText.text = playerInfo.getNumBushes().ToString();
        smellScore = playerInfo.getSmellScore();
        switch(smellScore) {
            case var _ when smellScore <= -3:
                smellScoreText.text = "Very Bad";
                break;
            case -2:
                smellScoreText.text = "Bad";
                break;
            case -1:
                smellScoreText.text = "Slightly Bad";
                break;
            case 0:
                smellScoreText.text = "Neutral";
                break;
            case 1:
                smellScoreText.text = "Slightly Good";
                break;
            case 2:
                smellScoreText.text = "Good";
                break;
            case var _ when smellScore >= 3:
                smellScoreText.text = "Very Good";
                break;
            default:
                smellScoreText.text = "Neutral";
                break;
        }
    }
}
