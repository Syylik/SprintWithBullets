using UnityEngine;
using TMPro;
using Helpers.FormatNums;

public class FinishBlock : MonoBehaviour
{
    public int multiplyNum; //увеличение монет (x100)
    [SerializeField] private TMP_Text multiplyNumText;
    private void Start()
    {
        string formatedNum = FormatNumsHelper.FormatNum(((float)multiplyNum));
        multiplyNumText.text = "x" + formatedNum;
    }
}