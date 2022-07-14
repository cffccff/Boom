using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadWriteSystem : MonoBehaviour
{
    public InputField idInputField;
    public InputField nameInputField;
    public InputField infoInputField;

    public void SaveToJson()
    {
        WeaponData data = new WeaponData();// cấp phát vùng lưu
        data.Id = idInputField.text;
        data.Name = nameInputField.text;
        data.Information = infoInputField.text;

        string json = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Application.dataPath +)
    }
}
