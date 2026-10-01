using UnityEditor;

namespace AdamKacem.RoomsGeneration.EditorTools
{
    [CustomEditor(typeof(MapGenerationSettings))]
    public class MapGenerationSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            serializedObject.ApplyModifiedProperties();

            MapGenerationSettings s = (MapGenerationSettings)target;

            EditorGUILayout.HelpBox(
                $"Wall piece size: {MapGenerationSettings.WallPieceSize} units (fixed).\n{s.GetRoomSizeSummary()}",
                MessageType.None);

            foreach (string warning in s.GetWarnings())
                EditorGUILayout.HelpBox(warning, MessageType.Warning);
        }
    }
}
