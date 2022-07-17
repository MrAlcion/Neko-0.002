using Local_Space;
using System;
using UnityEngine;

public class moveAnimation : MonoBehaviour
{
    // - - - Механизмы персонажа
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rigb;
    // - - - Остальная ...
    private Vector2 dM = new Vector2 { x = 0.08f, y = 0.1f };
    private float timeSecs = 0;                                                                          //отсчитанные секунды
    private const float notMoveTrigger = 7f;                                                            //пороговое значение у таймера15
    private bool temp = true;//Временная переменная для смены направления персонажа

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (Script.mFN.conscious_moving)                                                                    //Если *перемщение Neko наммеренное (кат-сцена или игрок)*
        {
            if (/*rigb.velocity.x*/ Input.GetAxis("oX") > 0 && !temp) { sr.flipX = false; temp = true;  }
            if (/*rigb.velocity.x*/Input.GetAxis("oX") < 0 && temp) {  sr.flipX = true; temp = false; }

            //playerOnRight();                                                                                //Функция смены направления спрайта в зависимости от направления движения
            if (Script.aTC.moduleOfBounces)                                                                 //Работа кода анимации в зависимости от того, активен ли модуль прыжков
            {
                // - - - Управление анимацией движения
                //Если */прыжок/ не активен* и *скорость Neko по Ох больше минимальной*, то /движение/ активно
                if ((!anim.GetBool("Jumping")) && ((Math.Abs(rigb.velocity.x)) > dM.x)) { anim.SetBool("Moving", true); }
                else anim.SetBool("Moving", false);                                                         //иначе (если */прыжок/ активен* или *скорость Neko по Ох меньше минимальной*) /движение/ неактивно
                // - - -
                // - - - Управление анимацией прыжка
                if (Script.tScr.Raycasts() || Math.Abs(rigb.velocity.y) < dM.y) anim.SetBool("Jumping", false);  //Если *Neko на поверхности* или *скорость Neko по Оу меньше минимальной*, то /прыжок/ неактивен
                else anim.SetBool("Jumping", true);                                                         //иначе (если *Neko не на поверхности* или *скорость Neko по Оу больше минимальной*) /прыжок/ активен
                // - - -
                // - - - Управление анимацией сидения (выключение)
                if (anim.GetBool("Moving") || anim.GetBool("Jumping"))                                      //Если */движение/ активно* или */прыжок/ активен*
                {
                    anim.SetBool("NMoving10s", false);                                                      // то /сидение/ неактивно
                    timeSecs = 0;                                                                           // таймер отсчёта анимации ОбНуЛиТь :D
                }
                // - - -
                //Debug.Log('1');
            }
            else
            {
                // - - - Управление анимацией движения
                if (Math.Abs(rigb.velocity.x) > dM.x) { anim.SetBool("Moving", true); }                     //Если *скорость Neko по Ох больше минимальной*, то /движение/ активно
                else { anim.SetBool("Moving", false); }                                                     //иначе (если *скорость Neko по Ох меньше минимальной*) /движение/ неактивно
                // - - -
                // - - - Управление анимацией сидения (выключение)
                if (anim.GetBool("Moving"))                                                                 //Если */движение/ активно* или */прыжок/ активен*
                {
                    anim.SetBool("NMoving10s", false);                                                      // то /сидение/ неактивно
                    timeSecs = 0;                                                                           // секундомер отсчёта анимации ОбНуЛиТь :D
                }
                // - - -
                //Debug.Log('2');
            }
        }
        else
        {
            anim.SetBool("Moving", false);
            if (Script.aTC.moduleOfBounces)                                                                     //Работа кода анимации в зависимости от того, активен ли модуль прыжков
            {
                // - - - Управление анимацией прыжка
                if (Script.tScr.Raycasts() || Math.Abs(rigb.velocity.y) < dM.y) anim.SetBool("Jumping", false);  //Если *Neko на поверхности* или *скорость Neko по Оу меньше минимальной*, то /прыжок/ неактивен
                else { anim.SetBool("Jumping", true); }                                                     //иначе (если *Neko не на поверхности* или *скорость Neko по Оу больше минимальной*) /прыжок/ активен
                // - - -
                // - - - Управление анимацией сидения (выключение)
                if (anim.GetBool("Jumping"))                                                                //Если */прыжок/ активен*
                {
                    anim.SetBool("NMoving10s", false);                                                      // то /сидение/ неактивно
                    timeSecs = 0;                                                                           // таймер отсчёта анимации ОбНуЛиТь :D
                }
                // - - -
                //Debug.Log('3');
            }
        }
        // - - - Управление анимацией сидения (включение)
        if (!anim.GetBool("Moving") && !anim.GetBool("NMoving10s")) timeSecs += Time.deltaTime;             //Если */движение/ неактивно* и */сидение/ неактивно*, то инкрементировать щют-щют секундомер
        if (timeSecs >= notMoveTrigger) anim.SetBool("NMoving10s", true);                                   //Если *накапанные секунды превышают заданный порог*, то /сидение/ активно
        // - - -       
    }
}
