using System.Collections;
using UnityEngine;

public class TestTransition : ITransitionEffect
{
    public IEnumerator TransitionEffect()
    {
        yield return new WaitForSeconds(1f);
        IsDone = true;
    }

    public bool IsDone { get; private set; }
}