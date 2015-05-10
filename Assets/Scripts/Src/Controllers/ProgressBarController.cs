using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Entities;

public class ProgressBarController : MonoBehaviour
{
	public Sprite BlueFill;
	public Sprite RedFill;
	public Sprite YellowFill;
	public Sprite GreenFill;

	[Range(0.0f, 1.0f)]
	public float progress = 1.0f;
	public int timeLeft = 30;
	public PlayerColour color = PlayerColour.Blue;


	private float lastProgress = -1f;
	private int lastTimeLeft = -1;
	private PlayerColour lastColour = PlayerColour.Gray;
	private Image imageRenderer;
	private Text timeLeftLabel;

	void Awake ()
	{
		this.imageRenderer = GetComponent<Image> ();
		this.timeLeftLabel = transform.GetChild (0).GetComponent<Text> ();
	}

	void Update ()
	{
		if (lastProgress != progress)
		{
			this.imageRenderer.fillAmount = this.progress;
			lastProgress = this.progress;
		}

		if (lastColour != color)
		{
			if (color == PlayerColour.Red) this.imageRenderer.sprite = RedFill;
			else if (color == PlayerColour.Yellow) this.imageRenderer.sprite = YellowFill;
			else if (color == PlayerColour.Green) this.imageRenderer.sprite = GreenFill;
			else this.imageRenderer.sprite = BlueFill;
			
			lastColour = this.color;
		}

		if (lastTimeLeft != timeLeft)
		{
			this.timeLeftLabel.text = this.timeLeft + "s";
			lastTimeLeft = timeLeft;
		}
	}
}
