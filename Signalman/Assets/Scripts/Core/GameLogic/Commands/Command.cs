using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Command
{
    protected CommandData _commandData;

    public Command(CommandData data)
    {
        _commandData = data;
    }

    public abstract UniTask Execute(Action onCompleted);
}
