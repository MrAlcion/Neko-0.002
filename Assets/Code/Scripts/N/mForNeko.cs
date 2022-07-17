using Local_Space;
using System;
using UnityEngine;

public class mForNeko : MonoBehaviour
{
    // - - - Компоненты
    internal Rigidbody2D rigb;
    // - - - Внутренние переменные
    private float actualSpeed;                  //скорость в реальном времени
    public short bounceTimes;                   //кол-во прыжков
    public float timeOfBoost;
    // - - - Внешние переменные
    public bool conscious_moving = false;       //намеренность движения
    // - - - Константы
    private const float dTimeBalance = 400f;          //баланс для Time.deltatime
    private float setBounce = 13f;              //настраиваемая величина прыжка
    //- - - - - - - - - - - - - - -
    private float setSpeed = 1;                 //настраиваемая скорость
    private bool isRegen = false;
    private float speedReg = 0.2f;              //скорость восстановления выносливости                                                 
    //- - - - - - - - - - - - - - -

    private void Awake()                        //общая инициализация
    {
        rigb = GetComponent<Rigidbody2D>();
     
    }
    private void Start()
    {
        if (!Script.aTC.CreatorMode) // Не режим креатора
        {
            if (Script.aTC.moduleOfBounces) bounceTimes = Script.aTC.pD.sk.jumpTimes; //установка начального значения счётчика прыжков (при активном модуле)
            if (Script.aTC.moduleOfBoostMovBar) timeOfBoost = Script.aTC.pD.sk.boostInSecs; //установка начального значения вемени ускорения (при активном модуле)
        }
    }

    private void Update()//перед кадром
    {
        //isGrounded = Raycasts();
        if (!Script.aTC.CreatorMode)// Не режим креатора
        {
            if (Script.aTC.moduleOfBounces) // активен ли модуль прыжков
            { if (Script.tScr.Raycasts()) bounceTimes = Script.aTC.pD.sk.jumpTimes; } //Если кот на поверхности, то счётчик прыжков обновляется
            DoJumps();
            DoMoving();
        }
    }

    private void FixedUpdate()//физ показатели
    {
        if (!Script.aTC.CreatorMode)// Не режим креатора
        {
            
            DoSpeed();
        }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Функции
    // - - - Управление прыжками
    private void DoJumps()
    {
        //Если *активен модуль прыжков*
        if (Script.aTC.moduleOfBounces && (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.jump) && bounceTimes > 0))
        { rigb.velocity += Vector2.up * setBounce; bounceTimes--; }
    }

    // - - - Управление движением по Ох
    private void DoMoving()
    {
        //Если *неактивна кат сцена*
        if (!Script.aTC.CatSceneMode)
        {
            rigb.velocity = new Vector2(Input.GetAxis("oX") * actualSpeed * Time.deltaTime * dTimeBalance, rigb.velocity.y);
            conscious_moving = Math.Abs(Input.GetAxis("oX")) > 0.01;
        }
    }

    // - - - Управление ускорением
    private void DoSpeed()
    {
        //Если *активен модуль ускорения персонажа*
        if (Script.aTC.moduleOfBoostMovBar)
        {
            if (timeOfBoost <= 0.1) isRegen = true;
            if (timeOfBoost > Script.aTC.pD.sk.boostInSecs / 2) isRegen = false;
            if (Input.GetKey(Script.aTC.sD.Key_Kodes.boost) && !isRegen) { actualSpeed = setSpeed * 1.5f; timeOfBoost -= Time.deltaTime; }       //при ускорении тратится выносливость
            else { actualSpeed = setSpeed; if(timeOfBoost < Script.aTC.pD.sk.boostInSecs) timeOfBoost += Time.deltaTime * speedReg; }      //при ходьбе она восстанавливается           
        }
        else { actualSpeed = setSpeed; } // если неактивен ли модуль ускорения персонажа
    }
}