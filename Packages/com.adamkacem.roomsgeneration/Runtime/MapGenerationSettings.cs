using System.Collections.Generic;
using UnityEngine;

namespace AdamKacem.RoomsGeneration
{
    /// <summary>
    /// A reusable "preset" for how maps are generated. Create one with
    /// right-click in the Project window > Create > Rooms Generation > Map Settings,
    /// then drag it into the MainGenerator's "Settings" slot.
    /// </summary>
    [CreateAssetMenu(fileName = "MapSettings", menuName = "Rooms Generation/Map Settings")]
    public class MapGenerationSettings : ScriptableObject
    {
        /// <summary>
        /// Width of one wall piece in units. The wall models, the filler pieces (0.25 / 0.5 / 0.75)
        /// and the door logic are all built around this size, so it is fixed for now.
        /// </summary>
        public const int WallPieceSize = 4;

        [Tooltip("Room prefab that holds the RoomGenerator and its placers (walls, objects, decorations, floor).")]
        public GameObject roomPrefab;

        [Tooltip("Total map width in units. Keep it a multiple of 4 (one wall piece).")]
        [Min(4)] public int mapWidth = 40;

        [Tooltip("Total map depth in units. Keep it a multiple of 4 (one wall piece).")]
        [Min(4)] public int mapDepth = 40;

        [Tooltip("Shortest allowed side of a room, counted in WALL PIECES (1 piece = 4 units), not units. " +
                 "Rooms end up between this and 2x this per side.")]
        [Min(1)] public int minRoomSide = 3;

        [Tooltip("If on, a room stops splitting as soon as the randomly chosen direction is too short, which can leave long, big rooms.")]
        public bool allowBigRooms;

        /// <summary>Plain-language problems with the current values. Empty list = all good.</summary>
        public List<string> GetWarnings()
        {
            var warnings = new List<string>();

            if (roomPrefab == null)
                warnings.Add("No Room Prefab assigned. Nothing can be generated.");
            else if (roomPrefab.GetComponent<RoomGenerator>() == null)
                warnings.Add("The Room Prefab has no RoomGenerator component.");

            if (mapWidth % WallPieceSize != 0 || mapDepth % WallPieceSize != 0)
                warnings.Add($"Map Width and Map Depth should be multiples of {WallPieceSize}. " +
                             $"The leftover units end up in the rooms along the right and top edges (filler walls, slightly stretched floor).");

            int widthInPieces = mapWidth / WallPieceSize;
            int depthInPieces = mapDepth / WallPieceSize;
            int neededToSplit = 2 * minRoomSide + 1;
            if (widthInPieces < neededToSplit && depthInPieces < neededToSplit)
            {
                int bestSide = (Mathf.Max(widthInPieces, depthInPieces) - 1) / 2;
                string msg = $"The map is too small to split with Min Room Side = {minRoomSide}: you will get ONE big room. " +
                             $"Make the map at least {neededToSplit * WallPieceSize} units on one side";
                msg += bestSide >= 1 ? $", or lower Min Room Side to {bestSide} or less." : ".";
                warnings.Add(msg);
            }

            return warnings;
        }

        /// <summary>Short summary of the room sizes these values produce.</summary>
        public string GetRoomSizeSummary()
        {
            int min = minRoomSide * WallPieceSize;
            int max = 2 * minRoomSide * WallPieceSize;
            return allowBigRooms
                ? $"Rooms: at least {min} units per side (Allow Big Rooms is on, so some can be much longer)."
                : $"Rooms: {min} to {max} units per side.";
        }
    }
}
