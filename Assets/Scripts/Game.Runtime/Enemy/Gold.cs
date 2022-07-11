using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] int gold =100;
    [SerializeField] EnemyHeart enemyHeart; //kéo vào
    [SerializeField] UiGold uiGold; 
    private void Start()
    {
        uiGold = FindObjectOfType<UiGold>();
    }


}
