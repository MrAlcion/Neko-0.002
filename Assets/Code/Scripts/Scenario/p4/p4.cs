using Local_Space;
using UnityEngine;

public class p4 : MonoBehaviour
{
    private bool InEnd;
    private bool haveOrb = false;
    internal bool FirstWater = false;
    private bool FirstWaterPanel = false;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact) && !haveOrb)
        {
            if (TermsForEnd0()) { SetEnd("Part_5"); }
        }
        if (haveOrb && Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            TScr.pd.playableAsset = TScr.Timelines[1]; TScr.pd.Play();
            haveOrb = false;
            Script.UIP.Hide();
            Script.aTC.pD.Sc.parts["P4.catSceneJaba_2"] = true;
        }

        if (FirstWater && !FirstWaterPanel)
        {
            Script.UIP.Show("CatNoWater");
            FirstWaterPanel = true;
        }
        if (Script.tScr.touchWaterSignal && !FirstWater) FirstWater = true;



    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.name)
        {
            case "T0":

                if (!Script.aTC.pD.Sc.parts["P4.catSceneJaba_1"])
                {
                    TScr.pd.playableAsset = TScr.Timelines[0]; TScr.pd.Play();
                    Script.aTC.pD.Sc.parts["P4.catSceneJaba_1"] = true;
                }
                break;
            case "Orb":
                Destroy(c.gameObject);
                Script.UIP.Show("zhmykhOrb");
                haveOrb = true;
                break;

            case "HidePanelWater":
                if (FirstWaterPanel) { Script.UIP.Hide(); }
                break;
            case "EndPart":
                InEnd = true;
                Script.UIP.Show("NextPart");
                break;
        }
    }
    private static TScript TScr => GameObject.Find("Timeline").GetComponent<TScript>();
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.name != "Orb" || c.name != "T0" || c.name != "HidePanelWater") Script.UIP.Hide();
        if (c.name == "EndPart") InEnd = false;
    }

    private bool TermsForEnd(string name) => name == "EndPart" && Script.aTC.pD.Sc.parts["P4.catSceneJaba_1"] && Script.aTC.pD.Sc.parts["P4.catSceneJaba_2"];
    private bool TermsForEnd0() => InEnd && Script.aTC.pD.Sc.parts["P4.catSceneJaba_1"] && Script.aTC.pD.Sc.parts["P4.catSceneJaba_2"];
    private void SetEnd(string NextPart) { Script.tScr.EndPart = true; Script.tScr.NextPart = NextPart; }
}
