using System;
using System.Collections.Generic;
using UnityEngine;
using Local_Space;
public class CmForNeko : MonoBehaviour
{
    public Rigidbody2D rigb;
    private float actualSpeed;                  //скорость в реальном времени
    public short bounceTimes;                   //кол-во прыжков
    public float timeOfBoost = 5;
    //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  Режим креатора
    public short CjumpTimes = 2;                //кол-во прыжков
    public float CboostInSecs = 5;
    public float CdTimeBalance = 6f;            //баланс для Time.deltatime
    public float CsetSpeed = 78;                //настраиваемая скорость
    public float CsetBounce = 50f;              //настраиваемая величина прыжка
    public float CspeedReg = 0.2f;              //скорость восстановления выносливости 
    public float CdistanceToCheck = 0.9f;

    //- - - - - - - - - - - - - - -
    private void Awake()                        //общая инициализация
    {

        rigb = GetComponent<Rigidbody2D>();
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  Режим креатора
        if (Script.aTC.CreatorMode)
        {
            if (Script.aTC.moduleOfBounces) bounceTimes = CjumpTimes; //установка начального значения счётчика прыжков (при активном модуле)
            if (Script.aTC.moduleOfBoostMovBar) timeOfBoost = CboostInSecs; //установка начального значения вемени ускорения (при активном модуле)
        }
    }
    void Update()//перед кадром
    {
        if (Script.aTC.CreatorMode)//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  Режим креатора
        {
            
            Script.tScr.distanceToCheck = CdistanceToCheck;
            if (Script.aTC.moduleOfBounces) // активен ли модуль прыжков
            { if (Script.tScr.Raycasts()) bounceTimes = CjumpTimes; } //Если кот на поверхности, то счётчик прыжков обновляется
            DoJumps();
        }
    }
    void FixedUpdate()//физ показатели
    {
        
        if (Script.aTC.CreatorMode)//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  Режим креатора
        {
            DoMoving();           
            DoSpeed();
        }
    }
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Функции
    // - - - Управление прыжками
    void DoJumps()
    {
        //Если *активен модуль прыжков*
        if (Script.aTC.moduleOfBounces && (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.jump) && bounceTimes > 0))
        { rigb.velocity += Vector2.up * CsetBounce; bounceTimes--; }
    }
    // - - - Управление движением по Ох
    void DoMoving()
    {
        //Если *неактивна кат сцена*
        if (!Script.aTC.CatSceneMode)
        {
            rigb.velocity = new Vector2(Input.GetAxis("oX") * actualSpeed * Time.deltaTime * CdTimeBalance, rigb.velocity.y);
            Script.mFN.conscious_moving = Math.Abs(Input.GetAxis("oX")) > 0.01;
        }
    }
    // - - - Управление ускорением
    void DoSpeed()
    {
        //Если *активен модуль ускорения персонажа*
        if (Script.aTC.moduleOfBoostMovBar)
        {
            if (Input.GetKey(Script.aTC.sD.Key_Kodes.boost) && timeOfBoost > 0.09) { actualSpeed = CsetSpeed * 1.5f; timeOfBoost -= Time.deltaTime; }       //при ускорении тратится выносливость
            else { actualSpeed = CsetSpeed; if (timeOfBoost < Script.aTC.pD.sk.boostInSecs) timeOfBoost += Time.deltaTime * CspeedReg; }      //при ходьбе она восстанавливается TODO!!! - доделать скорость восстановления
        }
        else { actualSpeed = CsetSpeed; } // если неактивен ли модуль ускорения персонажа

    }
}

