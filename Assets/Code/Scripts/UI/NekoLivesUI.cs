using Local_Space;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NekoLivesUI : MonoBehaviour
{
    private int lives = 3;
    [SerializeField] private Sprite[] catLiveMassive;   // НЕ ТРОГАТЬ!!! - массив спрайтов котожизней
    private SpriteRenderer spr;
    private void Awake() => spr = GetComponent<SpriteRenderer>();
    private void Start() => shiftLives(lives);

    private void Update()
    {
        if (Script.aTC.moduleOfNekoLivesUI)
        {
            if(!spr.enabled) spr.enabled = true;
            if (Script.tScr.touchWaterSignal && lives == 0)
            {
                Script.tScr.EndPartOut = true;
                Script.tScr.NextPart = SceneManager.GetActiveScene().name;
            }
            if (Script.tScr.touchWaterSignal && lives > 0) 
            {
                lives--;
                GameObject.Find("Neko").transform.position = Fx.ObjByTag("Respawn").transform.position;
                switch (lives)
                {
                    case 2: shiftLives(2); break;
                    case 1: shiftLives(1); break;
                    case 0: shiftLives(0); break;
                }
            }
        }
        else if (spr.enabled) spr.enabled = false;
    }
    private void shiftLives(int num) => spr.sprite = catLiveMassive[num];
}
