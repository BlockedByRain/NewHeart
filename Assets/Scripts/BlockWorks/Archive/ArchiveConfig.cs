using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

public class ArchiveConfig
{
    //增加usersData放在内存中
    public static Dictionary<string, UserData> usersData = new Dictionary<string, UserData>();

    // 选取一些用于异或的字符
    public static char[] keyChars = { 'a', 'b', 'c', 'd', 'e' };


    // 保存用户数据为文本
    public static void SaveUserData(UserData userData)
    {
        // 在persistentDataPath下再创建一个/users文件夹，方便管理
        if (!File.Exists(Application.persistentDataPath + "/users"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        //保存缓存数据
        usersData[userData.name] = userData;
        // 转换用户数据为JSON字符串
        string jsonData = JsonConvert.SerializeObject(userData);
        //加密
#if UNITY_EDITOR
        jsonData = Encrypt(jsonData);
#endif
        // 将JSON字符串写入文件中(文件名为userData.name)
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json", userData.name), jsonData);
    }

    // 读取用户数据到内存
    public static UserData LoadUserData(string userName)
    {
        string path = Application.persistentDataPath + string.Format("/users/{0}.json", userName);
        // 检查用户配置文件是否存在
        if (File.Exists(path))
        {
            // 从文本文件中加载JSON字符串
            string jsonData = File.ReadAllText(path);
            //解密
            jsonData = Decrypt(jsonData);
#if UNITY_EDITOR
            jsonData = Decrypt(jsonData);
#endif
            // 将JSON字符串转换为用户内存数据
            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);
            //读取时，如果userData已经缓存，就直接使用
            usersData[userName] = userData;
            return userData;
        }
        else
        {
            return null;
        }
    }


    // 加密
    public static string Encrypt(string data)
    {
        char[] dataChars = data.ToCharArray();
        for (int i = 0; i < dataChars.Length; i++)
        {
            char dataChar = dataChars[i];
            char keyChar = keyChars[i % keyChars.Length];
            // 重点：通过异或得到新的字符
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
        }
        return new string(dataChars);
    }


    // 解密
    public static string Decrypt(string data)
    {
        // 两次异或执行的是同样的操作
        return Encrypt(data);
    }

}
