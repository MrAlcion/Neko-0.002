using Local_Space;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private float timer = 0;
    private bool flag;
    private PlayerData pD;    // данные уровня со скилами и собранными ресурсами
    private SettingsData sD;
    private void Awake()
    {
        PlayerData pD = new PlayerData();    // данные уровня со скилами и собранными ресурсами
        SettingsData sD = new SettingsData();
       
        //if (!pD.Sc.parts["Before_P0.catScene_0"]) { GameObject.Find("BContinue").GetComponent<Button>().interactable = false; }

    }
    private void Start()
    {
        Db.existPD();           //
        Db.existSD();           //
        pD = Db.DeSerzJSON<PlayerData>(Db.addrPlayerJSON);
        sD = Db.DeSerzJSON<SettingsData>(Db.addrSettingsJSON);
        //Debug.Log(Db.addrPlayerJSON);
        //Debug.Log(pD.sk.jumpTimes);
    }
    private void FixedUpdate()
    {
        if (timer < 3 && flag) timer += Time.deltaTime;
        if (timer >= 3) { flag = false; timer = 0; Script.UIP.Hide(); }
    }
    public void StartGame()
    {
        Script.aTC.pD.Sc.SetF();
        Db.SavePlayerData(Script.aTC.pD);
        SceneManager.LoadScene("Before_Part_0");
    }
    public void Continue()
    {
        Script.UIP.Show("На данный момент эта кнопка недоступна", true); flag = true;
    }
    public void Settings() { /*SceneManager.LoadScene("A2-Settings");*/Script.UIP.Show("На данный момент эта кнопка недоступна", true); flag = true; }
    public void Exit() => Application.Quit();
}
