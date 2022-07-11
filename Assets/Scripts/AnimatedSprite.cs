using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;// hình đứng yên
    public Sprite[] animationSprites; //các hình để tạo animation

    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;// bật compoment SpriteRenderer khi nhấn nút di chuyển
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime); //sau 1/6s thực hiện và lặp lại 1/6s/1 lần// như hàm update
    }
    private void NextFrame()
    {
        animationFrame++; //tăng lên
        if (loop && animationFrame >= animationSprites.Length) //nếu tăng tới 4
        {
            animationFrame = 0; //reset về 0
        }
        if (idle)
        {
            spriteRenderer.sprite = idleSprite; //spriteRenderer.sprite hình ảnh hiển thị
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length) //lớn hơn = 0 và nhỏ hơn 4
        {
            spriteRenderer.sprite = animationSprites[animationFrame];//nếu animationFrame = 0 thì chạy sprite thứ 0
        }

    }
}
