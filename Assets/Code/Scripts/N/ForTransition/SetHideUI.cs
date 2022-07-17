using UnityEngine;
using UnityEngine.UI;

public class SetHideUI : MonoBehaviour
{
    private Image Img;                          //Компонент картинки в панели
    internal bool Transparency;                 //Курс на затемнение либо на осветление панели
    internal float LocalTimer;                  //Локальный секундомер для затемнения панели
    private float TransitSpeed = 6f;
    private void Awake()
    {
        Img = gameObject.GetComponent<Image>();
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 1.01f, Screen.height * 1.01f);  //Изменение размера панели для нужного перекрытия
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10, -10);                              //Отодвигание панели на расстояние для нужного перекрытия

        SetHide(1f);
        LocalTimer = 1f;
        Transparency = true;
    }
    private void FixedUpdate()
    {
        if (!Transparency && LocalTimer < 1) LocalTimer += TransitSpeed * 0.1f * Time.deltaTime;    //Если *НЕ(курс на осветление)* и *Локальный секундомер < 1*, то /Локальный секундомер += скорость изменения цвета х 0,1 ч кусочек времени/
        if (Transparency && LocalTimer > 0) LocalTimer -= TransitSpeed * 0.1f * Time.deltaTime;     //Если *курс на осветление* и *Локальный секундомер > 0*, то /Локальный секундомер -= скорость изменения цвета х 0,1 ч кусочек времени/
        if (LocalTimer > 0 && LocalTimer <=1) SetHide(LocalTimer);                                  //Если *Локальный секундомер > 0*, то /Компонент картинки в панели.прозрачность = Локальный секундомер.значение/
    }
    private void SetHide(float value) => Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, value);
}
