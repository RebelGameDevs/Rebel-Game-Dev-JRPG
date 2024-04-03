using UnityEngine;
using RebelGameDevs.Utils.UnrealIntegration;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Mythic
{ 
	public class Case2 : UnrealObject
	{
		private float constant1 = 1, constant2 = 1;
		private float t = 0;
		[SerializeField] private int amountOfPoints = 5000;
		private float lambda1 =>  -a + Mathf.Sqrt(b * c);
		private float lambda2 =>  -a - Mathf.Sqrt(b * c);
		private float a, b, c;
		Vector2 u1 => new Vector2(b / Mathf.Sqrt(b * c), 1);
		Vector2 u2 => new Vector2(-b / Mathf.Sqrt(b * c), 1);
		float Romeo, Juliet;

		[SerializeField] private Transform graphContainer;
		[SerializeField] private GameObject pointPrefab;
		[SerializeField] private float pointPrefabZOffset = -1;
		private List<Vector2> dataPoints = new List<Vector2>();
		private List<Point> romeoPoints; 
		private List<Point> julietPoints;
		[SerializeField] private float xStartPos = 5, yStartPos = -5;
		[SerializeField] private Vector2 graphSize = Vector2.one;
		public Vector2 positionScalars = Vector2.one;
		[SerializeField] private Material RomeoMat, JulietMat;
		public float timeToTake = 3f;
		public float tCap = 1;

		protected override void BeginPlay()
		{	
			SpawnPoints();
		}
		private void SpawnPoints()
		{
			romeoPoints = new List<Point>(); 
			julietPoints = new List<Point>();   
			for(int i = 0; i <= amountOfPoints; i++)
			{
				Point point = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity).GetComponent<Point>();
				point.Initialize(RomeoMat);
				romeoPoints.Add(point);
				point = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity).GetComponent<Point>();
				point.Initialize(JulietMat);
				julietPoints.Add(point);
			}
		}
		public IEnumerator ShowRomeoJulietOptions(float a, float b, float c, int constant1, int constant2, Action finished)
		{
			yield return StartCoroutine(GenerateRandomValues(a, b, c, constant1, constant2));
			yield return UpdateTime();
			finished?.Invoke();
		}
	/*	private IEnumerator test()
		{
			yield return StartCoroutine(GenerateRandomValues(10, 5 ,1, new Vector2(1, 50)));
			yield return UpdateTime();
			yield return new WaitForSeconds(5);
			yield return StartCoroutine(GenerateRandomValues(1, 5 ,10, new Vector2(1, 1)));
			yield return UpdateTime();
			yield return new WaitForSeconds(5);
			yield return StartCoroutine(GenerateRandomValues(1, 1 ,1, new Vector2(1, 20)));
			yield return UpdateTime();
		}*/
		private IEnumerator UpdateTime()
		{
			for(int i = 0; i <= amountOfPoints; i++)
			{ 
				PlotPoints();
				t = ((float)i / (float)amountOfPoints) * tCap;
			}
			yield return StartCoroutine(RenderGraph());
		}
		private void PlotPoints()
		{
			FindValues();
			dataPoints.Add(new Vector2(Romeo, Juliet));
		}
		private IEnumerator GenerateRandomValues(float value1, float value2, float value3, int const1, int const2)
		{
			if(dataPoints.Count > 0)
			{
				foreach(Point point in romeoPoints) StartCoroutine(point.Show(.5f, false));
				foreach(Point point in julietPoints) StartCoroutine(point.Show(.5f, false));
				yield return new WaitForSeconds(1f);
				dataPoints.Clear();
			}
			t = 0;
			a = value1; 
			b = value2;
			c = value3;
			constant1 = const1;
			constant2 = const2;
		}
		private void FindValues()
		{
			//Equation: C{sub1}e^(lambda{sub1}*t)*v + C{sub2}e^(lambda{sub2}*t)*v:
			// => C{sub1}e^(-a+sqrt(bc)t) * u{sub11} + C{sub2}e^(-a-sqrt(bc)*t):
			//=> constant * exponent^{(-a + sqrt(b * c)) * t} + constant * exponent^{(-a - sqrt(b * c)) * t}
			Vector2 romeoRaw;
			romeoRaw.x = constant1 * (Mathf.Pow(Mathf.Exp(1), lambda1 * t) * u1.x);
			romeoRaw.y = constant2 * (Mathf.Pow(Mathf.Exp(1), lambda2 * t) * u2.x);
			Romeo = romeoRaw.x + romeoRaw.y;

			//Check for NAN as 0 / 0 returns nan = 0:
			Romeo = float.IsNaN(Romeo) ? 0 : Romeo;

			Vector2 julietRaw;
			julietRaw.x = constant1 * (Mathf.Pow(Mathf.Exp(1), lambda1 * t) * u1.y);
			julietRaw.y = constant2 * (Mathf.Pow(Mathf.Exp(1), lambda2 * t) * u2.y);
			Juliet = julietRaw.x + julietRaw.y;

			//Check for NAN as 0 / 0 returns nan = 0:
			Juliet = float.IsNaN(Juliet) ? 0 : Juliet;
		}

		private IEnumerator RenderGraph()
		{
			float graphLength = graphSize.x;
			float graphHeight = graphSize.y;

			float halfGraphWidth = graphLength / 2f;
			float xIncrement = graphLength / (float)amountOfPoints; 
			float yIncrement = graphHeight / (float)amountOfPoints;

			
			float timeToWaitPerDot = timeToTake / (amountOfPoints * 2);
			for (int i = 0; i < amountOfPoints; i++)
			{
				//x is time,y is location (y axis):
				Vector3 worldPosition = new Vector3(xStartPos + ((float)(xIncrement * i) * positionScalars.x), yStartPos + ((float)(dataPoints[i].x * yIncrement) * positionScalars.y), 0);
				romeoPoints[i].transform.position = worldPosition + new Vector3(0, 0, pointPrefabZOffset);
				StartCoroutine(romeoPoints[i].Show(.5f, true));

				worldPosition = new Vector3(xStartPos + ((float)(xIncrement * i) * positionScalars.x), yStartPos + ((float)(dataPoints[i].y * yIncrement * positionScalars.y)), 0);
				julietPoints[i].transform.position = worldPosition + new Vector3(0, 0, pointPrefabZOffset);
				StartCoroutine(julietPoints[i].Show(.5f, true));
				yield return new WaitForSeconds(timeToWaitPerDot);
			}
		}
	}

}
