using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHeart : MonoBehaviour
{

    public int fakePerPop = 4;

    public int fakeHeartRate = 60;
    public int min, max;

    public float value;


    private void Start()
    {
        Gamemanager.butterflyPopped += AlterHeart;
        Randomize();
    }

    private void Randomize()
    {
        StartCoroutine(Randomness());
        IEnumerator Randomness()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Random.Range(2, 15));
                fakeHeartRate += (Random.value) >= .5f ? 1 : -1;
                ClampHeartRate();
            }
        }
    }

    private void AlterHeart(object sender, System.EventArgs args)
    {
        fakeHeartRate += fakePerPop;
        ClampHeartRate();
    }

    private void ClampHeartRate()
    {
        fakeHeartRate = Mathf.Clamp(fakeHeartRate, min, max);
        value = Mathf.InverseLerp(min, max, fakeHeartRate);
    }

}
