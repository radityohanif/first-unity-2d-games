using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int score;
    public int highScore;
}

public class GameData
{
    public string GetDataJson(string path)
    {
        string filePath = Application.dataPath + "/Datas/" + path;
        string jsonContent = System.IO.File.ReadAllText(filePath);
        return jsonContent;
    }

    public PlayerData GetPlayerData()
    {
        return JsonUtility.FromJson<PlayerData>(GetDataJson("player.json"));
    }

    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + "/Datas/player.json";
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Player data saved to: " + filePath);
    }
}
