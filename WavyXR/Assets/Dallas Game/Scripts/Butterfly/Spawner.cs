
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float range;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float lowestPoint, highestPoint;
    public float min, max;
    public float maxButterflies;
    public float currentNumber;

    private void Start()
    {
        Spawn();
        Gamemanager.butterflyPopped += delegate { currentNumber--; };
        Instantiate(prefab, player.transform.position + GetRandomPos(), Quaternion.identity);
        currentNumber++;
    }

    public void Spawn()
    {
        StartCoroutine(SpawnDelay());

        IEnumerator SpawnDelay()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(Mathf.Lerp(max, min, Heart.Instance.Interpolated));
                if (currentNumber < maxButterflies)
                {
                    Instantiate(prefab, player.transform.position + GetRandomPos(), Quaternion.identity);
                    currentNumber++;
                }
            }
        }
    }

    private Vector3 GetRandomPos()
    {
        return new Vector3(RandomRange(range), Mathf.Lerp(lowestPoint, highestPoint, Random.value), RandomRange(range));
    }

    private float RandomRange(float value)
    {
        return Mathf.Lerp(-value, value, Random.value);
    }
}
