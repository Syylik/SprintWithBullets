using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float slowMoTimeScale = 0.6f;
    [SerializeField] private GameObject winPanel;

    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if(instance == null) instance = this; 
    }
    public static void SlowMoOn() => Time.timeScale = instance.slowMoTimeScale;
    public static void SlowMoOff() => Time.timeScale = 1;
 
    public void Win(int multiply)
    {
        winPanel.SetActive(true);
    }
    public void Lose()
    {
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneFade.ChangeScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneFade.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }
}
