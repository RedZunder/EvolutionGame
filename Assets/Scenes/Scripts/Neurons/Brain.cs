using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    Vector2 targetPos, direction;
    [SerializeField] float speed;
    Color color;    //output only
    Quaternion rotation;
    public string genoma;
    public List<float> w1, w2;

    [SerializeField, Header("Number of inputs and outputs per cell")] int ipN;
    [SerializeField] int opN;
    [SerializeField, Header("Connections per Neuron")] int cpn;



    //  iA.iB.oB - 11.12.13.23.21.31 - oA.oC.oE
    //ipN=opN=3     cpn=6
    //no weights, only pass basic info of neurons connections
    //cells divide after having X requisites


    void Start()
    {
        doInitial();
        doTheThink();
        string t = "iAiBiF11132321oAoBoE";
        decodeG(ref t);

    }

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation;
        direction = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);

        doMove(ref direction);

    }





    void doTheThink()
    {

        for (int i = 0; i < cpn; i++)
        {

        }






    }




    void decodeG(ref string gen)
    {
        for (int i = 1; i < ipN * 2; i += 2)
        {
            string n = gen[i] + "" + gen[i + 1];    //one neuron

            float w = getInput(ref n);
            for (int j = ipN * 2; j < cpn * 2; j++)
            {
                //midlayer
            }
            goOut(ref n, ref w);
        }
    }


    float getInput(ref string n)
    {
        float bk = 0;
        switch (n)
        {
            default: break;

            case "iA":
                bk = transform.position.x;
                break;
            case "iB":
                bk = transform.position.y;
                break;
            case "iC":
                bk = speed;
                break;
            case "iD":

                break;


        }

        return bk;
    }

    float midLayer(ref string gen, ref string n, ref float w)
    {
        float prod = 0;

        int i = int.Parse(n[0].ToString()); //input neuron of the connection
        int o = int.Parse(n[1].ToString()); //output neuron of the connection

        string n1 = gen[2 * i - 2] + "" + gen[2 * i - 1];   //get inN 
        string n2 = gen[2 * i - 2 + ipN + cpn] + "" + gen[2 * i - 1 + ipN + cpn];   //get outN 
        float w1 = getInput(ref n1);




        return prod;
    }


    //output
    void goOut(ref string n, ref float w)
    {
        switch (n)
        {
            default: break;
            case "oA":
                transform.position = new Vector2(w, transform.position.y);
                break;
            case "oB":
                transform.position = new Vector2(transform.position.x, w);

                break;
            case "oC":
                break;
            case "oD":
                break;
            case "oE":
                break;
        }
    }

    void doMove(ref Vector2 dir)
    {

        transform.position += new Vector3(Time.deltaTime * speed * dir.normalized.x,
            Time.deltaTime * speed * dir.normalized.y);
    }

    float MatrixProduct(ref List<float> v, ref List<float> w)
    {
        float prod = 0;
        if (v.Capacity == w.Capacity)
            for (int i = 0; i < v.Capacity - 1; i++)      //vector.length or list.capacity-1
                prod += v[i] * w[i];
        return prod;
    }
    void doInitial()
    {
        rotation = transform.rotation;
        direction = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        if (speed == 0)
        {
            speed = ((int)(Random.Range(1f, 4) * 100)) / 100f;  //to get only two decimals
        }
        if (targetPos == Vector2.zero)
        {
            targetPos = 10 * Vector2.one;
        }

    }





}
