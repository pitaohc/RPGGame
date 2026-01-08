using UnityEngine;

public interface ICounterable
{
    bool CanBeCountered { get; }
    public void HandleCounter();
}
