using Local_Space;
using UnityEngine;
using UnityEngine.UI;

public class BoostBarUI : MonoBehaviour
{
    private Slider sld;
    private void Start()
    {
        sld = GetComponent<Slider>();
        if (Script.aTC.CreatorMode) // Режим креатора
        {
            // максимальное значение бара ускорения
            sld.maxValue = GameObject.Find("Neko").GetComponent<CmForNeko>().CboostInSecs;
        }
        else
        {
            // максимальное значение бара ускорения
            sld.maxValue = Script.aTC.pD.sk.boostInSecs;
        }
    }

    private void Update()
    {
        if (Script.aTC.moduleOfBoostMovBar)
        {
            sld.value = Script.aTC.CreatorMode ? GameObject.Find("Neko").GetComponent<CmForNeko>().timeOfBoost : Script.mFN.timeOfBoost;
        }
    }
}
