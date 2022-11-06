//using System;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.EditorTools;

//[EditorTool("Move Right")]
//class PlatformTool : EditorTool
//{

//    public override void OnToolGUI(EditorWindow window)
//    {
//        window.ShowNotification(new GUIContent("Move Right"));

//        EditorGUI.BeginChangeCheck();

//        Vector3 position = Tools.handlePosition;

//        using (new Handles.DrawingScope(Color.green))
//        {
//            position = Handles.Slider(position, Vector3.right);
//        }

//        //如果在调用 EditorGUI.BeginChangeCheck 后 GUI 状态发生更改，则返回 true，否则返回 false。
//        if (EditorGUI.EndChangeCheck())
//        {
//            Vector3 delta = position - Tools.handlePosition;

//            Undo.RecordObjects(Selection.transforms, "Move Right");

//            foreach (var transform in Selection.transforms)
//                transform.position += delta;
//        }
//    }
//}