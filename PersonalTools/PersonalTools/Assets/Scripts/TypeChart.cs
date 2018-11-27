using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;

[CreateAssetMenu(fileName = "New_TypeChart", menuName = "Pokedex/Create Type Chart")]
public class TypeChart : ScriptableObject
{
	[TableMatrix(HorizontalTitle = "Defender", VerticalTitle = "Attacker")]
	[ShowInInspector]
	public float[,] typeChart;

	[PropertyOrder(1)]
	[Button(ButtonSizes.Medium)]
	private void InitializeMatrix()
	{
		int matrixSize = 18;//Enum.GetValues(typeof(PokemonTypes)).Length;
		typeChart = new float[matrixSize,matrixSize];
		
		string dataPath = "Assets/Resources/TypeChart.txt";
		using (var streamReader = new StreamReader(dataPath))
		{
			int i = 0;
			while (!streamReader.EndOfStream)
			{
				string line = streamReader.ReadLine();
				for (int j = 0; j < matrixSize; j++)
				{
					switch (line[j])
					{
						case '0':
							typeChart[i, j] = 0f;
							break;
						
						case '1':
							typeChart[i, j] = 0.5f;
							break;
						
						case '2':
							typeChart[i, j] = 1;
							break;
						
						case '3':
							typeChart[i, j] = 2;
							break;
					}
				}
				i++;
			}
		}
	}

	private void OnEnable()
	{
		InitializeMatrix();
	}
}