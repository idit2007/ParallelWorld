using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class PlayerProfile : BentoProfile {
		public string Name;
		public string Age;
		public List<Item> Items = new List<Item> ();
	}

	public class Item{
		public string Name;
		public int Price;
		public bool Unlocked;
	}
}