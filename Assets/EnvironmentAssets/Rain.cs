using System.Collections;
using System.Collections.Generic;
using EmotionEngine;
using UnityEngine;

public class Rain : EmotionAsset
{
    private ParticleSystem _rainSystem;
    public bool _outside = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _rainSystem = GetComponent<ParticleSystem>();
        var rainSystemEmission = _rainSystem.emission;
        rainSystemEmission.enabled = false;
    }

    public void EnterHouse()
    {
        _outside = false;
        var rainSystemEmission = _rainSystem.emission;
        rainSystemEmission.enabled = false;
    }

    public void ExitHouse()
    {
        _outside = true;
    }


    protected override void ChangeAsset(DiscreteEmotion emotion)
    {
        if (!_outside) return;
        if (emotion.GetEmotion(EmotionType.Sadness).Intensity > 0.5f)
        {
            var rainSystemEmission = _rainSystem.emission;
            rainSystemEmission.enabled = true;
        } else if (emotion.GetEmotion(EmotionType.Sadness).Intensity > 0.5f)
        {
            var rainSystemEmission = _rainSystem.emission;
            rainSystemEmission.enabled = true;
        }
        else
        {
            var rainSystemEmission = _rainSystem.emission;
            rainSystemEmission.enabled = false;
        }
    }
}
