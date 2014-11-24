using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
	public GameObject enabledParticlesPrefab = null;
	public GameObject activatedParticlesPrefab = null;

	private GameObject enabledParticles = null;
	private GameObject activatedParticles = null;

	// Use this for initialization
	void Start () 
	{
		enabledParticles = Instantiate(enabledParticlesPrefab, transform.position, transform.rotation) as GameObject;
		enabledParticles.transform.parent = transform;
	}
	
	private void OnMouseOver()
	{
		if (enabledParticles)
		{
			Destroy(enabledParticles);
			enabledParticles = null;
			audio.Play();
			activatedParticles = Instantiate(activatedParticlesPrefab, transform.position, transform.rotation) as GameObject;
			activatedParticles.transform.parent = transform;
			int nextChildIndex = transform.GetSiblingIndex() + 1;
			if (nextChildIndex < transform.parent.childCount)
			{
				Transform nextChild = transform.parent.GetChild(nextChildIndex);
				if (nextChild)
				{
					Star star = nextChild.GetComponent<Star>();
					if (star)
					{
						SphereLines sphereLines = GetComponentInParent<SphereLines>();
						sphereLines.Add(transform.position);
						star.enabled = true;
					}
					else
					{
						// Turn on the illustration
						nextChild.gameObject.SetActive(true);

						foreach (Transform t in nextChild.transform.root)
						{
							if (transform.IsChildOf(t))
							{
								// Turn on the next constellation
								int nextConstellationIndex = t.GetSiblingIndex() + 1;
								if (nextConstellationIndex < nextChild.transform.root.childCount)
								{
									Transform nextConstellation = nextChild.transform.root.GetChild(nextConstellationIndex);
									nextConstellation.gameObject.SetActive(true);
								}

								// Reparent the illustration up one layer
								nextChild.transform.parent = nextChild.transform.parent.parent;
								
								// Destroy the old constellations' stars
								Destroy(transform.parent.gameObject, 5f);
								break;
							}
						}
					}
				}
			}
		}
	}
}
