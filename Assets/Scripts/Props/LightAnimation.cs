using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Light))]
public class LightAnimation : MonoBehaviour
{
    private Light _light;
    void Start()
    {
        _light = GetComponent<Light>();
        _light.DOIntensity(2f, .5f)
              .SetDelay(Random.Range(0f, 2f))
              .SetLoops(-1, LoopType.Yoyo);
    }
}