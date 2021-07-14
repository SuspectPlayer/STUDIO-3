using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering_Lights : MonoBehaviour
{

	Light testLight;
	public float minWaitTime;
	public float maxWaitTime;

	void Start()
	{
		testLight = GetComponent<Light>();
		//StartCoroutine(Flashing());
	}

    void Update()
    {
        if(GetComponent<LightControl>().assignedButton.image.sprite == GetComponent<LightControl>().lightOn)
        {
			StartCoroutine(Flashing());
        }
        else
        {
			StopAllCoroutines();
        }
    }

    IEnumerator Flashing()
	{
		while (true)
		{
			Debug.Log("Flash");
			yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
			testLight.enabled = !testLight.enabled;
		}
	}
}
