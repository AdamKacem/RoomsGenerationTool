using UnityEngine;

namespace AdamKacem.RoomsGeneration
{
    public class FloorPlacer : MonoBehaviour
    {
        RoomGrid room;

        [Tooltip("Floor tile prefab. Its pivot must be at the CENTER of the tile.")]
        public GameObject floorTile;

        [Tooltip("Size of one floor tile in units at its normal scale (X = width, Y = depth).")]
        public Vector2 tileSize = new Vector2(4, 4);

        [Tooltip("Height of the floor above the room's base, to avoid flickering with other surfaces.")]
        public float heightOffset = 0.1f;

        public void Init(RoomGrid room)
        {
            this.room = room;
        }

        public void PlaceFloor()
        {
            if (floorTile == null || tileSize.x <= 0 || tileSize.y <= 0) return;

            // Full room size including the walls (the grid is inset by 1 unit on each side).
            float roomWidth = room.gridWidth + 2;
            float roomDepth = room.gridHeight + 2;

            // How many whole tiles fit on each side (at least 1).
            int tilesX = Mathf.Max(1, Mathf.FloorToInt(roomWidth / tileSize.x));
            int tilesZ = Mathf.Max(1, Mathf.FloorToInt(roomDepth / tileSize.y));

            // Stretch the tiles a little so they cover the room exactly, with no gap.
            // When the room is an exact multiple of the tile size, the stretch is 1 (no change).
            float xStretch = roomWidth / (tilesX * tileSize.x);
            float zStretch = roomDepth / (tilesZ * tileSize.y);

            float stepX = tileSize.x * xStretch;
            float stepZ = tileSize.y * zStretch;

            Vector3 baseScale = floorTile.transform.localScale; // keep the prefab's own scale
            Vector3 tileScale = new Vector3(baseScale.x * xStretch, baseScale.y, baseScale.z * zStretch);

            // Center of the first tile: start at the wall line (1 unit outside the grid), then half a tile in.
            Vector3 first = room.origin + new Vector3(-1f + stepX / 2f, heightOffset, -1f + stepZ / 2f);

            for (int row = 0; row < tilesZ; row++)
            {
                for (int col = 0; col < tilesX; col++)
                {
                    Vector3 position = first + new Vector3(col * stepX, 0, row * stepZ);
                    GameObject floorPiece = Instantiate(floorTile, position, floorTile.transform.rotation, transform);
                    floorPiece.transform.localScale = tileScale;
                }
            }
        }
    }
}
