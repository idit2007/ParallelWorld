using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayBento
{
	public class PB : ScriptableObject{
		private static bool isInit = false;

		/// <summary>
		/// Initialize Play Bento. Call this method before calling any other Play Bento method
		/// </summary>
		public static void Init()
		{
			if(!isInit)
			{
				GameObject playBento = new GameObject();
				playBento.name = "PlayBentoObject";

			
				Local.Init();

                // Initialize Parse

                
                // Leave server config as null if we intend to use Parse.com
                

			

			
				GameObject playBentoGroup = new GameObject();
				playBentoGroup.name = "Thornflower";
				DontDestroyOnLoad(playBentoGroup);

				playBento.transform.parent = playBentoGroup.transform;
			

				isInit = true;
			}
		}
	}
}
