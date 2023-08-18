using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class myCounter : MonoBehaviour
{
	[System.Serializable] public class Car
	{
		public string name = "";
		public int yearModel = 0;
		public Car(string name, int yearModel)
		{
			this.name = name;
			this.yearModel = yearModel;
		}
	}
	[System.Serializable] public class Toyota : Car 
	{
		private string crashedMessage = "";
		public Toyota(string name, int year, string whatToPrint) : base(name, year)
		{
			crashedMessage = whatToPrint;
		}
		public void Crashed()
		{
			Debug.Log(crashedMessage);
		}
	}
	[System.Serializable] public class Ford : Car 
	{
		public Ford(string name, int year) : base(name, year)
		{

		}
	}

	public List<Car> myCars = new List<Car>();


	private void Awake()
	{
		Toyota toyotaCar1 = new Toyota("Toyota", 1996, "Boom!");
		Toyota toyotaCar2 = new Toyota("Toyota", 1996, "Kaboom!");
		Toyota toyotaCar3 = new Toyota("Toyota", 1996, "CarCrashed");
		Ford fordCar = new Ford("Ford", 2001);
		Car basicCar = new Car("Garbage", 2023);

		myCars.Add(basicCar);
		myCars.Add(toyotaCar1);
		myCars.Add(fordCar);
		myCars.Add(toyotaCar2);
		myCars.Add(toyotaCar3);

		Debug.Log("===============");

		foreach(Car item in myCars)
		{
			Debug.Log($"Name: {item.name}, Model: {item.yearModel}");
			if (item is Toyota)
			{
				Toyota carType = (Toyota)item;
				carType.Crashed();
			}
		}

		Debug.Log("===============");
	}
}
