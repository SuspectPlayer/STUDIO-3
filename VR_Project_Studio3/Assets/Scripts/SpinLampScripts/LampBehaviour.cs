using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehaviour : MonoBehaviour
{
    public float baseIntensity = 20;


    Color startColour = Color.white;
    bool lampOnDefault = true;
    bool lampSpinningDefault = false;

    Animator spinner;
    public Light smol;
    public Light focus;
    
    void Awake()
    {
        spinner = GetComponent<Animator>();
        SetLamp(lampOnDefault, startColour, !lampSpinningDefault);
    }


    /// <summary>
    /// Use this to set aspects of the lamp.
    /// </summary>
    /// <param name="lampOn">Sets if lamp is on or off. Does not disable light component. Defaults to true.</param>
    /// <param name="colour">Sets new colour for lamp spotlight. Defaults to white.</param>
    /// <param name="lampSpin">Sets if lamp should spin. Defaults to false.</param>
    public void SetLamp(bool lampOn, Color colour, bool lampSpin)
    {
        smol.color = colour;
        focus.color = colour;

        if(lampOn)
        {
            smol.intensity = baseIntensity;
            focus.intensity = baseIntensity * 3;
        }
        else
        {
            smol.intensity = 0f;
            focus.intensity = 0f;
        }

        spinner.SetBool("Spin", lampSpin);
    }

    /// <summary>
    /// Use this to set aspects of the lamp.
    /// </summary>
    /// <param name="lampOn">Sets if lamp is on or off. Does not disable light component. Defaults to true.</param>
    /// <param name="colour">Sets new colour for lamp spotlight. Defaults to white.</param>
    /// <param name="lampSpin">Sets if lamp should spin. Defaults to false.</param>
    public void SetLamp(bool lampOn, Color colour)
    {
        smol.color = colour;
        focus.color = colour;

        if (lampOn)
        {
            smol.intensity = baseIntensity;
            focus.intensity = baseIntensity * 3;
        }
        else
        {
            smol.intensity = 0f;
            focus.intensity = 0f;
        }

        spinner.SetBool("Spin", lampSpinningDefault);
    }

    /// <summary>
    /// Use this to set aspects of the lamp.
    /// </summary>
    /// <param name="lampOn">Sets if lamp is on or off. Does not disable light component. Defaults to true.</param>
    /// <param name="colour">Sets new colour for lamp spotlight. Defaults to white.</param>
    /// <param name="lampSpin">Sets if lamp should spin. Defaults to false.</param>
    public void SetLamp(Color colour, bool lampSpin)
    {
        smol.color = colour;
        focus.color = colour;

        if (lampOnDefault)
        {
            smol.intensity = baseIntensity;
            focus.intensity = baseIntensity * 3;
        }
        else
        {
            smol.intensity = 0f;
            focus.intensity = 0f;
        }

        spinner.SetBool("Spin", lampSpin);
    }

    /// <summary>
    /// Use this to set aspects of the lamp.
    /// </summary>
    /// <param name="lampOn">Sets if lamp is on or off. Does not disable light component. Defaults to true.</param>
    /// <param name="colour">Sets new colour for lamp spotlight. Defaults to white.</param>
    /// <param name="lampSpin">Sets if lamp should spin. Defaults to false.</param>
    public void SetLamp(bool lampOn, bool lampSpin)
    {
        smol.color = startColour;
        focus.color = startColour;

        if (lampOn)
        {
            smol.intensity = baseIntensity;
            focus.intensity = baseIntensity * 3;
        }
        else
        {
            smol.intensity = 0f;
            focus.intensity = 0f;
        }

        spinner.SetBool("Spin", lampSpin);
    }

    /// <summary>
    /// Use this to set aspects of the lamp.
    /// </summary>
    /// <param name="lampOn">Sets if lamp is on or off. Does not disable light component. Defaults to true.</param>
    /// <param name="colour">Sets new colour for lamp spotlight. Defaults to white.</param>
    /// <param name="lampSpin">Sets if lamp should spin. Defaults to false.</param>
    public void SetLamp(Color colour)
    {
        smol.color = colour;
        focus.color = colour;

        if (lampOnDefault)
        {
            smol.intensity = baseIntensity;
            focus.intensity = baseIntensity * 3;
        }
        else
        {
            smol.intensity = 0f;
            focus.intensity = 0f;
        }

        spinner.SetBool("Spin", lampSpinningDefault);
    }
}
