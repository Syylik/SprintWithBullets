using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Helpers.FormatNums;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLAY, WIN }
    public GameState state;

    [SerializeField] private TMP_Text moneyAddText;
    [SerializeField] private TMP_Text moneyMultiplyText;

    public static int money;
    [SerializeField] private TMP_Text moneyText;

    public static int moneyDropped;

    [SerializeField] private float slowMoTimeScale = 0.6f;
    [SerializeField] private GameObject winPanel;
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if(instance == null) instance = this; 
        state = GameState.PLAY;
    }
    public static void SlowMoOn()
    {
        Gun.ChangeShootState(false);
        Time.timeScale = instance.slowMoTimeScale;
    }
    public static void SlowMoOff()
    {
        Gun.ChangeShootState(true);
        Time.timeScale = 1;
    }
    public static void AddMoney(int value) => moneyDropped += value;
    public void Win(int multiply)
    {
        state = GameState.WIN;
        winPanel.SetActive(true);
        money = moneyDropped * multiply;
        moneyAddText.text = FormatNumsHelper.FormatNum((float)moneyDropped);
        moneyMultiplyText.text = "x" + FormatNumsHelper.FormatNum((float)multiply);
        Mathf.MoveTowards(moneyDropped, money, 10);
        StartCoroutine(WinnedMoney());
    }
    private IEnumerator WinnedMoney()
    {
        yield return new WaitForSeconds(1);
        moneyText.text = FormatNumsHelper.FormatNum((float)money);
    }
    public void Lose()
    {
        Restart();
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
