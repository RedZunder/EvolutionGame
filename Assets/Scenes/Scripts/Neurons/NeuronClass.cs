using System.Collections.Generic;
using UnityEngine;
public class NeuronClass
{
    public float x, w;
    public string name;
    public float restraint;
    public bool isInput;            //true is output, false is input or both
    public float timeToUpdate;      //when should the neuron update its info (0 for never) [s]
    public List<NeuronClass> dest;           //destination neurons
    public Transform cellBody;      


    public NeuronClass(Transform body, string n = "", bool F = false, float tToUpdinS = 1)
    {
        timeToUpdate = tToUpdinS;
        dest = new List<NeuronClass> { };           //create empty list when initialized by default
        name = n;
        isInput = F;
        w = Random.Range(-2, 2f);           //weight to the exit connection
        restraint = 10;
        cellBody = body;
    }

    public void receiveSignal(NeuronClass n)
    {
        x = n.x * n.w;

    }

  
}
