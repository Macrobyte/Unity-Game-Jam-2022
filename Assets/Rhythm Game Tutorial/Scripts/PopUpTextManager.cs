using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextManager : MonoBehaviour
{
    public static PopUpTextManager Instance { get { return instance; } }
    private static PopUpTextManager instance;

    [Header("PopUp")]
    public ObjectPool textPooler;
    public Vector3 offset;
    public Vector3 randomIntensity;
     
    void Awake() { instance = this; }

    public void PopUpText (Transform spawnPosition, string text, Color color)
    {
        GameObject textPopUp = textPooler.SpawnObject(transform, Quaternion.identity, true);
        textPopUp.transform.position = spawnPosition.position + offset;

        textPopUp.transform.localPosition += new Vector3(
            Random.Range(-randomIntensity.x, randomIntensity.x),
            Random.Range(-randomIntensity.x, randomIntensity.y),
            Random.Range(-randomIntensity.x, randomIntensity.z));

        textPopUp.GetComponent<TextMeshPro>().text = text;
        textPopUp.GetComponent<TextMeshPro>().color = color;
    }
}
