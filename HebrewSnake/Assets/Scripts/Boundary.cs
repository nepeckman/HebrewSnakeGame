using UnityEngine;
using System.Collections;

public class Boundary{

	public SpawnZone upper_left_zone;
	public SpawnZone upper_right_zone;
	public SpawnZone lower_left_zone;
	public SpawnZone lower_right_zone;

	public Boundary(SpawnZone upper_left_zone, SpawnZone upper_right_zone, SpawnZone lower_left_zone, SpawnZone lower_right_zone){
		this.upper_left_zone = upper_left_zone;
		this.upper_right_zone = upper_right_zone;
		this.lower_left_zone = lower_left_zone;
		this.lower_right_zone = lower_right_zone;
	}

	public Vector2 getSpawnPoint(BoundaryEnum Zone){
		switch (Zone) {
		case BoundaryEnum.UPPER_LEFT:
			return upper_left_zone.getSpawnPoint();
		case BoundaryEnum.UPPER_RIGHT:
			return upper_right_zone.getSpawnPoint();
		case BoundaryEnum.LOWER_LEFT:
			return lower_left_zone.getSpawnPoint();
		case BoundaryEnum.LOWER_RIGHT:
			return lower_right_zone.getSpawnPoint();
		default:
			return new Vector2(0, 0);
		}
	}
}
