using Local_Space;
using UnityEngine;
using UnityEngine.UI;
// показывает, сколько и каких рыб(ресурсов) собрано
public class resUI : MonoBehaviour
{
    private Text txt;
    private void Awake() { txt = GetComponent<Text>(); }
    private void Start()
    {
        if (gameObject.name == "NumBigFish") txt.text = Script.aTC.pD.Resources.bFish.ToString();
        if (gameObject.name == "NumSmallFish") txt.text = Script.aTC.pD.Resources.smFish.ToString(); 
    }
    private void Update()
    {
        if (Script.aTC.moduleOfResUIScript)// активен ли модуль
        {
            if (!RenderFishesIf()) RenderFishes(true);//Если *активен модуль рыбок* и *неакивна прорисовка UI*, то включить прорисовку 
            if (Script.rScr.resGotcha)
            {
                if (gameObject.name == "NumBigFish") txt.text = Script.aTC.pD.Resources.bFish.ToString();
                if (gameObject.name == "NumSmallFish") txt.text = Script.aTC.pD.Resources.smFish.ToString();
                Script.rScr.resGotcha = false;
            }
        }
        else { txt.text = ""; RenderFishes(false); }
    }
    private void RenderFishes(bool b) { GameObject.Find("BigFish").GetComponent<Renderer>().enabled = b; GameObject.Find("SmallFish").GetComponent<Renderer>().enabled = b; }
    private bool RenderFishesIf() => GameObject.Find("BigFish").GetComponent<Renderer>().enabled ^ GameObject.Find("SmallFish").GetComponent<Renderer>().enabled;
}