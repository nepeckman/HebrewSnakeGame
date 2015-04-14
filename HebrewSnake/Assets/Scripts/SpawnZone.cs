using UnityEngine;
using System.Collections;

public class SpawnZone{

	private float xmin;
	private float xmax;
	private float ymin;
	private float ymax;

	public SpawnZone(float xmin, float xmax, float ymin, float ymax){
		this.xmax = xmax;
		this.xmin = xmin;
		this.ymax = ymax;
		this.ymin = ymin;
	}
	 
	public float getXmin(){
		return xmin;
	}

	public float getXmax(){
		return xmax;
	}

	public float getYmin(){
		return ymin;
	}

	public float getYmax(){
		return ymax;
	}

	public Vector2 getSpawnPoint(){
		float x = (float)(Random.Range (xmin, xmax));
		float y = (float)(Random.Range (ymin, ymax));

		return new Vector2 (x, y);
	}
}
