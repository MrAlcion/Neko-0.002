using Local_Space;
using UnityEngine;
public class TransitioNeko : MonoBehaviour
{
    [SerializeField] private GameObject[] Points;                           //Добавленные в редакторе точки перемещения
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    private bool InZoneTransit;                                             //Находится ли Neko в зоне для перемещения
    private Vector3 CoordTransit;                                           //Координаты конечной точки перемещения
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    private void Update() { if (Script.setH.LocalTimer >= 1) { gameObject.transform.position = CoordTransit; Script.setH.Transparency = true; } }     //Если *Локальный секундомер >= 1*, то /перемещение Neko в полученные координаты конечной точки перемещения/ и /курс на осветление/
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact) && InZoneTransit && Script.setH.LocalTimer < 0.8f) Script.setH.Transparency = false;   //Если *нажата клавиша взаимодействия* и *Neko в зоне для перемещения* и *Локальный секундомер >= 0,8*, то /курс на затемнение/
    }
    internal void OnTriggerExit2D(Collider2D c)                                                                                         //При выходе Neko из коллайдера триггера:
    {
        foreach (GameObject g in Points) if (c.gameObject == g && InZoneTransit) { InZoneTransit = false; /*ObjTrigName = null; */}     //Пробегая по массиву из точек перемещения: Если *точка перемещения из триггера = точке перемещения в списке*, то /Neko не в зоне для перемещения/ и /имя точки перемещение отсутствует/
    }
    private void OnTriggerStay2D(Collider2D c)                                                                                          //При нахождении Neko в коллайдере триггера:
    {
        if (!InZoneTransit)                                                                                                             //Если *НЕ(Neko в зоне для перемещения)*
        {
            foreach (GameObject g in Points)                                                                                            //Пробегая по массиву из точек перемещения:
            {
                if (c.gameObject == g) { CoordTransit = c.gameObject.GetComponent<ToPoints>().TransitTo.transform.position; InZoneTransit = true; /*ObjTrigName = c.gameObject.name;*/ }
            } /*Если *точка перемещения из триггера = точке перемещения в списке*, то /координаты конечной точки перемещения = полученные из скрипта (как часть точки триггера) координаты конечной точки/ и /Neko в зоне для перемещения/ и /имя точки перемещение = имя текущет точки коллайдера триггера/*/
        }
    }
}