using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SlidersManager : MonoBehaviour
{
    public Image productImage;
    
    public Slider[] dataSliders;
    public Slider[] handledSliders;

    void Awake()
    {
        foreach (Slider slider in dataSliders)
        {
            slider.value = 0;
            StartCoroutine(StartSliders(slider));
        }
    }

    void Start()
    {
        
    }

    private void Update()
    {
        foreach (Slider slider in dataSliders)
        {
            throw new NotImplementedException();
        }
    }

    IEnumerator StartSliders(Slider slider)
    {
        float elapsedTime = 0;
        float finalValue = (float)Random.value;
        while (elapsedTime < .25f)
        {
            elapsedTime += Time.fixedDeltaTime;
            slider.value = elapsedTime / .25f * finalValue;
            yield return new WaitForFixedUpdate();
        }
    }
}
