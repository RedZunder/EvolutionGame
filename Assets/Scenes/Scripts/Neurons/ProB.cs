using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProB : MonoBehaviour           //------------PROTO_BRAIN-------------//
{

    [SerializeField] List<Transform> NeuronPos;
    [SerializeField] Transform cellBodies;      //children are cell bodies
    [SerializeField] MakeLines linemaker;


    NeuronClass speedX, speedY, posX, posY, colR, colG, colB, N1, N2, N3;
    public List<NeuronClass> ns;        //all possible NeuronClasss

    float spX, spY;     //temporal variables for movement
    
    void Start()
    {
        Application.targetFrameRate = 60;
        //--------------------INITIALIZE NEURONS-----------------
        ///------------FIND("NAME OF OBJECT IN SCENE")--------

        speedX = new NeuronClass(cellBodies.Find("speedX"),"speedX");
        speedY = new NeuronClass(cellBodies.Find("speedY"),"speedY");
        posX = new NeuronClass(cellBodies.Find("posX"),"positionX", true);
        posY = new NeuronClass(cellBodies.Find("posY"),"positionY", true);
        colR = new NeuronClass(cellBodies.Find("colR"), "colourROut");
        colG = new NeuronClass(cellBodies.Find("colG"),"colourGOut");
        colB = new NeuronClass(cellBodies.Find("colB"),"colourBOut");
        N1 = new NeuronClass(cellBodies.Find("N1"),"change1");
        N2 = new NeuronClass(cellBodies.Find("N2"),"change2");
        N3 = new NeuronClass(cellBodies.Find("N3"),"change3");

        //------------------------------------------

        ///PUT IN THE LIST BEFORE INITIALIZATION IN SWITCH
        ns = new List<NeuronClass> { posX, posY, colB, colG, colR, speedX, speedY };

    }


    void Update()
    {

        //--------------------------BRAIN----------------------------------
        

        foreach (NeuronClass o in ns)        //establish the connections and override values
        {
            if (o.timeToUpdate != 0)        //if we update the neuron from time to time
            {
                //o -- output n       i -- input n
                float heavy = 0.0f;
                foreach (NeuronClass i in ns)
                {
                    if (i.dest.Contains(o))
                    {
                        doTheIns(i);           //update emitter neuron info
                        
                        heavy += i.x * i.w;
                    }
                }

                if (heavy != 0)             //update sum of all inputs if there was any
                {
                    o.x = heavy;            //update receiver neuron info
                    doTheOuts(o);
                }
                else if (heavy != 0)
                {

                }
            }
               
        }


        //---------------------OTHER--------------------------------------
        if (Input.GetAxis("Horizontal") != 0)
            transform.position += new Vector3(Time.deltaTime * spX 
                                        * Mathf.Sign(Input.GetAxis("Horizontal")), 0);
        if (Input.GetAxis("Vertical") != 0)
            transform.position += new Vector3(0, Time.deltaTime * spY 
                                        * Mathf.Sign(Input.GetAxis("Vertical")));
    }

    
    public void mutateConnections()
    {
        int rand = 0;
        int rand2 = 0;

        while (!(ns[rand].isInput && !ns[rand2].isInput))       //opposite of what we want
        {
            rand = Random.Range(0, ns.Count);
            rand2 = Random.Range(0, ns.Count);
        }
        

        if (ns[rand].isInput && !ns[rand2].isInput)      //connect only from input to output
        {
            ns[rand].dest.Add(ns[rand2]);                           //rand2 is out rand is in
            Debug.Log(ns[rand].name + "   " + ns[rand2].name);
            Debug.Log(ns[rand].cellBody.name + "  " + ns[rand2].cellBody.name);
            linemaker.LineBetweenNeurons(ns[rand].cellBody, ns[rand2].cellBody);
        }

        
    }


    void doTheOuts(NeuronClass i)
    {
        switch(i.name)
        {
                case "colourROut":
                GetComponent<SpriteRenderer>().color =
                    new Color(Random.Range(1f, 2) * Mathf.Clamp(Mathf.Abs(i.x / 10), 0, 128),
                    i.cellBody.GetComponent<SpriteRenderer>().color.g,
                    i.cellBody.GetComponent<SpriteRenderer>().color.b) ;
            break;
                case "colourGOut":
                GetComponent<SpriteRenderer>().color =
                    new Color(i.cellBody.GetComponent<SpriteRenderer>().color.r,
                     Random.Range(1f, 2) * Mathf.Clamp(Mathf.Abs(i.x / 10), 1, 128),
                    i.cellBody.GetComponent<SpriteRenderer>().color.b);
            break;
                case "colourBOut":
                    GetComponent<SpriteRenderer>().color =
                        new Color(i.cellBody.GetComponent<SpriteRenderer>().color.r,
                        i.cellBody.GetComponent<SpriteRenderer>().color.g,
                         Random.Range(1f, 2) * Mathf.Clamp(Mathf.Abs(i.x / 10), 0, 128));
            break;
            case "speedX":
                spX = Mathf.Abs(i.x);
                break;

            case "speedY":
                spY = Mathf.Abs(i.x);
                break;
                
        }
    }

    void doTheIns(NeuronClass i)
    {
        switch (i.name)
        {
            default: break;
            //--------------------INPUT ONLY------------------------------

            case "positionX":
            i.x = Mathf.Clamp(transform.position.x, -i.restraint, i.restraint);
                //Debug.Log("case: "+ i.x);

                break;
            case "positionY":
                i.x = Mathf.Clamp(transform.position.y, -i.restraint, i.restraint);

                break;


                
        }
  
    }




    float sumVector(List<float> x)
    {
        float result = 0;

        foreach (float i in x)
            result += i;

        return result;
    }

    


}
