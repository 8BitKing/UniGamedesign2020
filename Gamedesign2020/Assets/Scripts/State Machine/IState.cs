using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void stateInit();
    void stateUpdate();
    void stateFixedUpdtate();
    void stateExit();
}
