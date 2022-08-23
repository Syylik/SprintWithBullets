using UnityEngine;

public class Bot : MonoBehaviour
{
    public static int moneyDrop;
    private void Start() => moneyDrop = Random.Range(1, 10) * 100;
    public void Die()
    {
        GameManager.AddMoney(moneyDrop);
        Destroy(gameObject);
    }
}