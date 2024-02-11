using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{

    [SerializeField]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDirection(Vector2Int inputVector) {
        switch (inputVector.ToString()){
            case "(0, 1)":
                animator.Play("player_nw_idle");
                break;
            case "(0, -1)":
                animator.Play("player_se_idle");
                break;
            case "(-1, 0)":
                animator.Play("player_sw_idle");
                break;
            case "(1, 0)":
                animator.Play("player_ne_idle");
                break;
            default:
                break;
        }
    }
}

