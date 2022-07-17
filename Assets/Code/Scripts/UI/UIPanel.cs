using Local_Space;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject Text;
    private Text T;
    private Animator Antor;
    private void Awake()
    {
        Antor = GetComponent<Animator>();
        T = Text.GetComponent<Text>();   
    }
    internal void Show(string KeyText)
    {
        T.text = Script.aTC.sD.Localization.Rus[KeyText];
        Antor.SetBool("Show", true);
    }
    internal void Show(string KeyText, bool NoN)
    {
        if(NoN)
        {
            T.text = KeyText;
            Antor.SetBool("Show", true);
        }       
    }
    internal void Hide()
    {
        T.text = "";
        Antor.SetBool("Show", false);
    }
}
