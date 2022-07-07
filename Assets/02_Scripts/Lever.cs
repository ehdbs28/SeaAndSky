using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool isLeft = true;
    bool isRight = false;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public SpriteRenderer currentSprite;

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isLeft == true && isRight == false)
        {
            LeftLever();
        }

        if (isRight == true && isLeft == false)
        {
            //RightLever();
        }
    }

    void LeftLever()
    {
        if(isLeft == true)
        {
            currentSprite.sprite = rightSprite;
            isLeft = false;
            Debug.Log("¿ÞÂÊ ´êÀ½");
            isRight = true;
        }
    }

    void RightLever()
    {
        if(isRight == true)
        {
            currentSprite.sprite = leftSprite;
            isRight = false;
            Debug.Log("¿À¸¥ÂÊ ´êÀ½");
            isLeft = true;
        }
        
    }
}
