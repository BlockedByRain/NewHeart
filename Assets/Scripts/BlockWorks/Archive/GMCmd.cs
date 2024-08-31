using UnityEditor;
using UnityEngine;

class GMCmd
{
    [MenuItem("GMCmd/SaveArchiveConfig")]
    public static void SaveArchiveConfig()
    {
        for (int i = 0; i < 5; i++)
        {
            UserData userData = new UserData();
            userData.name = "fyj" + i.ToString();
            userData.level = i;
            ArchiveConfig.SaveUserData(userData);
        }
        Debug.Log("Save End");
    }

    [MenuItem("GMCmd/LoadArchiveConfig")]
    public static void LoadArchiveConfig()
    {
        for (int i = 0; i < 5; i++)
        {
            string name = "fyj" + i.ToString();
            UserData userData = ArchiveConfig.LoadUserData(name);
            Debug.Log(userData.name);
            Debug.Log(userData.level);
        }
        Debug.Log("Load End");
    }

}
