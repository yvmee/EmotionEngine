using System.Collections;
using System.Collections.Generic;
using System.Text;
using EmotionEngine;
using TMPro;
using UnityEngine;

public class TextUpdateEmotionState : MonoBehaviour
{
    private TextMeshProUGUI _text;
    
    // Start is called before the first frame update
    void Start()
    {
        EmotionModel.EmotionStateChanged.AddListener(UpdateText);

        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateText(IEmotion arg0)
    {
        _text.text = (DisplayEmotion((DiscreteEmotion)arg0));
    }

    private string DisplayEmotion(DiscreteEmotion e)
    {
        var sb = new StringBuilder();
        foreach (var emotion in e.GetEmotions())
        {
            sb.Append(emotion.Type);
            sb.Append(": ");
            sb.Append(emotion.Intensity);
            sb.Append('\n');
        }
        return sb.ToString();
    }

}
