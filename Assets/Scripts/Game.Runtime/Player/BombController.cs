﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;// số lượng bom được phép đặt
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1; // phạm vi boom nổ
    public Vector2 destroyPosition;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab; //script animaton vỡ 
    public Tile destructibleTile;


    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    private void Update()
    {
        if (bombsRemaining >0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }
    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;// tạo vị trí
        position.x = Mathf.Round(position.x); // làm tròn x
        position.y = Mathf.Round(position.y); //
        //dùng hàm pooling
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;
        
        yield return new WaitForSeconds(bombFuseTime);// thời gian tồn tại

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        //nhân bản nổ start
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);//gán class vừa tạo vào class của nhân bản boom
        explosion.SetActiveRenderer(explosion.start); //
        explosion.DestroyAfter(explosionDuration); //? sao giống dưới vậy?
        Destroy(explosion.gameObject, explosionDuration);//phá huỷ nổ sau 1s

        //truyền hướng nổ để tạo đối tượng nổ mid và end
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;

        position += direction;//vị trí 4 hướng truyền vào + thêm tạo ra vị trí nhân bản nổ 4 hướng
        
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))// nếu tại vị trí có layer thì không tạo nhân bản nổ
        {
            ClearDestructible(position);
            return;
        }

        //nhân bản nổ mid, end
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); // nếu lớn hơn 1 dùng mid ngược lại dùng end, vì hiện tại phạm vi nổ là 1 nên là end 
        explosion.SetDirection(direction);// truyền hướng nổ để hiển thị animation cho đúng
        explosion.DestroyAfter(explosionDuration);// giống dưới vậy
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1); //nhân bản end song thì nhân bản các mid nếu lengh > 1
    }
    private void OnTriggerExit2D(Collider2D collision) //nếu player ra khỏi bomb
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }
    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position); //Chuyển đổi vị trí thế giới thành vị trí ô trong destrucibles
        //TileBase tile = destructibleTiles.GetTile(cell); // lấy ô trong tittle map destrucibles
        Tile tile = destructibleTiles.GetTile<Tile>(cell); //lấy ô trong destrucible tại vị trí cell trong tileBase

        if (tile != null && tile == destructibleTile) // ô đó khác null ô đó là ô phá huỷ được
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity);//nhân bản gameobject script animation 
            destructibleTiles.SetTile(cell, null); //chuyển ô trong tittle map thành null.
        }
    }
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
