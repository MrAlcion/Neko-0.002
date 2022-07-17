using Local_Space;
using UnityEngine;

public class OpenLogB : MonoBehaviour
{
    private bool k = true;
    private void FixedUpdate()
    {
        if (k)
        {
            k = false;
            Script.UIP.Show("LogOfBoost");
        }
    }
}
