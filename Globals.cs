using UnityEngine;
using UnityEngine.UI;

public static class Globals 
{
    public const string NUMBER_FORMAT = "+0.00;-0.00;0.00";
    public static float Episode = 0;
    public static float Success = 0;
    public static float Fail = 0;
    private static Text txtDebug = null;

    public static void ScreenText()
    {
        // ensure pointer to txtDebug
        if (txtDebug == null)
        {
            if (GameObject.Find("txtDebug") != null)
            {
                txtDebug = GameObject.Find("txtDebug").GetComponent<Text>();
            }
        }

        // display
        if (txtDebug != null)
        {
            float percent = (Success / (float)(Success + Fail)) * 100;
            txtDebug.text = string.Format("Episode={0}, Success={1}, Fail={2}, %{3}", Episode, Success, Fail, percent.ToString("0"));
        }
    }
}
