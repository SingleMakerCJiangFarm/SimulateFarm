using Sirenix.OdinInspector;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     点生成器
    /// </summary>
    public sealed class PointGenerator : MonoBehaviour
    {
        [LabelText("左")] public float Left = -15f;
        [LabelText("右")] public float Right = 14f;
        [LabelText("上")] public float Up = 10f;
        [LabelText("下")] public float Down = -20f;
        [LabelText("垂直间隔")] public float Delta = 5f;
        [LabelText("行")] public int Column = 4;
        [LabelText("列")] public int Row = 2;
        [LabelText("点")] public Vector3[] Positions;

        [Button]
        private void GeneratePoints(float y)
        {
            var length = Row * Column;
            Positions = new Vector3[length];
            for (var i = 0; i < length; ++i)
            {
                var t = i / 2;
                var z = Up - t * Delta;
                var x = i % 4 == 0 || i % 4 == 3 ? Left : Right;
                Positions[i] = new Vector3(x, y, z);
            }
        }
    }
}