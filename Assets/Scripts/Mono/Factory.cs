using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Factory : MonoBehaviour
{
    [SerializeField] Slider slider;

    [Space]
    [Header("Settings")]
    [SerializeField] float timeToProduce;
    [SerializeField] int produceAmount;
    [SerializeField] ResourceType resourceToProduce;
    [SerializeField] RequiredResource[] requiredResources;

    [Space]
    [Header("Animation")]
    [SerializeField] [Range(1f, 3f)] float minDuration;
    [SerializeField] [Range(2f, 5f)] float maxDuration;
    [SerializeField] Ease easeType;

    Transform target;
    GameObject resPrefab;
    UIHandler uihandler;
    //I used simple animation here, just in case that i can work with default animations, not only with DoTweens
    Animation anim;
    bool inprogress = false;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        uihandler = GameObject.Find("UI").GetComponent<UIHandler>();
        switch (resourceToProduce)
        {
            case ResourceType.wood: 
                { 
                    resPrefab = Resources.Load<GameObject>("Prefabs/wood"); 
                    target = GameObject.Find("Resource1").transform.Find("image"); 
                    break; 
                }
            case ResourceType.crystal: 
                { 
                    resPrefab = Resources.Load<GameObject>("Prefabs/gem");
                    target = GameObject.Find("Resource2").transform.Find("image");
                    break; 
                }
            default: break;
        }
    }

    private void Update()
    {
        if(inprogress && !anim.isPlaying)
            anim.Play("FactoryWork");
    }

    public void StartProduction()
    {
        if (requiredResources.Length > 0)
        {
            foreach (RequiredResource res in requiredResources)
            {
                if (PlayerDataHandler.data.Resources[(int)res.type] < res.value)
                {
                    //Animation of an error
                    return;
                }
            }
        }
        //Animation of payment

        if (!inprogress)
        {
            slider.gameObject.SetActive(true);
            StartCoroutine(ProduceResource());
            inprogress = true;
        }
    }

    void SpawnResource()
    {
        Vector3 targetPos = target.position;
        for (int i = 0; i < produceAmount; i++)
        {
            GameObject temp = Instantiate(resPrefab, transform.position, Quaternion.identity);

            float duration = UnityEngine.Random.Range(minDuration, maxDuration);

            temp.transform.DOMove(targetPos, duration)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    Destroy(temp);
                    PlayerDataHandler.data.Resources[(int)resourceToProduce]++;
                    uihandler.AddResource(resourceToProduce, 1);
                });
        }
    }

    IEnumerator ProduceResource()
    {
        float tick = 0.01f;
        float value = tick / timeToProduce;

        while (slider.value <= 1)
        {
            slider.value += value;
            yield return new WaitForSeconds(tick);
            if (slider.value >= 1)
                break;
        }

        inprogress = false;
        slider.gameObject.SetActive(false);
        slider.value = 0f;
        SpawnResource();
        yield return null;
    }

    [Serializable]
    public struct RequiredResource
    {
        public ResourceType type;
        public int value;
    }
}
