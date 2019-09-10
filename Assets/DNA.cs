using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public int maxIteration = AI.maxIterations;
    public List<Vector3> forceVectors = new List<Vector3>();
    private float maxForce = 51;

    public DNA()
    {
        int i = 0;
        while(i < maxIteration)
        {
            forceVectors.Add(new Vector3(Mathf.RoundToInt(Random.Range(-1 * maxForce, maxForce)), Mathf.RoundToInt(Random.Range(-1 * maxForce, maxForce)), 0));

            //Debug.Log(Random.Range(-1 * maxForce, maxForce));
            i++;

        }
    }

    public DNA(List<Vector3> input)
    {
        int i = 0;
        while (i < maxIteration)
        {
            forceVectors.Add(input[i]);
            i++;
        }
    }

    public DNA(DNA parentA, DNA parentB)
    {
        float index = Mathf.RoundToInt(Random.Range(0, maxIteration));
        int i = 0;
        while (i < index)
        {
            forceVectors.Add(parentA.forceVectors[i]);
            i++;
        }
        while(i < maxIteration)
        {
            forceVectors.Add(parentB.forceVectors[i]);
            i++;
        }
    }

    public void mutate()
    {
        
        int indexA = Mathf.RoundToInt(Random.Range(0, maxIteration));
        int indexB = Mathf.RoundToInt(Random.Range(0, maxIteration));

        int i = (int)Mathf.Min(indexA, indexB);

        while (i < Mathf.Max(indexA, indexB))
        {
            float x = forceVectors[i].x;
            float y = forceVectors[i].y;
            forceVectors[i].Set(x + Mathf.RoundToInt(x * Random.Range(-1f, 1f)), y + Mathf.RoundToInt(y * Random.Range(-1f, 1f)), 0);
            i++;
        }



    }
}