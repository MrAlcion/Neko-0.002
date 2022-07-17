using Local_Space;
using UnityEngine;
public class actToCam : MonoBehaviour
{
    //Активные модули уровня 
    public bool moduleOfNekoLivesUI;
    public bool moduleOfResUIScript;
    public bool moduleOfBoostMovBar;
    public bool moduleOfBounces;
    //- - - - - - - - - - - - - -
    public bool CatSceneMode;
    public bool CreatorMode;
    //- - - - - - - - - - - - - -
    public PlayerData pD = new PlayerData();    // данные уровня со скилами и собранными ресурсами
    public SettingsData sD = new SettingsData();
    private void Awake()
    {
        pD = Db.DeSerzJSON<PlayerData>(Db.addrPlayerJSON);
        sD = Db.DeSerzJSON<SettingsData>(Db.addrSettingsJSON);
    }
}