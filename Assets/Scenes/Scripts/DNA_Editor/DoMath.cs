using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoMath : MonoBehaviour
{
    //relative rotation = neighbor.rot - this.rot
    List<Values> matrix;
    [SerializeField] List<Transform> items;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void updateValues(Transform i, Transform j = null)
    {
        i.GetComponent<Values>().angle = i.localEulerAngles.z;
        try
        {
            i.GetComponent<Values>().value = j.GetComponent<Values>().value;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
