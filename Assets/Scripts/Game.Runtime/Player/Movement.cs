using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rigidbodyPlayer { get; private set; }
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    float initialSpeed = 5f;

    // gán nút
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    // gắn các script animation di chuyển các hướng.
    public AnimatedSprite spriteRendererUp;
    public AnimatedSprite spriteRendererDown;
    public AnimatedSprite spriteRendererLeft;
    public AnimatedSprite spriteRendererRight;
    public AnimatedSprite activeSpriteRenderer;

    public SaveGold saveGold;
    private void Start()// cứ vào scene là truyền dữ liệu từ shop tới
    {
        saveGold = FindObjectOfType<SaveGold>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        resetBuff();
    }
    public void resetBuff()// dùng khi vừa vào scene và khi load map() chưa update
    {
        speed = initialSpeed + saveGold.speedLevel;
    }

    private void Awake()
    {
        activeSpriteRenderer = spriteRendererDown;
    }
    private void Update()
    {
        if      (Input.GetKey(inputUp))    SetDirection(Vector2.up, spriteRendererUp);//nếu nhấn nút w truyển vector với coppoment up vào SetDirection
        else if (Input.GetKey(inputDown))  SetDirection(Vector2.down, spriteRendererDown);
        else if (Input.GetKey(inputLeft))  SetDirection(Vector2.left, spriteRendererLeft);
        else if (Input.GetKey(inputRight)) SetDirection(Vector2.right, spriteRendererRight);
        else                               SetDirection(Vector2.zero, activeSpriteRenderer);
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbodyPlayer.position; //vị trí theo rigi
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbodyPlayer.MovePosition(position + translation);// di chuyển nhân vật theo hướng
    }
    private void SetDirection(Vector2 newDirection, AnimatedSprite spriteRenderer) //
    {
        direction = newDirection; // truyền hướng tới fixupdate

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp; //bật compoment script của Up lên nếu 
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;// true khi đứng yên.
    }
    public void death()
    {
        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
    }
}
