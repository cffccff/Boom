using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] animationSprites; //các hình để tạo animation
    public Sprite[] animationSprites2;
    public Sprite[] animationSprites3;
    public Sprite[] animationSprites4;

    public float animationTime = 0.25f;
    private int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
       
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime); //sau 1/6s thực hiện và lặp lại 1/6s/1 lần// như hàm update
    }
    private void NextFrame()
    {
        animationFrame++; //tăng lên

        if (PlayerPrefs.GetInt("SelectedLevel") <= 3)
        {         

            if (animationFrame >= 0 && animationFrame < animationSprites.Length) //lớn hơn = 0 và nhỏ hơn 4
            {
                spriteRenderer.sprite = animationSprites[animationFrame];//nếu animationFrame = 0 thì chạy sprite thứ 0
            }
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") > 3 && PlayerPrefs.GetInt("SelectedLevel") <= 6)
        {           
            if (animationFrame >= 0 && animationFrame < animationSprites2.Length) //lớn hơn = 0 và nhỏ hơn 4
            {
                spriteRenderer.sprite = animationSprites2[animationFrame];//nếu animationFrame = 0 thì chạy sprite thứ 0
            }
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") > 6 && PlayerPrefs.GetInt("SelectedLevel") <= 9)
        {
            if (animationFrame >= 0 && animationFrame < animationSprites3.Length) //lớn hơn = 0 và nhỏ hơn 4
            {
                spriteRenderer.sprite = animationSprites3[animationFrame];//nếu animationFrame = 0 thì chạy sprite thứ 0
            }
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") > 9)
        {
            if (animationFrame >= 0 && animationFrame < animationSprites4.Length) //lớn hơn = 0 và nhỏ hơn 4
            {
                spriteRenderer.sprite = animationSprites4[animationFrame];//nếu animationFrame = 0 thì chạy sprite thứ 0
            }
        }
    }
}
