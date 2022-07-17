using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private SpriteRenderer SR;
    private Rigidbody2D R;
    private bool temp = true;//Временная переменная для смены направления персонажа

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        R = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (R.velocity.x > 0 && !temp) { SR.flipX = false; temp = true; }
        if (R.velocity.x < 0 && temp) { SR.flipX = true; temp = false; }
    }
}