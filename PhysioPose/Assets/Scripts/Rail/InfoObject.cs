using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoObject : MonoBehaviour
{
    public difficulty diff;
    public static List<float> points = new List<float>() {100f, 200f, 300f};

    public float Score;
    public TextMeshProUGUI text;

    public float AppearingTimeText;
    
    public enum difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public void LunchCo(int score)
    {
        StartCoroutine(AppearingText(score));
    }
    
    public IEnumerator AppearingText(int score)
    {
        text.enabled = true;
        text.text = $" + {score}";
        yield return new WaitForSeconds(AppearingTimeText);
        text.enabled = !text.enabled;
        yield return null;
    }
    
}
