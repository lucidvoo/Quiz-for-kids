using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bool", menuName = "ScriptableObjects/Variables/bool", order = 10)]
public class BoolVar : ScriptableObject
{
    public bool value;

    private bool previousValue;

    public void SetTemporary(bool value)
    {
        previousValue = this.value;

        this.value = value;
    }

    public void Restore()
    {
        value = previousValue;
    }
}
