using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Description_Controller : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDescription(string description)
    {
        Debug.Log(descriptionText.name);
        descriptionText.SetText(description);
    }
}
