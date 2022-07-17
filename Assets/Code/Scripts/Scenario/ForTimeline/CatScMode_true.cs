using Local_Space;
using UnityEngine;

public class CatScMode_true : MonoBehaviour
{
    private void FixedUpdate() { Script.aTC.CatSceneMode = true; Script.mFN.rigb.velocity = new Vector2(0, Script.mFN.rigb.velocity.y); }
}
