using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

public class ArchiveConfig
{
    //����usersData�����ڴ���
    public static Dictionary<string, UserData> usersData = new Dictionary<string, UserData>();

    // ѡȡһЩ���������ַ�
    public static char[] keyChars = { 'a', 'b', 'c', 'd', 'e' };


    // �����û�����Ϊ�ı�
    public static void SaveUserData(UserData userData)
    {
        // ��persistentDataPath���ٴ���һ��/users�ļ��У��������
        if (!File.Exists(Application.persistentDataPath + "/users"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        //���滺������
        usersData[userData.name] = userData;
        // ת���û�����ΪJSON�ַ���
        string jsonData = JsonConvert.SerializeObject(userData);
        //����
#if UNITY_EDITOR
        jsonData = Encrypt(jsonData);
#endif
        // ��JSON�ַ���д���ļ���(�ļ���ΪuserData.name)
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json", userData.name), jsonData);
    }

    // ��ȡ�û����ݵ��ڴ�
    public static UserData LoadUserData(string userName)
    {
        string path = Application.persistentDataPath + string.Format("/users/{0}.json", userName);
        // ����û������ļ��Ƿ����
        if (File.Exists(path))
        {
            // ���ı��ļ��м���JSON�ַ���
            string jsonData = File.ReadAllText(path);
            //����
            jsonData = Decrypt(jsonData);
#if UNITY_EDITOR
            jsonData = Decrypt(jsonData);
#endif
            // ��JSON�ַ���ת��Ϊ�û��ڴ�����
            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);
            //��ȡʱ�����userData�Ѿ����棬��ֱ��ʹ��
            usersData[userName] = userData;
            return userData;
        }
        else
        {
            return null;
        }
    }


    // ����
    public static string Encrypt(string data)
    {
        char[] dataChars = data.ToCharArray();
        for (int i = 0; i < dataChars.Length; i++)
        {
            char dataChar = dataChars[i];
            char keyChar = keyChars[i % keyChars.Length];
            // �ص㣺ͨ�����õ��µ��ַ�
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
        }
        return new string(dataChars);
    }


    // ����
    public static string Decrypt(string data)
    {
        // �������ִ�е���ͬ���Ĳ���
        return Encrypt(data);
    }

}
