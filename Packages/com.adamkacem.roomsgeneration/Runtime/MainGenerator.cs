using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AdamKacem.RoomsGeneration
{
    public class MainGenerator : MonoBehaviour
    {
        public const string ContainerName = "Generated Map";

        [Tooltip("The map preset to use (size, room sizes, room prefab). Create one with Create > Rooms Generation > Map Settings.")]
        public MapGenerationSettings settings;

        [Tooltip("Same seed + same settings = same map.")]
        public int seed;

        [Tooltip("Pick a new random seed every time a map is generated.")]
        public bool randomSeedEachTime;

        [Tooltip("Generate a new map automatically when entering Play mode. Turn off if you generate in the editor and want to keep that map.")]
        public bool generateOnStart = true;

        [Tooltip("Place the map relative to this object's position instead of the world origin.")]
        public bool useThisObjectPosition = false;

        [SerializeField, HideInInspector] Transform generatedRoot;
        [SerializeField, HideInInspector] List<RoomInfo> rooms = new List<RoomInfo>();

        /// <summary>Fired after a map is generated. Passes the container that holds all rooms. Read Rooms for the room data.</summary>
        public event Action<Transform> OnGenerated;

        public Transform GeneratedRoot => generatedRoot;

        /// <summary>All rooms of the current map (position, size, doors). Empty until a map is generated.</summary>
        public IReadOnlyList<RoomInfo> Rooms => rooms;

        /// <summary>The room that contains this world position, or null.</summary>
        public RoomInfo GetRoomAt(Vector3 worldPosition)
        {
            foreach (RoomInfo room in rooms)
                if (room.Contains(worldPosition)) return room;
            return null;
        }

        /// <summary>The room whose center is farthest from the given room (handy for start/end rooms).</summary>
        public RoomInfo GetFarthestRoomFrom(RoomInfo from)
        {
            RoomInfo farthest = null;
            float best = -1f;
            foreach (RoomInfo room in rooms)
            {
                float d = (room.center - from.center).sqrMagnitude;
                if (d > best) { best = d; farthest = room; }
            }
            return farthest;
        }

        void Start()
        {
            if (generateOnStart) Generate();
        }

        /// <summary>Generate a new map using the given seed.</summary>
        public void Generate(int newSeed)
        {
            seed = newSeed;
            Build();
        }

        /// <summary>Clear the previous map and generate a new one (new random seed if "Random Seed Each Time" is on).</summary>
        public void Generate()
        {
            if (randomSeedEachTime) seed = UnityEngine.Random.Range(0, int.MaxValue);
            Build();
        }

        void Build()
        {
            if (settings == null)
            {
                Debug.LogError("[MainGenerator] No Map Settings assigned. Create one with Create > Rooms Generation > Map Settings.", this);
                return;
            }
            if (settings.roomPrefab == null)
            {
                Debug.LogError("[MainGenerator] The Map Settings has no Room Prefab assigned.", settings);
                return;
            }
            foreach (string warning in settings.GetWarnings())
                Debug.LogWarning("[MainGenerator] " + warning, settings);

            Clear();

            // Some placers still use UnityEngine.Random, so seed it too to keep maps repeatable.
            UnityEngine.Random.InitState(seed);

            generatedRoot = new GameObject(ContainerName).transform;
            generatedRoot.SetParent(transform, false);
            // Keep the container at world origin unless asked otherwise, so rooms land where they always did.
            if (!useThisObjectPosition) generatedRoot.position = Vector3.zero;
            generatedRoot.rotation = Quaternion.identity;

    #if UNITY_EDITOR
            if (!Application.isPlaying) Undo.RegisterCreatedObjectUndo(generatedRoot.gameObject, "Generate Map");
    #endif

            RectInt area = new RectInt(0, 0, settings.mapWidth, settings.mapDepth);
            BSPNode root = new BSPNode(area);
            SeededRandom rng = new SeededRandom(seed);

            root.SplitWithDoors(settings.minRoomSide, rng, settings.allowBigRooms);
            root.CreateRooms(this, rng);

    #if UNITY_EDITOR
            if (!Application.isPlaying) EditorUtility.SetDirty(this);
    #endif

            OnGenerated?.Invoke(generatedRoot);
        }

        /// <summary>Delete the current map (everything under the "Generated Map" container).</summary>
        public void Clear()
        {
            rooms.Clear();

            if (generatedRoot == null)
            {
                Transform found = transform.Find(ContainerName);
                if (found != null) generatedRoot = found;
            }
            if (generatedRoot == null) return;

            if (Application.isPlaying)
            {
                Destroy(generatedRoot.gameObject);
            }
            else
            {
    #if UNITY_EDITOR
                Undo.DestroyObjectImmediate(generatedRoot.gameObject);
    #else
                DestroyImmediate(generatedRoot.gameObject);
    #endif
            }
            generatedRoot = null;
        }

        //with seed
        public void GenerateOneRoom(int width, int height, int openTop, int openBot, int openRight, int openLeft, Vector3 position, SeededRandom rng)
        {
            Vector3 worldPos = generatedRoot != null ? generatedRoot.TransformPoint(position) : position;
            GameObject newRoom = Instantiate(settings.roomPrefab, worldPos, Quaternion.identity, generatedRoot);
            newRoom.name = $"Room {rooms.Count}";
            RoomGenerator roomGenerator = newRoom.GetComponent<RoomGenerator>();
            //initialize roomGenerator
            roomGenerator.Init(width, height, openTop, openBot, openRight, openLeft, rng);

            //generate the room
            roomGenerator.GenerateRoom();

            //remember the room so other scripts can use it
            rooms.Add(new RoomInfo
            {
                index = rooms.Count,
                gameObject = newRoom,
                center = newRoom.transform.position,
                size = new Vector2(width + 2, height + 2),
                doorTop = openTop,
                doorBottom = openBot,
                doorLeft = openLeft,
                doorRight = openRight,
            });
        }


        //without seed
        public void GenerateOneRoom(int width, int height, int openTop, int openBot, int openRight, int openLeft, Vector3 position)
        {
            int roomSeed = UnityEngine.Random.Range(0, 500);
            SeededRandom rng = new SeededRandom(roomSeed);
            GenerateOneRoom(width, height, openTop, openBot, openRight, openLeft, position, rng);
            Debug.Log($"Generated a room at position: {position} with seed = {roomSeed}");
        }
    }
}
