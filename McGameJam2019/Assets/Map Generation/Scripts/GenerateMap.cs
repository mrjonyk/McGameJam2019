﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{

	public int mapHeight;
	public int mapWidth;

	public int rowGap;
	public int colGap;

	public int maxObstacles;
	public int minObstacles;

    public GameObject crate;
	public GameObject barrel;

	private List<GameObject> obstacles;
	private static readonly System.Random rng = new System.Random();
    // Use this for initialization
    void Start()
    {
		obstacles = new List<GameObject>();
		GenerateObstaclesRowPriorityCenterOut(rng.Next(minObstacles, maxObstacles + 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
			for (int i = 0; i < obstacles.Count; i++)
			{
				Destroy(obstacles[i]);
			}
			obstacles.Clear();
            GenerateObstaclesRowPriorityCenterOut(rng.Next(minObstacles, maxObstacles + 1));
        }
    }

	private void GenerateObstaclesRadial(int maxNumToGen)
	{
		Debug.Log("GenerateObstacles: " + maxNumToGen);
		int[] didGenCol = new int[mapWidth];
		int[] didGenRow = new int[mapHeight];
		int leftToGen = maxNumToGen;
		int leftY = mapHeight / 2;
		int rightY = mapHeight / 2 + 1;
		while((leftY >= 0 || rightY <= mapHeight - 1) && leftToGen > 0)
		{
			if (leftY >= 0 && leftToGen > 0)
			{
				leftToGen -= GenerateObstacleRowCenterOut(leftY--, leftToGen);
			}
			if (rightY <= mapHeight - 1 && leftToGen > 0)
			{
				leftToGen -= GenerateObstacleRowCenterOut(rightY++, leftToGen);
			}
		}
	}

	private void GenerateObstaclesRowPriorityCenterOut(int maxNumToGen)
	{
		Debug.Log("GenerateObstacles: " + maxNumToGen);
		int[] columnsGen = new int[mapWidth];
		int leftToGen = maxNumToGen;
		int leftY = mapHeight / 2;
		int rightY = mapHeight / 2 + 1;
		while((leftY >= 0 || rightY <= mapHeight - 1) && leftToGen > 0)
		{
			if (leftY >= 0 && leftToGen > 0)
			{
				leftToGen -= GenerateObstacleRowCenterOut(leftY--, leftToGen);
			}
			if (rightY <= mapHeight - 1 && leftToGen > 0)
			{
				leftToGen -= GenerateObstacleRowCenterOut(rightY++, leftToGen);
			}
		}
	}
	private int GenerateObstacleRowCenterOut(int rowNum, int maxNumToGen)
	{
		int didGen = 0;
		int leftX = mapWidth / 2;
		int rightX = mapWidth / 2 + 1;
		bool didGenerate = false;
		while((leftX >= 0 || rightX <= mapWidth - 1) && didGen < maxNumToGen && mapWidth - didGen > rowGap)
		{
			if (leftX >= 0  && didGen < maxNumToGen && mapWidth - didGen > rowGap)
			{
				didGenerate = GenerateObstacle(leftX--, rowNum);
				if (didGenerate)
				{
					didGen++;
				}
			}
			if (rightX <= mapWidth - 1  && didGen < maxNumToGen && mapWidth - didGen > rowGap)
			{
				didGenerate = GenerateObstacle(rightX++, rowNum);
				if (didGenerate)
				{
					didGen++;
				}
			}
		}
		return didGen;
	}

	private bool GenerateObstacle(int x, int y)
	{
		int toGen = rng.Next(0, 2);
		if (toGen == 1)
		{
			int barrelOrCrate = rng.Next(0, 2);
			GameObject generated = Instantiate((barrelOrCrate == 1) ? barrel : crate) as GameObject;
			generated.SetActive(true);
			generated.transform.Translate(new Vector2(x, y));
			obstacles.Add(generated);
		}

		return toGen == 1;
	}
}