using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace PlayBento
{
	public class Local {
		private static Katsu<BentoProfile> profileKatsu;
		private static Katsu<BentoConfig> configKatsu;

		/// <summary>
		/// Initialize Local block. Call this method before calling any other method
		/// </summary>
		public static void Init()
		{
			profileKatsu = new Katsu<BentoProfile>();
			configKatsu = new Katsu<BentoConfig>();

			LoadProfile ();
			LoadConfig ();
		}

		/// <summary>
		/// Get the profile of the specified type
		/// </summary>
		/// <returns>The profile</returns>
		/// <param name="type">Type</param>
		public static KatsuObject GetProfile (Type type)
		{
			return profileKatsu.Get(type);
		}

		/// <summary>
		/// Get the config of the specified type
		/// </summary>
		/// <returns>The config</returns>
		/// <param name="type">Type</param>
		public static KatsuObject GetConfig (Type type)
		{
			return configKatsu.Get (type);
		}

		/// <summary>
		/// Get all profiles loaded
		/// </summary>
		public static void LoadProfile()
		{
			profileKatsu.Load ();
		}

		/// <summary>
		/// Get all configs loaded
		/// </summary>
		public static void LoadConfig ()
		{
			configKatsu.Load ();
		}

		/// <summary>
		/// Save all profiles to local storage
		/// </summary>
		public static void SaveProfile ()
		{
			profileKatsu.Save ();
		}

		/// <summary>
		/// Get total profiles
		/// </summary>
		/// <returns>Number of total profiles</returns>
		public static int ProfileCount()
		{
			return profileKatsu.ObjectCount;
		}

		/// <summary>
		/// Get total configs
		/// </summary>
		/// <returns>Number of total configs</returns>
		public static int ConfigCount()
		{
			return configKatsu.ObjectCount;
		}

		/// <summary>
		/// Update Profile Katsu
		/// </summary>
		/// <param name="katsu">Katsu</param>
		public static void UpdateProfileKatsu(Katsu<BentoProfile> katsu)
		{
			profileKatsu = katsu;
		}

		/// <summary>
		/// Get Profile Katsu
		/// </summary>
		/// <returns>Profile Katsu</returns>
		public static Katsu<BentoProfile> GetProfileKatsu()
		{
			return profileKatsu;
		}
	}
}