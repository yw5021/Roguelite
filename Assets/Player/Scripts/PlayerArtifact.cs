using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtifact
{
    private int index;
    private int stack = 0;

    public int Index
    {
        get
        {
            return index;
        }
    }
    public int Stack
    {
        get
        {
            return stack;
        }
    }

    public void AddStack()
    {
        stack++;
    }

    public PlayerArtifact(int index)
    {
        this.index = index;
    }
}
