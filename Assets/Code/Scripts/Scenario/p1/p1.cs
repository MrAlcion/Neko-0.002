using Local_Space;
using UnityEngine;

public class p1 : MonoBehaviour
{
    private bool InEnd;
    private bool Lib = false;
    private bool B = false;
    private bool Sel = false;
    private bool T0 = false;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact)) if (TermsForEnd0()) { SetEnd("Part_2"); }
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            if (!Script.aTC.pD.Sc.parts["P1.dialogLibrary"] && Lib)
            {
                Script.UIP.Hide();
                TScr.pd.playableAsset = TScr.Timelines[2]; TScr.pd.Play();
                Script.aTC.pD.Sc.parts["P1.dialogLibrary"] = true;
                Lib = false;
            }
        }
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact) && B)
        {
            if (B)
            {
                Destroy(GameObject.Find("GetBook"));
                Script.UIP.Hide();
                TScr.pd.playableAsset = TScr.Timelines[4]; TScr.pd.Play();
                Script.aTC.pD.Sc.parts["P1.inLibrary"] = true;
                B = false;
            }

        }
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            if (!Script.aTC.pD.Sc.parts["P1.dialogShop"] && Sel)
            {
                Script.UIP.Hide();
                TScr.pd.playableAsset = TScr.Timelines[1]; TScr.pd.Play();
                Script.aTC.pD.Sc.parts["P1.dialogShop"] = true;
                Sel = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.name)
        {
            case "T0":
                if (!T0)
                {
                    TScr.pd.playableAsset = TScr.Timelines[0]; TScr.pd.Play();
                    T0 = true;
                }
                break;
            case "T3":
                if (!Script.aTC.pD.Sc.parts["P1.catSceneVoron"])
                {
                    if (Script.aTC.pD.Sc.parts["P1.dialogLibrary"] && Script.aTC.pD.Sc.parts["P1.dialogShop"])
                    {
                        TScr.pd.playableAsset = TScr.Timelines[3]; TScr.pd.Play();
                        Script.aTC.pD.Sc.parts["P1.catSceneVoron"] = true;
                    }
                }
                break;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            case "TrigL":
                if (!Script.aTC.pD.Sc.parts["P1.dialogLibrary"])
                {
                    Script.UIP.Show("SpeakWith");
                    Lib = true;


                }
                break;
            case "LibraryIn":
                Script.UIP.Show("GoOut");
                break;
            case "GetBook":
                Script.UIP.Show("Забрать книгу", true);
                B = true;


                break;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            case "TrigS":
                if (!Script.aTC.pD.Sc.parts["P1.dialogShop"])
                {
                    Script.UIP.Show("SpeakWith");

                    Sel = true;


                }
                break;
            case "ShopIn":
                Script.UIP.Show("GoOut");
                break;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            case "LibraryOut":
                Script.UIP.Show("GoInLib");
                break;
            case "ShopOut":
                Script.UIP.Show("GoInShop");
                break;

            case "EndPart":
                InEnd = true;
                if (EndP()) Script.UIP.Show("NextPart");
                else Script.UIP.Show("NextPart0");

                break;
        }
    }

    private static TScript TScr => GameObject.Find("Timeline").GetComponent<TScript>();

    private void OnTriggerExit2D(Collider2D c)
    {
        Script.UIP.Hide();
        if (c.name == "EndPart") InEnd = false;
    }
    //private void OnTriggerStay2D(Collider2D c)
    //{
    //    switch (c.name)
    //    {
    //        case "TrigS":
    //            if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
    //            {
    //                if (!Script.aTC.pD.Sc.parts["P1.dialogShop"])
    //                {
    //                    Script.UIP.Hide();
    //                    TScr.pd.playableAsset = TScr.Timelines[1]; TScr.pd.Play();
    //                    Script.aTC.pD.Sc.parts["P1.dialogShop"] = true;
    //                }
    //            }
    //            break;
    //        case "TrigL":
    //            if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
    //            {
    //                if (!Script.aTC.pD.Sc.parts["P1.dialogLibrary"])
    //                {
    //                    Script.UIP.Hide();
    //                    TScr.pd.playableAsset = TScr.Timelines[2]; TScr.pd.Play();
    //                    Script.aTC.pD.Sc.parts["P1.dialogLibrary"] = true;
    //                }
    //            }
    //            break;
    //        case "GetBook":
    //            if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
    //            {
    //                Destroy(c.gameObject);
    //                Script.UIP.Hide();
    //                TScr.pd.playableAsset = TScr.Timelines[4]; TScr.pd.Play();
    //                Script.aTC.pD.Sc.parts["P1.inLibrary"] = true;
    //            }
    //            break;
    //    }
    //}
    private bool TermsForEnd(string name) => name == "EndPart" && EndP();
    private bool TermsForEnd0() => InEnd && EndP();
    private bool EndP() => Script.aTC.pD.Sc.parts["P1.dialogLibrary"] && Script.aTC.pD.Sc.parts["P1.dialogShop"] && Script.aTC.pD.Sc.parts["P1.catSceneVoron"];
    private void SetEnd(string NextPart) { Script.tScr.EndPart = true; Script.tScr.NextPart = NextPart; }
}