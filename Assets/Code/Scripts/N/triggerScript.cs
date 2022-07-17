using Local_Space;
using UnityEngine;
using UnityEngine.SceneManagement;
public class triggerScript : MonoBehaviour
{

    public bool touchWaterSignal;
    public float distanceToCheck = 0.01f;       //длина обнаруживающих лучей под персонажем (1 метод)
    private LayerMask whatsGround;              //слой, где находится Ground
    internal bool EndPart = false;
    internal bool EndPartOut;
    private bool EndPartOutFlag;
    internal string NextPart;

    private void Awake()
    {
        whatsGround = LayerMask.GetMask("Ground");
        transform.position = Fx.ObjByTag("Respawn").transform.position; //Начальное положение кота в начале уровня
    }
    private void Update()
    {
        if (Input.GetKeyDown(Script.aTC.sD.Key_Kodes.interact))
        {
            if (EndPart) Script.setH.Transparency = false;
        }
        if (Script.setH.LocalTimer >= 1 && EndPart) { Db.SavePlayerData(Script.aTC.pD); SceneManager.LoadScene(NextPart); }
        if (Script.setH.LocalTimer >= 1 && EndPartOut) SceneManager.LoadScene(NextPart);
    }

    private void FixedUpdate()
    {
        if (EndPartOut && !EndPartOutFlag) { Script.setH.Transparency = false; EndPartOutFlag = true; }       
    }

    //механика столкновения с водой
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (Script.aTC.moduleOfNekoLivesUI)// активен ли модуль
            if (c.tag == "Water") touchWaterSignal = true;
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        if (Script.aTC.moduleOfNekoLivesUI)// активен ли модуль
            if (c.tag == "Water") touchWaterSignal = false;
    }

    // - - - Обнаружение слоя "Земли"
    public bool Raycasts()
    {
        //                                   начало луча                                          направление / длина обнаружения / слой
        return Physics2D.Raycast(new Vector2(transform.position.x - 0.05f, transform.position.y), Vector2.down, distanceToCheck, whatsGround)
            || Physics2D.Raycast(new Vector2(transform.position.x + 0.05f, transform.position.y), Vector2.down, distanceToCheck, whatsGround)
            || Physics2D.Raycast(transform.position, Vector2.down, distanceToCheck, whatsGround);
    }
}
