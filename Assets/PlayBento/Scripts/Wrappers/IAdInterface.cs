using UnityEngine;
using System.Collections;

namespace PlayBento
{
	public interface IAdInterface
	{
		void Init();
		void Cache();
		void Show(Ad.Format format);
		bool Ready(Ad.Format format);
		int Priority{ get;}
	}
}