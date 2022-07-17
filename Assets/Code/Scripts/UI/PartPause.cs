using UnityEngine;
using UnityEngine.SceneManagement;

public class PartPause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject CanvasPause;
    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }
    public void Resume()
    {
        isPaused = false;
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
    }
    void Pause()
    {
        Time.timeScale = 0f;
        CanvasPause.SetActive(true);
        isPaused = true;
    }
    public void ToMenu() => SceneManager.LoadScene("A0-Main menu");
}