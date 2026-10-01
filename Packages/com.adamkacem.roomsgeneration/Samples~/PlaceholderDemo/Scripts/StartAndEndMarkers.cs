using AdamKacem.RoomsGeneration;
using UnityEngine;

namespace AdamKacem.RoomsGeneration.Samples
{
    /// <summary>
    /// Example of using the room data: every time a map is generated,
    /// put a green marker in the first room and a red marker in the room farthest from it,
    /// and a small yellow marker in every doorway.
    /// Put it on the same object as the MainGenerator.
    /// </summary>
    [ExecuteAlways]
    [RequireComponent(typeof(MainGenerator))]
    public class StartAndEndMarkers : MonoBehaviour
    {
        public bool markDoors = true;

        MainGenerator generator;

        void OnEnable()
        {
            generator = GetComponent<MainGenerator>();
            generator.OnGenerated += PlaceMarkers;
        }

        void OnDisable()
        {
            if (generator != null) generator.OnGenerated -= PlaceMarkers;
        }

        void PlaceMarkers(Transform mapRoot)
        {
            if (generator.Rooms.Count == 0) return;

            RoomInfo start = generator.Rooms[0];
            RoomInfo end = generator.GetFarthestRoomFrom(start);

            CreateMarker("Start", start.center + Vector3.up, 1.5f, Color.green, mapRoot);
            CreateMarker("End", end.center + Vector3.up, 1.5f, Color.red, mapRoot);

            if (!markDoors) return;
            foreach (RoomInfo room in generator.Rooms)
                foreach (Vector3 door in room.GetDoorPositions())
                    CreateMarker("Door", door + Vector3.up * 0.5f, 0.5f, Color.yellow, mapRoot);
        }

        static void CreateMarker(string name, Vector3 position, float size, Color color, Transform parent)
        {
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.name = name;
            marker.transform.SetParent(parent, true);
            marker.transform.position = position;
            marker.transform.localScale = Vector3.one * size;

            Renderer renderer = marker.GetComponent<Renderer>();
            // Copy the default material so we can tint it without changing the shared one.
            renderer.sharedMaterial = new Material(renderer.sharedMaterial) { color = color };

            // Markers are only visual: remove the collider so they don't block anything.
            Object.DestroyImmediate(marker.GetComponent<Collider>());
        }
    }
}
