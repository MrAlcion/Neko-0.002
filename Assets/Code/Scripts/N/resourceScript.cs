using Local_Space;
using UnityEngine;
//скрипт сборки ресурсов
public class resourceScript : MonoBehaviour
{
    public bool resGotcha = false; //обнуление сигнала, что ресурсы подобраны
    private void OnTriggerEnter2D(Collider2D collision) //при сборе ресурса он уничтожаентся, а переменные в скрипте actToCam с классом PlayerData и переменными ресурсов инкременируются
    {
        if (Script.aTC.moduleOfResUIScript)// активен ли модуль
            switch (collision.tag)
            {
                case "smallFish": //Ежели Большие рыбы
                    Script.aTC.pD.Resources.smFish++;
                    resGotcha = true;
                    goto case "0";
                case "bigFish": //Ежели Маленькие рыбы
                    Script.aTC.pD.Resources.bFish++;
                    resGotcha = true;
                    goto case "0";
                case "plusJump": //открытие дополнительного прыжка 
                    Script.aTC.pD.sk.jumpTimes++;
                    goto case "0";
                case "0":
                    Destroy(collision.gameObject); break;
            }
    }
}
