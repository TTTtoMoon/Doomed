using System;
using UnityEngine;

/// <summary>
/// 等待某个条件成立的Instruction
/// </summary>
public class BoolYieldInstruction : CustomYieldInstruction
{
    private Func<bool> Function;

    public BoolYieldInstruction(Func<bool> function)
    {
        Function = function;
    }

    public override bool keepWaiting
    {
        get
        {
            //取反，等待某个条件不成立不合常理
            return !Function.Invoke();
        }
    }
}
