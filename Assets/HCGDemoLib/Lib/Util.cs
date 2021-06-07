using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public static class Util{

	public static bool IsPressed()
	{
		return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0);
	}

	public static bool IsPressing()
	{
		return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0);
	}
    public static void SendEmail(string toEmail, string emailSubject, string emailBody)
    {
        emailSubject = System.Uri.EscapeUriString(emailSubject);
        emailBody = System.Uri.EscapeUriString(emailBody);
        Application.OpenURL("mailto:" + toEmail + "?subject=" + emailSubject + "&body=" + emailBody );
    }

    public static void Log(object o)
    {
#if UNITY_EDITOR
        Debug.Log(o);
#endif
    }



 }