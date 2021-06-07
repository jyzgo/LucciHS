using UnityEngine;
using System.Collections;

namespace HCGDemo {
	public class DemoDestroyMe : MonoBehaviour {

    float timer;
		public float deathtimer = 10;


		// Use this for initialization
	
		// Update is called once per frame
		void Update()
		{
			timer += Time.deltaTime;

			if (timer >= deathtimer)
			{
				Destroy(gameObject);
			}

		}
	}
}
