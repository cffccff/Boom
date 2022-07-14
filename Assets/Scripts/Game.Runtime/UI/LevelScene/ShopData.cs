[System.Serializable]

public class ShopData
{
    //public int totalGold, heart, bomb, explosion, speed;
    public int totalGold, speedLevel, bombLevel, explosionLevel;
    public ShopData()
    {

    }
    public ShopData(int totalGold, int speedLevel, int bombLevel, int explosionLevel)
    {
        this.speedLevel = speedLevel;
        this.totalGold = totalGold;
        this.bombLevel = bombLevel;
        this.explosionLevel = explosionLevel;
    }

}
//    public ShopData(int totalGold, int heart, int bomb, int explosion, int speed)
//    {
//        this.totalGold = totalGold;
//        this.heart = heart;
//        this.bomb = bomb;
//        this.explosion = explosion;
//        this.speed = speed;
//    }
//}