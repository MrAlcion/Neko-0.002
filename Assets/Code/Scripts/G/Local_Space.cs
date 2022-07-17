using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Local_Space
{
    internal class Script
    {
        public static mForNeko mFN => GameObject.Find("Neko").GetComponent<mForNeko>();
        public static actToCam aTC => GameObject.Find("Main Camera").GetComponent<actToCam>();
        public static moveAnimation mAni => GameObject.Find("Neko").GetComponent<moveAnimation>();
        public static resourceScript rScr => GameObject.Find("Neko").GetComponent<resourceScript>();
        public static triggerScript tScr => GameObject.Find("Neko").GetComponent<triggerScript>();
        public static UIPanel UIP => GameObject.Find("TextUI").GetComponent<UIPanel>();
        public static SetHideUI setH => GameObject.Find("HidePanel").GetComponent<SetHideUI>();
    }

    internal class Fx
    {
        public static GameObject ObjByTag(string tag) => GameObject.FindGameObjectWithTag(tag);
    }

    internal class Db
    {
        public static string addrPlayerJSON => Application.persistentDataPath + "/PlayerData.json";
        public static string addrSettingsJSON => Application.persistentDataPath + "/SettingsData.json";

        // - - - Запись (сериализация) в БД JSON
        public static void SerzJSON(object item, string @addr) => File.WriteAllText(@addr, JsonConvert.SerializeObject(item, Formatting.Indented));
        // - - - Чтение (десериализация) из БД JSON
        public static T DeSerzJSON<T>(string @addr) => JsonConvert.DeserializeObject<T>(File.ReadAllText(@addr));

        public static void existPD() { if (!File.Exists(addrPlayerJSON)) { PlayerData d = new PlayerData(); d.setFirst(); SerzJSON(d, addrPlayerJSON); } }
        public static void existSD() { if (!File.Exists(addrSettingsJSON)) { SettingsData d = new SettingsData(); d.setFirst(); SerzJSON(d, addrSettingsJSON); } }
        public static void ReSetBD() { File.Delete(addrPlayerJSON); File.Delete(addrSettingsJSON); PlayerData d = new PlayerData(); d.setFirst(); SerzJSON(d, addrPlayerJSON); SettingsData sd = new SettingsData(); sd.setFirst(); SerzJSON(sd, addrSettingsJSON); }
        public static void SavePlayerData(PlayerData pd) { SerzJSON(pd, addrPlayerJSON); }
        public static void SaveSettingsData(SettingsData sd) { SerzJSON(sd, addrSettingsJSON); }
    }


    public class PlayerData
    {
        public DResourses Resources;
        public DSkills sk;
        public OpenSkills osk;
        public Scenario Sc = new Scenario();
        public struct DResourses        //количество ресурсов
        {
            public int smFish;
            public int bFish;
        }
        public struct DSkills           //открытые навыки
        {
            public short jumpTimes;
            public float boostInSecs;
        }

        public struct OpenSkills
        {
            public bool swimWater;
        }

        public void setFirst()          //установка базовых значений в хранилище
        {
            Resources.bFish = Resources.smFish = 0;
            sk.boostInSecs = 5f;
            sk.jumpTimes = 1;
            osk.swimWater = false;
            Sc.SetF();
        }
        public void SetCount0() => Resources.smFish = Resources.bFish = 0;  //для подсчёта ресурсов в начале уровня
    }

    public class SettingsData
    {
        public KeyKodes Key_Kodes;
        public Localz Localization = new Localz();
        public struct KeyKodes
        {
            public KeyCode boost, jump, interact;
        }
        public void setFirst()
        {
            Key_Kodes.boost = KeyCode.LeftShift;
            Key_Kodes.jump = KeyCode.Space;
            Key_Kodes.interact = KeyCode.E;
            Localization.SetFR();
        }
    }
    public class Scenario
    {
        public Dictionary<string, bool> parts;
        public void SetF()
        {
            parts = new Dictionary<string, bool>
            {
                { "Before_P0.catScene_0", false },
                //Башня ведьмы
                { "P0.dialog", false },
                //Город
                { "P1.catSceneVoron", false },
                { "P1.dialogLibrary", false },
                { "P1.inLibrary", false },
                { "P1.dialogShop", false },
                //Башня ведьмы 2
                { "P2.catSceneVoron", false },
                //Долина с бугорками
                { "P3.catSceneVoron", false },
                //Озеро, звезда и жаба
                { "P4.catSceneJaba_1", false },
                { "P4.catSceneJaba_2", false },
                //У подножия горы
                { "P5.catSceneVoron", false }
            };
        }
    }
    public class Localz
    {
        public Dictionary<string, string> Rus;
        public Dictionary<string, string> Eng;
        public void SetFR()
        {
            Rus = new Dictionary<string, string>
            {
                { "GoInShop", "Войти в Лавку\n[E]" },
                { "GoInLib", "Войти в Библиотеку\n[E]" },
                { "SpeakWith", "Поговорить\n[E]" },
                { "GoOut", "Выйти\n[E]" },
                { "NextPart", "Пойти далее\n[E]" },
                { "NextPart0", "Может сначала зайти\n в лавку и Библиотеку?" },
                { "LogOfBoost", "Чтобы ускориться\nзажмите Lshift" },
                { "LogOfJump", "Теперь вы можете прыгать\nПробел для прыжка" },
                { "zhmykhOrb", "Раздавить шар\n[E]" },
                { "CatNoWater", "Если вы не знали,\nто этот чел сказал,\nчто наш кот не любит воду\nСтранный..." },
                { "InCode", "На данный момент эта кнопка недоступна" }
            };
        }
        //public void SetFE()
        //{
        //    Eng = new Dictionary<string, string>
        //    {
        //        { "GoInShop", "Enter Shop" },
        //        { "GoInLib", "Go to Library" },
        //        { "SpeakWith", "Talk" },
        //        { "GoOut", "Exit" },
        //        { "NextPart", "Go next" },
        //        { "NextPart0", "Maybe you\n go to the shop and the Library first?" },
        //        { "LogOfBoost", "Press Lshift to speed up" },
        //        { "LogOfJump", "Now you can jump\nSpace to jump" },
        //        { "zhmykhOrb", "Crush the ball\n[E]" },
        //        { "CatNoWater", "In case you didn't know, \nthe «scientists» proved that\ncats don't like water" },
        //        { "InCode", "This button is currently unavailable" }
        //    };
        //}
    }
}