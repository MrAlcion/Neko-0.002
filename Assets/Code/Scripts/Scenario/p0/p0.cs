using Local_Space;
using UnityEngine;

public class p0 : MonoBehaviour
{
    private bool InEnd;
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            if (TermsForEnd0()) { SetEnd("Part_1"); }
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.gameObject.name)
        {
            case "T0":

                if (!Script.aTC.pD.Sc.parts["P0.dialog"])
                {
                    TScr.pd.playableAsset = TScr.Timelines[0]; TScr.pd.Play();
                    Script.aTC.pD.Sc.parts["P0.dialog"] = true;
                }
                break;
            case "EndPart":
                Script.UIP.Show("NextPart");
                InEnd = true;
                break;
        }
    }
    private static TScript TScr => GameObject.Find("Timeline").GetComponent<TScript>();
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.name != "T0") Script.UIP.Hide();
        if (c.name == "EndPart") InEnd = false;
    }

    private bool TermsForEnd(string name) => name == "EndPart" /*&& Script.aTC.pD.Sc.parts["P0.dialog"]*/;
    private bool TermsForEnd0() => InEnd /*&& Script.aTC.pD.Sc.parts["P0.dialog"]*/;
    private void SetEnd(string NextPart) { Script.tScr.EndPart = true; Script.tScr.NextPart = NextPart; }
}
