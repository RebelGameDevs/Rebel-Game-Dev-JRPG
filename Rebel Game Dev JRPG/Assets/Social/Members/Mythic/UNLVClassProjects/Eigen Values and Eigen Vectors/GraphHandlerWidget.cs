using RebelGameDevs.Utils.UnrealIntegration;
using UnityEngine;
using System.Collections;
using System;
using RebelGameDevs.Utils;
using TMPro;

namespace Mythic
{
	public class GraphHandlerWidget : UIWidget
	{
		[System.Serializable] private class ParamOption
		{
			public UnrealUIButton incrementBtn;
			public UnrealUIButton decrementBtn;
			public TextMeshProUGUI text;
		}
		[System.Serializable] private class RandomOption
		{
			public TextMeshProUGUI a;
			public TextMeshProUGUI b;
			public TextMeshProUGUI c;
		}
		[SerializeField] private UnrealUIButton openClose;
		[SerializeField] private UnrealUIButton moveCamera;
		[SerializeField] private UnrealUIButton paramSettings;
		[SerializeField] private UnrealUIButton[] UIOptionsBtn;
		[SerializeField] private Animator graphingOptionsAnimator;
		[SerializeField] private Animator paramSettingsAnimator;
		[SerializeField] private RandomOption randomOption;
		[SerializeField] private ParamOption speedOption;
		[SerializeField] private ParamOption timeOption;
		private Vector3 cameraPositionIdle, cameraPositionZoomedOut = new Vector3(0f, 4f, -19f), cameraPositionZoomedOut2x = new Vector3(0f, 17f, -23f);
		private bool isGraphOptionsOpen = false;
		private bool isParamSettingsOpen = false;
		private int currentCameraIndex = 0;
		private Coroutine isAnimating = null;
		private Coroutine isCameraMoving = null;
		private Case2 romeoJulietCase;
		private Action finishedShowingResults;
		private Pawn pawn;
		private UnrealCurve lerpCurve;

