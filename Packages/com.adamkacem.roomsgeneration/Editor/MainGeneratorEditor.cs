using UnityEditor;
using UnityEngine;

namespace AdamKacem.RoomsGeneration.EditorTools
{
    [CustomEditor(typeof(MainGenerator))]
    public class MainGeneratorEditor : Editor
    {
        Editor settingsEditor;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MainGenerator gen = (MainGenerator)target;

            // Show the settings asset's fields right here, so you don't have to click the asset to edit it.
            if (gen.settings == null)
            {
                EditorGUILayout.HelpBox("Assign a Map Settings asset.\nCreate one with right-click in the Project window > Create > Rooms Generation > Map Settings.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.Space(6);
                EditorGUILayout.LabelField($"Settings: {gen.settings.name} (shared by everything that uses this asset)", EditorStyles.miniBoldLabel);
                using (new EditorGUI.IndentLevelScope())
                {
                    CreateCachedEditor(gen.settings, null, ref settingsEditor);
                    settingsEditor.OnInspectorGUI();
                }
            }

            EditorGUILayout.Space(8);
            EditorGUILayout.LabelField("Map", EditorStyles.boldLabel);

            using (new EditorGUI.DisabledScope(gen.settings == null))
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Generate", GUILayout.Height(28)))
                {
                    Undo.RecordObject(gen, "Generate Map");
                    gen.Generate();
                }

                if (GUILayout.Button("New Seed + Generate", GUILayout.Height(28)))
                {
                    Undo.RecordObject(gen, "Generate Map");
                    gen.Generate(Random.Range(0, int.MaxValue));
                }
            }

            if (gen.Rooms.Count > 0)
                EditorGUILayout.LabelField($"Current map: {gen.Rooms.Count} rooms (seed {gen.seed})", EditorStyles.miniLabel);

            using (new EditorGUI.DisabledScope(gen.GeneratedRoot == null && gen.transform.Find(MainGenerator.ContainerName) == null))
            {
                if (GUILayout.Button("Clear"))
                {
                    Undo.RecordObject(gen, "Clear Map");
                    gen.Clear();
                }
            }
        }

        [MenuItem("GameObject/Rooms Generation/Map Generator", false, 10)]
        static void CreateGenerator(MenuCommand command)
        {
            var go = new GameObject("Map Generator");
            go.AddComponent<MainGenerator>();
            GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create Map Generator");
            Selection.activeObject = go;
        }

        void OnDisable()
        {
            if (settingsEditor != null) DestroyImmediate(settingsEditor);
        }
    }
}
