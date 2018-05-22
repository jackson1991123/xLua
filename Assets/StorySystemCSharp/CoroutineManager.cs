using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private class CorotinePair
    {
        public int id;
        public System.Func<IEnumerator> corotine;
    }

    private static CoroutineManager g_instance;
    public static CoroutineManager Get() { return g_instance; }

    private int increasedId = 0;
    private List<CorotinePair> allRunnings = new List<CorotinePair>();



    private void Awake()
    {
        g_instance = this;
    }

    public Coroutine StartASyncFunction(System.Func<IEnumerator> function)
    {
        return StartCoroutine(function.Invoke());
    }

    public void StopASyncFunction(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
