using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public int[] a;
    private int index;
    private int b;

    private int max;
    private int nmax;

    // Use this for initialization
    void Start() {

        for (int i = 0; i < a.Length - 1; i++)
        {
            index = i;
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[j] < a[index])
                {
                    index = j;
                }
            }
            if (index != i)
            {
                b = a[index];
                a[index] = a[i];
                a[i] = b;
            }
        }

        foreach (var m in a)
        {
            print(m);
        }

        print("----------------------------");

        if (a[0] >= a[1])
        {
            max = a[0];
            nmax = a[1];
        }
        else
        {
            max = a[1];
            nmax = a[0];
        }

        for (int i = 2; i < a.Length; i++)
        {
            if (a[i] >= max)
            {
                nmax = max;
                max = a[i];
            }
            else if (a[i] > nmax)
            {
                nmax = a[i];
            }
        }

        print("max: " + max + "     next max: " + nmax);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
