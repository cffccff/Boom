[System.Serializable]

public class ShopData
{
    //public int totalGold, heart, bomb, explosion, speed;
    public int totalGold, speedLevel;
    public ShopData()
    {

    }
    public ShopData(int totalGold, int speedLevel)
    {
        this.speedLevel = speedLevel;
        this.totalGold = totalGold;
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