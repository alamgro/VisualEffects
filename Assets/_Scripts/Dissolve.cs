using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [Header("Dissolve")]
    [SerializeField] private float dissolveSpeed = 1f;
    [SerializeField] private float dissolveWait = 1f;
    [Space]
    [SerializeField] private bool useIndex;
    [SerializeField] private MeshRenderer dissolveMesh;
    [SerializeField] private int dissolveMeshIndex;
    [Header("References")]
    [SerializeField] private Material dissolveMaterial;
    

    private bool isDissolving;
    private float dissolveCurrentVal;
    private float dissolveStartVal = 1f;
    private float dissolveEndVal = 0.5f;
    private int hashDissolve = Shader.PropertyToID("_DISSOLVE");

    private void Start()
    {
        if (useIndex)
            dissolveMaterial = dissolveMesh.materials[dissolveMeshIndex];

        dissolveMaterial.SetFloat(hashDissolve, dissolveStartVal); //Set material's dissolve value

    }

    private void Update()
    {
        print(dissolveCurrentVal);
        if(Input.GetKeyDown(KeyCode.Space) && !isDissolving)
        {
            StartCoroutine(PerformDissolve());
        }
    }

    IEnumerator PerformDissolve()
    {
        isDissolving = true;
        dissolveCurrentVal = dissolveStartVal;

        //Start dissolving by time
        while(dissolveCurrentVal > dissolveEndVal)
        {
            dissolveCurrentVal -= Time.deltaTime * dissolveSpeed;
            dissolveMaterial.SetFloat(hashDissolve, dissolveCurrentVal); //Set material's dissolve value
            yield return null;
        }

        dissolveCurrentVal = dissolveEndVal;

        Debug.Log($"<<color-gren> Dissolved! </color>");
         
        yield return new WaitForSecondsRealtime(dissolveWait);

        //Start dissolving by time
        while (dissolveCurrentVal < dissolveStartVal)
        {
            dissolveCurrentVal += Time.deltaTime * dissolveSpeed;
            dissolveMaterial.SetFloat(hashDissolve, dissolveCurrentVal); //Set material's dissolve value
            yield return null;
        }


        isDissolving = false;
    }


}
