using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBeefNBeaconBurger : MonoBehaviour
{
    [Header("This is a genuine, serious script, I promise")]
    [SerializeField] MeshRenderer scanPanel;
    [SerializeField] MeshRenderer glowStick;
    [Tooltip("Material for when touchy")] [SerializeField] Material onMatScanner;
    [Tooltip("Material for when no touchy")] [SerializeField] Material offMatScanner;
    [SerializeField] Material onMatGlowstick;
    [SerializeField] Material offMatGlowstick;

    [SerializeField] Light areaLight;

    [HideInInspector] public bool isThisBeaconGlowy = false;

    private void Start()
    {
        if (onMatScanner != null && offMatScanner != null)
            scanPanel.material = offMatScanner;
        if (onMatGlowstick != null && offMatGlowstick != null)
            glowStick.materials[2] = offMatGlowstick;

        areaLight.enabled = false;
    }


    //----PLEASE PUT LOCAL METHODS HERE-----
    public void Beaconator() //Local Glowy Setter
    {
        if(isThisBeaconGlowy == true)
        {
            scanPanel.material = offMatScanner;
            glowStick.materials[2] = offMatGlowstick;

            areaLight.enabled = false;

            isThisBeaconGlowy = false;
        }
        else if (isThisBeaconGlowy == false)
        {
            scanPanel.material = onMatScanner;
            glowStick.materials[2] = onMatGlowstick;

            areaLight.enabled = true;

            isThisBeaconGlowy = true;
        }
    }


    //----IF NECESSARY, PUT YOUR SCARY PUN/RPC MALARKEY UNDER HERE-----
}
