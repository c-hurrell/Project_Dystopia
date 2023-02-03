using UnityEngine;

public class WorldObject : MonoBehaviour
{
    private Vector3 _originalScale;

    /// <summary>
    /// Ran when entering a battle scene
    /// </summary>
    public virtual void BattleStart()
    {
    }
    
    public virtual void FinishedStartTransition()
    {
        var t = transform;

        _originalScale = t.localScale;
        t.localScale = Vector3.zero;
    }

    /// <summary>
    /// Ran when exiting a battle scene
    /// </summary>
    public virtual void BattleEnd()
    {
        transform.localScale = _originalScale;
    }
    
    public virtual void FinishedEndTransition()
    {
    }
}