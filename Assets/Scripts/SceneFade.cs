using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    private static int sceneToLoad;
    private static Animator anim;
    private static readonly int FadeTrigger = Animator.StringToHash("fade");
    private void Awake() => anim = GetComponent<Animator>();
    public static void ChangeScene(int loadScene)
    {
        anim.SetTrigger(FadeTrigger);
        sceneToLoad = loadScene;
    }
    public void LoadScene() => SceneManager.LoadScene(sceneToLoad);
}
