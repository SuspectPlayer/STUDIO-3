using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMethods : MonoBehaviour
{
    //General Methods for actions that are outside of xr default
    public void TestLeft()
    {
        Debug.Log("Pressed Left DPad");
    }

    public void TestRight()
    {
        Debug.Log("Pressed Right DPad");
    }

    public void TestUp()
    {
        Debug.Log("Pressed Up DPad");
    }

    public void TestDown()
    {
        Debug.Log("Pressed Down DPad");
    }

    public void TestTrigger()
    {
        Debug.Log("Pressed Trigger");
    }
    public void TestGrip()
    {
        Debug.Log("Pressed Grip");
    }

    public void TestMenu()
    {
        Debug.Log("Pressed Menu");
    }

    public void TestPrimary()
    {
        Debug.Log("Pressed primary");
    }

    public void TestSecondary()
    {
        Debug.Log("Pressed Secondary");
    }
}
