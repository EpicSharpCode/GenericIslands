using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericIslands
{
    public class GI_GenerateScript : MonoBehaviour
    {
        [SerializeField] int xSize = 50;
        [SerializeField] int zSize = 50;
        // масштаб определ€ет, насколько детализирована будет сгенерированна€ карта default - 0.005f
        [SerializeField] float scale = 0.02f;
        [SerializeField] float xOffset = 0;
        [SerializeField] float zOffset = 0;
        // ћаксимальна€ высота, котора€ может быть на ландшафте. Default 255.
        [SerializeField] int maxHeight = 25;

        public static int[,] generateHeightMap(int _xSize, int _zSize, float _scale, float _xOffset, float _zOffset, int _maxHeight = 25)
        {
            // создаем 2D-массив, в котором будет содержатьс€ карта шумов дл€ высот мира.
            // "width" и "height" Ц это встроенные переменные, представл€ющие, соответственно, ширину и высоту окна  
            int[,] heightMap = new int[_xSize, _zSize];

            for (int x = 0; x != _xSize; x++)
            {
                for (int z = 0; z != _zSize; z++)
                {
                    // сгенерировать шум ѕерлина на основе координат x,y 
                    // умножение координат на масштаб вли€ет на детализацию сгенерированной карты 
                    // функци€ noise() возвращает число в диапазоне от 0 до 1
                    float value = Mathf.PerlinNoise((x + _xOffset) * _scale, (z + _zOffset) * _scale);

                    // выразить на карте сгенерированный шум между 0 и maxHeight, затем округлить
                    int h = Mathf.RoundToInt(Mathf.Lerp(0, _maxHeight, value));
                    // наконец, установить высоту актуальной точки на карте 
                    heightMap[x,z] = h;
                }
            }

            return heightMap;
        }

        private void Start()
        {
            var heightMap = generateHeightMap(xSize, zSize, scale, xOffset, zOffset, maxHeight);

            for(int x = 0; x < xSize; x++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.SetParent(transform, false);
                    go.transform.position = new Vector3(x, heightMap[x,z], z);
                }
            }
        }

        public class GeneratedParams
        {
            public int xSize = 50;
            public int zSize = 50;
            public float scale = 0.02f;
            public float xOffset = 0;
            public float zOffset = 0;
            public int maxHeight = 25;

            public GeneratedParams(int _xSize, int _zSize, float _scale, float _xOffset, float _zOffset, int _maxHeight)
            {
                xSize = _xSize;
                zSize = _zSize;
                scale = _scale;
                xOffset = _xOffset;
                zOffset = _zOffset;
                maxHeight = _maxHeight;
            }

            public bool CheckEqual(GeneratedParams _params)
            {
                if (xSize != _params.xSize) { return false; }
                if (zSize != _params.zSize) { return false; }
                if (scale != _params.scale) { return false; }
                if (xOffset != _params.xOffset) { return false; }
                if (zOffset != _params.zOffset) { return false; }
                if (maxHeight != _params.maxHeight) { return false; }
                return true;
            }
        }
    }
}
