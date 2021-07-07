
using UnityEditor;

[InitializeOnLoad]
public class GlobalConfig
{
    static GlobalConfig()
    {
        PlayerSettings.Android.keystorePass = "123456";
        //PlayerSettings.Android.keyaliasName = "key_alias_name";
        PlayerSettings.Android.keyaliasPass = "123456";
    }

}
