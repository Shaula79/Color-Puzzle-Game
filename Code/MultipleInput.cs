using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleInput : Button
{
    public List<Button> allow; // buttons that must be active for this object to be active
    public List<Button> deny; // buttons that must be inactive



    void Update()
    {
        toggle = true;

        // looping through active buttons
        for (int i = 0; i < allow.Count; i++)
        {
            if (!allow[i].toggle)
            {
                toggle = false;
            }
        }
        // looping through inactive buttons
        for (int i = 0; i < deny.Count; i++)
        {
            if (deny[i].toggle)
            {
                toggle = false;
            }
        }
        
    }
}
