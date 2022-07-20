using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombExplosion : MonoBehaviour
{
    //private Vector2 position;
    public GameObject bombPrefab;
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab; //script animaton vỡ 
    //public Tile destructibleTile;
    public TileBase[] destructibleTile;
    private float bombFuseTime = 1.25f;
    private float explosionDuration = 0.625f;
    private void Start()
    {
        destructibleTiles = GameObject.Find("Sky").GetComponent<Tilemap>();
    }
    private void OnDestroy()
    {
        BombController.bombsRemaining++;
        BombController.checkBombPosition = new Vector2(0, 0);
        Debug.Log("Stop coroutine");
        StopAllCoroutines();
    }
    public IEnumerator TestExplosion(Vector2 position)
    {
        position.x = Mathf.Round(position.x); // làm tròn x
        position.y = Mathf.Round(position.y); //
        Debug.Log(position);

        //dùng hàm pooling
      //  GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

     

        yield return new WaitForSeconds(bombFuseTime);// thời gian tồn tại
        try
        {
            position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            //nhân bản nổ start
            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);//gán class vừa tạo vào class của nhân bản boom
            explosion.SetActiveRenderer(explosion.start); //
            explosion.DestroyAfter(explosionDuration); //? sao giống dưới vậy?
            Destroy(explosion.gameObject, 1);//phá huỷ nổ sau 1s

            //truyền hướng nổ để tạo đối tượng nổ mid và end
            Explode(position, Vector2.up, BombController.explosionRadius);
            Explode(position, Vector2.down, BombController.explosionRadius);
            Explode(position, Vector2.left, BombController.explosionRadius);
            Explode(position, Vector2.right, BombController.explosionRadius);
            Destroy(gameObject);
           
            
        }
        catch
        {
            Debug.Log("Bomb is destroyed!!");
        }

      //  bombAudio.Play();

       
        
      
    }
    private void Explode(Vector2 position, Vector2 direction, int length)
    {
       // Debug.Log("explode");
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
        Destroy(explosion.gameObject, 1);

        Explode(position, direction, length - 1); //nhân bản end song thì nhân bản các mid nếu lengh > 1
    }
    private void ClearDestructible(Vector2 position)
    {
        
        Vector3Int cell = destructibleTiles.WorldToCell(position); //Chuyển đổi vị trí thế giới thành vị trí ô trong destrucibles
        //Tile tile = destructibleTiles.GetTile<Tile>(cell);// lấy ô trong tittle map destrucibles
        TileBase tile = destructibleTiles.GetTile<TileBase>(cell); //lấy ô trong destrucible tại vị trí cell trong tileBase //
        for (int i = 0; i < destructibleTile.Length; i++)
        {
            if (tile != null && tile == destructibleTile[i]) // ô đó khác null ô đó là ô phá huỷ được
            {
                Debug.Log(tile.name);
                Instantiate(destructiblePrefab, position, Quaternion.identity);//nhân bản gameobject script animation 
                destructibleTiles.SetTile(cell, null); //chuyển ô trong tittle map thành null.
            }
        }
    }
}
