using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MakeLines : MonoBehaviour
{
    [SerializeField] List<GameObject> neus;
    [SerializeField] GameObject sprite;

    public void LineBetweenNeurons(Transform neuron1, Transform neuron2)
    {
        int size = neus.Capacity;

        //-------------------CREATE THE LINES-------------------

        Vector2 d = neuron1.transform.position - neuron2.transform.position;

        GameObject line = Instantiate(sprite, neuron1.transform);

        line.transform.position =
            neuron1.transform.position - new Vector3(d.x / 2, d.y / 2);

        line.transform.rotation =
            Quaternion.Euler(0, 0, -Mathf.Sign(d.x) * Vector3.Angle(Vector3.up, d));

        line.transform.localScale = new Vector3(0.1f, d.magnitude);
        line.name = neuron2.name;

        //-----------------CHANGE THE COLOR BASED ON WEIGHT-----------
        line.GetComponent<SpriteRenderer>().color =
        new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0.1f, 0.5f));
    }

    public void removeLines()
    {
        foreach (GameObject n in neus)
        {
            foreach (Transform line in n.transform)     //delete childs
            {
                Destroy(line.gameObject);
            }
        }
    }
}

