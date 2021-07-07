using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIHandler : MonoBehaviour
{
    TextMeshProUGUI[] Resources;
    Transform[] Images;

    private void Awake()
    {
        Resources = new TextMeshProUGUI[(int)ResourceType._NumberOfTypes];
        Images = new Transform[(int)ResourceType._NumberOfTypes];

        for (int i = 0; i < (int)ResourceType._NumberOfTypes; i++)
        {
            //some universality for UI script
            // or if we use names instead of "resource1,2,3.. etc" -> transform.Find($"{((ResourceType)i).ToString()}/Text (TMP)")
            Resources[i] = transform.Find($"Resource{i + 1}/Text (TMP)").GetComponent<TextMeshProUGUI>();
            Images[i] = transform.Find($"Resource{i + 1}/image");
        }
    }

    public void AddResource(ResourceType type, int amount)
    {
        Images[(int)type].DOPunchScale(Vector3.one / 10f, 1f, 10);
        Resources[(int)type].text = $": {PlayerDataHandler.data.Resources[(int)type]}";
    }
}
