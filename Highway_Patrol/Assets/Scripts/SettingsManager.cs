using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Text tb;

    public void LowSensi()
    {
        CarController.carSpeed = 110f;
        tb.text = "Console : " + "Sensitivity is now low";
    }

   public void MidSensi()
    {
        CarController.carSpeed = 175f;
        tb.text = "Console : " + "Sensitivity is now moderate";
    }

   public void HighSensi()
    {
        CarController.carSpeed = 260f;
        tb.text = "Console : " + "Sensitivity is now high";
    }

   public void VHighSensi()
    {
        CarController.carSpeed = 300f;
        tb.text = "Console : " + "Sensitivity is now extreme";
    }
}
