using UnityEngine;
using UnityEngine.Playables;


public class TScript : MonoBehaviour
{
    [SerializeField] internal PlayableAsset[] Timelines;
    internal PlayableDirector pd;
    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }
}
