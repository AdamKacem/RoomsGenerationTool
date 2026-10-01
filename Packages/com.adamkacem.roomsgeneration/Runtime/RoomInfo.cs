using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdamKacem.RoomsGeneration
{
    /// <summary>
    /// Everything other scripts need to know about one generated room:
    /// where it is, how big it is, and where its doors are.
    /// Read them from MainGenerator.Rooms after a map is generated.
    /// </summary>
    [Serializable]
    public class RoomInfo
    {
        /// <summary>Order in which the room was created (0, 1, 2...).</summary>
        public int index;

        /// <summary>The room's GameObject (walls, props and floor are its children).</summary>
        public GameObject gameObject;

        /// <summary>World position of the room's center, at floor level.</summary>
        public Vector3 center;

        /// <summary>Room size in units, walls included (X = width, Y = depth).</summary>
        public Vector2 size;

        /// <summary>Door position on each side, counted in wall pieces from the start of that wall. -1 = no door on that side.</summary>
        public int doorTop = -1, doorBottom = -1, doorLeft = -1, doorRight = -1;

        /// <summary>The room's area on the ground, walls included.</summary>
        public Bounds Bounds => new Bounds(center, new Vector3(size.x, 0f, size.y));

        /// <summary>Floor area in square units (walls included).</summary>
        public float Area => size.x * size.y;

        /// <summary>Number of doors this room has.</summary>
        public int DoorCount =>
            (doorTop >= 0 ? 1 : 0) + (doorBottom >= 0 ? 1 : 0) + (doorLeft >= 0 ? 1 : 0) + (doorRight >= 0 ? 1 : 0);

        /// <summary>True if the point (ignoring height) is inside this room.</summary>
        public bool Contains(Vector3 worldPoint)
        {
            return Mathf.Abs(worldPoint.x - center.x) <= size.x / 2f &&
                   Mathf.Abs(worldPoint.z - center.z) <= size.y / 2f;
        }

        /// <summary>World positions of the middle of each doorway (at floor level).</summary>
        public List<Vector3> GetDoorPositions()
        {
            float piece = MapGenerationSettings.WallPieceSize;
            float left = center.x - size.x / 2f;
            float right = center.x + size.x / 2f;
            float bottom = center.z - size.y / 2f;
            float top = center.z + size.y / 2f;

            var doors = new List<Vector3>();
            if (doorBottom >= 0) doors.Add(new Vector3(left + doorBottom * piece + piece / 2f, center.y, bottom));
            if (doorTop >= 0) doors.Add(new Vector3(left + doorTop * piece + piece / 2f, center.y, top));
            if (doorLeft >= 0) doors.Add(new Vector3(left, center.y, bottom + doorLeft * piece + piece / 2f));
            if (doorRight >= 0) doors.Add(new Vector3(right, center.y, bottom + doorRight * piece + piece / 2f));
            return doors;
        }
    }
}
