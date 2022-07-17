using Local_Space;
using UnityEngine;

public class CloseLogB : MonoBehaviour
{
    private bool k = true;
    private void FixedUpdate()
    {
        if (k)
        {
            k = false;
            Script.UIP.Hide();
        }
    }
}