using Local_Space;
using UnityEngine;

public class p2 : MonoBehaviour
{
    private bool InEnd;
    private bool HidedJump = false;
    public bool TbookJ = false;
    public float timer = 0;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            if (TermsForEnd0()) SetEnd("Part_3");
        }
        if (timer < 5 && TbookJ) timer += Time.deltaTime;
        if (timer >= 5&&!HidedJump)
        {
            Script.UIP.Hide();
            HidedJump = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.name)
        {
            case "T0":
                if (!Script.aTC.pD.Sc.parts["P2.catSceneVoron"])
                {
                    TScr.pd.playableAsset = TScr.Timelines[0]; TScr.pd.Play();
                    Script.aTC.pD.Sc.parts["P2.catSceneVoron"] = true;
                }
                break;
            //--
            case "BookOfJump":
                Script.aTC.moduleOfBounces = true;

                Destroy(c.gameObject);
                Script.UIP.Show("LogOfJump");
                TbookJ = true;
                break;
            //--
            case "EndPart":
                InEnd = true;
                Script.UIP.Show("NextPart");
                break;
        }
    }
    private static TScript TScr => GameObject.Find("Timeline").GetComponent<TScript>();
    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.name != "T0") Script.UIP.Hide();

        if (c.name == "EndPart") InEnd = false;
    }

    private bool TermsForEnd(string name) => name == "EndPart" && Script.aTC.pD.Sc.parts["P2.catSceneVoron"];
    private bool TermsForEnd0() => InEnd && Script.aTC.pD.Sc.parts["P2.catSceneVoron"];
    private void SetEnd(string NextPart) { Script.tScr.EndPart = true; Script.tScr.NextPart = NextPart; }
}