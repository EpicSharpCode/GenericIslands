using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericIslands
{
    public class GI_MovableMeshGeneratorScript : GI_MeshGeneratorScript
    {
        [Header("Movable Settings")]
        [SerializeField] float xOffsetSpeed = 0;
        [SerializeField] float zOffsetSpeed = 0;

        // Update is called once per frame
        void Update()
        {
            base.Update();
            AddOffset(xOffsetSpeed * Time.deltaTime, zOffsetSpeed * Time.deltaTime);
        }
    }
}
