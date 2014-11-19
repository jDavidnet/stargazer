using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour 
{
	private IEnumerator Start() 
	{
		HelperText ht = FindObjectOfType<HelperText>();
		bool showHelper = true;
		float startTime = Time.realtimeSinceStartup;

		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		bool complete = false;
		while (!complete)
		{
			complete = true;
			foreach (Renderer r in renderers)
			{
				if (r.material.color.a < 0.1f)
				{
					// Only play the sound once all text is shown
					complete = false;
				}
				else
				{
					// If they've already pointed at one thing, then no need for a helper
					showHelper = false;
				}
			}

			if ((Time.realtimeSinceStartup - startTime) > 5f && showHelper)
			{
				float showTime = ht.ShowText("Go ahead...\nTouch the heavens...");
				startTime = Time.realtimeSinceStartup + showTime;
			}

			yield return null;
		}

		// Now ramp up the alpha quickly
		audio.Play();
		complete = false;
		while (!complete)
		{
			complete = true;
			foreach (Renderer r in renderers)
			{
				// Take control over the TextAlpha
				Destroy(r.GetComponent<TextAlpha>());

				Color c = r.material.color;
				c.a = Mathf.Clamp01(c.a + 0.1f);
				r.material.color = c;

				if (c.a < 1f)
				{
					complete = false;
				}
			}

			yield return null;
		}

		while (audio.isPlaying)
		{
			yield return null;
		}

		// Now ramp it down quickly
		complete = false;
		while (!complete)
		{
			complete = true;
			foreach (Renderer r in renderers)
			{
				Color c = r.material.color;
				c.a -= 0.1f;
				r.material.color = c;
				
				if (c.a > 0f)
				{
					complete = false;
				}
			}
			
			yield return null;
		}

		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(ht.ShowText("Now, lie down and look up at the stars..."));
		yield return new WaitForSeconds(2f);
		yield return new WaitForSeconds(ht.ShowText("Go ahead...Relax and lie down..."));
		
		Destroy(gameObject);
	}
}
