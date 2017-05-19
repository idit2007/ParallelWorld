
using UnityEngine;
using UnityEditor;

public class AssetBundle {

	[MenuItem("Assets/Build AssetBundle")]

	static void ExportResource () {

		BuildPipeline.BuildAssetBundles("bundle", BuildAssetBundleOptions.None, BuildTarget.Android); // เปลี่ยน Android เป็น Platform อื่นได้ตามใจชอบครับ

	}

}