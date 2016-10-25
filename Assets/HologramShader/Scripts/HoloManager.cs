using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HoloManager : MonoBehaviour {

	public enum patterVertical {None, Up, Down, UpStutter, DownStutter};
	public enum patterHorizontal {None, Left, Right, LeftStutter, RightStutter};

	[Tooltip("Choose the horizontal direction in which you pattern should move")]
	public patterVertical patternVerticalAnimation = patterVertical.None;
	[Tooltip("Speed of the pattern animation in vertical directions")]
	public float patternVerticalSpeed = 1.5f;

	[Tooltip("Choose the vertical direction in which you pattern should move")]
	public patterHorizontal patternHorizontalAnimation = patterHorizontal.None;
	[Tooltip("Speed of the pattern animation in horizontal directions")]
	public float patternHorizontalSpeed = 1.5f;

	[Tooltip("If you have a noise pattern and want it to be animated")]
	public bool noise;
	[Tooltip("Speed of the noise animation")]
	public float noiseSpeed = 30;

	[Tooltip("Animate the color intensity")]
	public bool animateColorIntensity;
	[Tooltip("Speed of the color intensity animation")]
	public float intensitySpeed = 8;
	[Tooltip("Range in which the intensity should stay")]
	public float intensityRange = 0.2f;

	private Material thisMat;
	private float startIntensity;
	private float horizontalSpeed, verticalSpeed;
	private float nextNoise;


	void Start () {
		thisMat = GetComponent<Renderer>().material;
		startIntensity = thisMat.GetFloat("_ColorIntensity");
	}
	
	void Update () {
		verticalSpeed = Time.time * patternVerticalSpeed;
		horizontalSpeed = Time.time * patternHorizontalSpeed;

		if( noise ) {
			if( Time.time > nextNoise && noiseSpeed != 0) {
				nextNoise = Time.time + 1/noiseSpeed;
				float noiseX = Random.Range(0, 1.0f);
				thisMat.SetFloat( "_PatternOffsetX", noiseX);
				thisMat.SetFloat( "_PatternOffsetY", noiseX);
			}
		}

		switch (patternVerticalAnimation) {
			case patterVertical.Down:
				thisMat.SetFloat( "_PatternOffsetY", verticalSpeed);
				break;
			case patterVertical.Up:
				thisMat.SetFloat( "_PatternOffsetY", - verticalSpeed);
				break;
			case patterVertical.UpStutter:
			thisMat.SetFloat( "_PatternOffsetY", Mathf.Acos(Mathf.Cos(verticalSpeed * 3f)) * Mathf.Sin(verticalSpeed)  - verticalSpeed );
				break;
			case patterVertical.DownStutter:
				thisMat.SetFloat( "_PatternOffsetY", Mathf.Acos(Mathf.Cos(verticalSpeed * 3f)) * Mathf.Sin(verticalSpeed) + verticalSpeed );
				break;
		}
		switch (patternHorizontalAnimation) {
			case patterHorizontal.Left:
				thisMat.SetFloat( "_PatternOffsetX", horizontalSpeed);
				break;
			case patterHorizontal.Right:
				thisMat.SetFloat( "_PatternOffsetX", - horizontalSpeed);
				break;
			case patterHorizontal.LeftStutter:
				thisMat.SetFloat( "_PatternOffsetX", Mathf.Acos(Mathf.Cos(horizontalSpeed * 3f)) * Mathf.Sin(horizontalSpeed) - horizontalSpeed );
				break;
			case patterHorizontal.RightStutter:
				thisMat.SetFloat( "_PatternOffsetX", Mathf.Acos(Mathf.Cos(horizontalSpeed * 3f)) * Mathf.Sin(horizontalSpeed) + horizontalSpeed );
				break;
		}

		if( animateColorIntensity ) {
			thisMat.SetFloat( "_ColorIntensity", Mathf.Sin(Time.time * intensitySpeed)*intensityRange + startIntensity );
		}
	}
}