		protected override void BeginPlay()
		{
			RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/EaseInEaseOutCurve", out lerpCurve);
			romeoJulietCase = FindObjectOfType<Case2>();
			speedOption.text.SetText(romeoJulietCase.timeToTake.ToString());
			timeOption.text.SetText(romeoJulietCase.tCap.ToString());

			//Lambdas:
			finishedShowingResults += () =>
			{
				foreach(UnrealUIButton btn in UIOptionsBtn) btn.Enable();
				openClose.Enable();
				moveCamera.Enable();
				paramSettings.Enable();
			};
			openClose.onReleased += () =>
			{
				if(isAnimating is not null) return;
				isGraphOptionsOpen = !isGraphOptionsOpen;
				if(isGraphOptionsOpen)
				{
					isAnimating = StartCoroutine(WaitForAnimation(graphingOptionsAnimator, "Open"));
					return;
				}
				isAnimating = StartCoroutine(WaitForAnimation(graphingOptionsAnimator, "Close"));
			};
			moveCamera.onReleased += () =>
			{
				if(isCameraMoving is not null) return;
				currentCameraIndex ++;
				if(currentCameraIndex > 2) currentCameraIndex = 0;

				Vector3 position = currentCameraIndex == 0 ? cameraPositionIdle : currentCameraIndex == 1 ? cameraPositionZoomedOut : cameraPositionZoomedOut2x;
					isCameraMoving = StartCoroutine(ChangeCameraPosition(position));

			};
			paramSettings.onReleased += () =>
			{
				if(isAnimating is not null) return;
				isParamSettingsOpen = !isParamSettingsOpen;
				if(isParamSettingsOpen)
				{
					isAnimating = StartCoroutine(WaitForAnimation(paramSettingsAnimator, "Open"));
					return;
				}
				isAnimating = StartCoroutine(WaitForAnimation(paramSettingsAnimator, "Close"));
			};

			speedOption.incrementBtn.onReleased += () =>
			{
				romeoJulietCase.timeToTake += 0.5f;
				speedOption.text.SetText($"{romeoJulietCase.timeToTake}'s");
			};
			speedOption.decrementBtn.onReleased += () =>
			{
				romeoJulietCase.timeToTake -= 0.5f;
				speedOption.text.SetText($"{romeoJulietCase.timeToTake}'s");
			};
			timeOption.incrementBtn.onReleased += () =>
			{
				romeoJulietCase.tCap += 0.5f;
				timeOption.text.SetText($"t = {romeoJulietCase.tCap}");
			};
			timeOption.decrementBtn.onReleased += () =>
			{
				romeoJulietCase.tCap -= 0.5f;
				timeOption.text.SetText($"t = {romeoJulietCase.tCap}");
			};

			//Min:
			UIOptionsBtn[0].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(0, -5, 0, 0, 0, finishedShowingResults));
				OptionPressed();
			};
			//Middle:
			UIOptionsBtn[1].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(5, 0, 5, 0, 0, finishedShowingResults));
				OptionPressed();
			};
			//Max:
			UIOptionsBtn[2].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(10, 5, 10, 0, 0, finishedShowingResults));
				OptionPressed();
			};
			//Min:
			UIOptionsBtn[3].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(0, -5, 0, 3, 3, finishedShowingResults));
				OptionPressed();
			};
			//Middle:
			UIOptionsBtn[4].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(5, 0, 5, 3, 3, finishedShowingResults));
				OptionPressed();
			};
			//Max:
			UIOptionsBtn[5].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(10, 5, 10, 3, 3, finishedShowingResults));
				OptionPressed();
			};
			//Min:
			UIOptionsBtn[6].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(0, -5, 0, 10, 10, finishedShowingResults));
				OptionPressed();
			};
			//Middle:
			UIOptionsBtn[7].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(5, 0, 5, 10, 10, finishedShowingResults));
				OptionPressed();
			};
			//Max:
			UIOptionsBtn[8].onReleased += () =>
			{
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(10, 5, 10, 10, 10, finishedShowingResults));
				OptionPressed();
			};

			//Random:
			UIOptionsBtn[9].onReleased += () =>
			{
				float a = UnityEngine.Random.Range(0f, 10f);
				float b = UnityEngine.Random.Range(-5f, 5f);
				float c = UnityEngine.Random.Range(0f, 10f);
				randomOption.a.SetText($"a = {a.ToString("F2")}");
				randomOption.b.SetText($"b = {b.ToString("F2")}");
				randomOption.c.SetText($"c = {c.ToString("F2")}");
				StartCoroutine(romeoJulietCase.ShowRomeoJulietOptions(a, b, c, 1, 1, finishedShowingResults));
				OptionPressed();
			};
		}
		public void Setup(Pawn pawnParent)
		{
			pawn = pawnParent;
			cameraPositionIdle = pawn.GetComponentInChildren<Camera>().transform.localPosition;
		}
		
		private void OptionPressed()
		{
			foreach(UnrealUIButton btn in UIOptionsBtn) btn.Disable();
			openClose.Disable();
			moveCamera.Disable();
			paramSettings.Disable();
		}
		
		private IEnumerator WaitForAnimation(Animator anim, string name)
        {
			openClose.Disable();
			paramSettings.Disable();
			moveCamera.Disable();
            anim.CrossFade(name, 0.1f, 0, 0);
            yield return null;
            AnimatorStateInfo currentStateInfo;
            while(true)
            {
                currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
                if(currentStateInfo.IsName(name) && currentStateInfo.normalizedTime >= 1f) break;
                yield return null;
            }
			isAnimating = null;
			openClose.Enable();
			paramSettings.Enable();
			moveCamera.Enable();
        }
		private IEnumerator ChangeCameraPosition(Vector3 positionToLerpTo)
		{
			foreach(UnrealUIButton btn in UIOptionsBtn) btn.Disable();
			openClose.Disable();
			moveCamera.Disable();
			paramSettings.Disable();
			float localTTime = 0;
			Camera cam = pawn.GetComponentInChildren<Camera>();
			Vector3 currentPosition = cam.transform.localPosition;
			while(localTTime < 1)
			{
				localTTime += Time.deltaTime / 1f;
				cam.transform.localPosition = Vector3.Lerp(currentPosition, positionToLerpTo, lerpCurve.CurveEvaluatedOverTime(localTTime));
				yield return null;
			}
			cam.transform.localPosition = positionToLerpTo;
			foreach(UnrealUIButton btn in UIOptionsBtn) btn.Enable();
			openClose.Enable();
			paramSettings.Enable();
			moveCamera.Enable();
			isCameraMoving = null;
		}
	}
}
