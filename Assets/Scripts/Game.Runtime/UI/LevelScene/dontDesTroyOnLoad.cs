using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDesTroyOnLoad : MonoBehaviour
{
    private static dontDesTroyOnLoad _instance;// tạo lớp lúc này đang là null
    private void Awake()
    {
        if (_instance != null) return;// nếu tồn tại thì không thực hiện hàm dưới nên
        _instance = this;//lớp này khác null
        DontDestroyOnLoad(this);
    }
}
