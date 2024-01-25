using Godot;
using System;

public partial class FadeController : Node {
	public async void FadeOut(CanvasItem canvas) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 1f);
		Color endColor = new(og.R, og.G, og.B, 0f);

		while(counter < 1f) {
			Color lerp = startColor.Lerp(endColor, (float) counter);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
	}

	public async void FadeIn(CanvasItem canvas) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 0f);
		Color endColor = new(og.R, og.G, og.B, 1f);

		while(counter < 1f) {
			Color lerp = startColor.Lerp(endColor, (float) counter);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
	}

	public async void FadeOut(CanvasItem canvas, float duration) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 1f);
		Color endColor = new(og.R, og.G, og.B, 0f);

		while(counter < duration) {
			Color lerp = startColor.Lerp(endColor, (float) counter / duration);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
	}

	public async void FadeIn(CanvasItem canvas, float duration) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 0f);
		Color endColor = new(og.R, og.G, og.B, 1f);

		while(counter < duration) {
			Color lerp = startColor.Lerp(endColor, (float) counter / duration);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}
		
		canvas.Modulate = endColor;
	}

	public async void FadeOutWithCallback(CanvasItem canvas, Action action) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 1f);
		Color endColor = new(og.R, og.G, og.B, 0f);

		while(counter < 1f) {
			Color lerp = startColor.Lerp(endColor, (float) counter);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
		action.Invoke();
	}

	public async void FadeInWithCallback(CanvasItem canvas, Action action) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 0f);
		Color endColor = new(og.R, og.G, og.B, 1f);

		while(counter < 1f) {
			Color lerp = startColor.Lerp(endColor, (float) counter);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}
		
		canvas.Modulate = endColor;
		action.Invoke();
	}

	public async void FadeOutWithCallback(CanvasItem canvas, Action action, float duration) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 1f);
		Color endColor = new(og.R, og.G, og.B, 0f);

		while(counter < duration) {
			Color lerp = startColor.Lerp(endColor, (float) counter / duration);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
		action.Invoke();
	}

	public async void FadeInWithCallback(CanvasItem canvas, Action action, float duration) {
		double counter = 0;
		Color og = canvas.Modulate;
		Color startColor = new(og.R, og.G, og.B, 0f);
		Color endColor = new(og.R, og.G, og.B, 1f);

		while(counter < duration) {
			Color lerp = startColor.Lerp(endColor, (float) counter / duration);
			counter += GetProcessDeltaTime();
			canvas.Modulate = lerp;
			await ToSignal(GetTree(), "process_frame");
		}

		canvas.Modulate = endColor;
		action.Invoke();
	}

	/*private void ToggleCanvasEnabled(CanvasItem canvas) {
		canvas.blocksRaycasts = !canvas.blocksRaycasts;
		canvas.interactable= !canvas.interactable;
	}*/
}
