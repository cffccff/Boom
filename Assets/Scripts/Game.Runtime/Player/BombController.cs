using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    //private AudioSource bombAudio;
    public GameObject bombPrefab;   
    //public float bombFuseTime = 3f;
    public int bombAmount = 1;// số lượng bom tối đa được phép đặt
    public static int bombsRemaining;
    
    public static Vector2 checkBombPosition;
    public Vector2 playerPosition;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public static int explosionRadius = 1; // phạm vi boom nổ đầu game //itempick điều chỉnh khi ăn buff

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab; //script animaton vỡ 
    //public Tile destructibleTile;
    public TileBase[] destructibleTile;

    public float timeExplotion;

    public SaveGold saveGold;
    private void Start()// cứ vào scene là truyền dữ liệu từ shop tới
    {
        //bombAudio = GetComponent<AudioSource>();
        saveGold = FindObjectOfType<SaveGold>();
        resetBomb();
        resetExplosion();
    }
    public void resetBomb()
    {
        bombAmount = saveGold.bombLevel + 1;
        bombsRemaining = bombAmount;        
    }
    public void resetExplosion()
    {
        explosionRadius = saveGold.explosionLevel + 1;
    }
    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
    private void Update()
    {
        
        if (bombsRemaining >0 && Input.GetKeyDown(inputKey))
        {
             
            playerPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

            if (checkBombPosition == new Vector2(0, 0)) checkBombPosition = playerPosition;
            else if (checkBombPosition != playerPosition) checkBombPosition = playerPosition;
            else if (checkBombPosition == playerPosition) return;

            // StartCoroutine(PlaceBomb());
            PlaceBomb1();
        }
    }
    private void PlaceBomb1()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x); 
        position.y = Mathf.Round(position.y);
        bombsRemaining--;
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        BombExplosion ab = bomb.GetComponent<BombExplosion>();
        StartCoroutine(ab.TestExplosion(position));
       
    }
    //private IEnumerator PlaceBomb()
    //{
    //    Vector2 position = transform.position;// tạo vị trí
    //    position.x = Mathf.Round(position.x); // làm tròn x
    //    position.y = Mathf.Round(position.y); //
    //    Debug.Log(position);

    //    //dùng hàm pooling
    //    GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);

    //    bombsRemaining--;

    //    yield return new WaitForSeconds(bombFuseTime);// thời gian tồn tại

    //    position = bomb.transform.position;
    //    position.x = Mathf.Round(position.x);
    //    position.y = Mathf.Round(position.y);
    //    //nhân bản nổ start
    //    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);//gán class vừa tạo vào class của nhân bản boom
    //    explosion.SetActiveRenderer(explosion.start); //
    //    explosion.DestroyAfter(explosionDuration); //? sao giống dưới vậy?
    //    Destroy(explosion.gameObject, explosionDuration);//phá huỷ nổ sau 1s

    //    //truyền hướng nổ để tạo đối tượng nổ mid và end
    //    Explode(position, Vector2.up, explosionRadius);
    //    Explode(position, Vector2.down, explosionRadius);
    //    Explode(position, Vector2.left, explosionRadius);
    //    Explode(position, Vector2.right, explosionRadius);

    //    bombAudio.Play();

    //    Destroy(bomb);
    //    bombsRemaining++;
    //    checkBombPosition = new Vector2(0, 0);
    //}
    //private void Explode(Vector2 position, Vector2 direction, int length)
    //{
    //    if (length <= 0) return;

    //    position += direction;//vị trí 4 hướng truyền vào + thêm tạo ra vị trí nhân bản nổ 4 hướng

    //    if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))// nếu tại vị trí có layer thì không tạo nhân bản nổ
    //    {
    //        ClearDestructible(position);
    //        return;
    //    }

    //    //nhân bản nổ mid, end
    //    Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    //    explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); // nếu lớn hơn 1 dùng mid ngược lại dùng end, vì hiện tại phạm vi nổ là 1 nên là end 
    //    explosion.SetDirection(direction);// truyền hướng nổ để hiển thị animation cho đúng
    //    explosion.DestroyAfter(explosionDuration);// giống dưới vậy
    //    Destroy(explosion.gameObject, explosionDuration);

    //    Explode(position, direction, length - 1); //nhân bản end song thì nhân bản các mid nếu lengh > 1
    //}
    private void OnTriggerExit2D(Collider2D collision) //nếu player ra khỏi bomb
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }
    //private void ClearDestructible(Vector2 position)
    //{
    //    Vector3Int cell = destructibleTiles.WorldToCell(position); //Chuyển đổi vị trí thế giới thành vị trí ô trong destrucibles
    //    //Tile tile = destructibleTiles.GetTile<Tile>(cell);// lấy ô trong tittle map destrucibles
    //    TileBase tile = destructibleTiles.GetTile<TileBase>(cell); //lấy ô trong destrucible tại vị trí cell trong tileBase //
    //    for (int i = 0; i < destructibleTile.Length; i++)
    //    {
    //        if (tile != null && tile == destructibleTile[i]) // ô đó khác null ô đó là ô phá huỷ được
    //        {
    //            Instantiate(destructiblePrefab, position, Quaternion.identity);//nhân bản gameobject script animation 
    //            destructibleTiles.SetTile(cell, null); //chuyển ô trong tittle map thành null.
    //            Debug.Log(tile);
    //        }
    //    }
    //}

}
