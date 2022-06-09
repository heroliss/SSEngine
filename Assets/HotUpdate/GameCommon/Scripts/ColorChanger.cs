using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace HotUpdate.GameCommon
{
    [Serializable]
    public class ComponentReferenceColorChanger : ComponentReference<ColorChanger>
    {
        public ComponentReferenceColorChanger(string guid) : base(guid) { }
    }

    [RequireComponent(typeof(MeshRenderer))]
    public class ColorChanger : MonoBehaviour
    {
        public Color DefaultColor;
        private MeshRenderer meshRenderer;
        MaterialPropertyBlock propertyBlock;
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            propertyBlock = new MaterialPropertyBlock();
        }

        private void Start()
        {
            //ResetColor();
        }

        public void ResetColor()
        {
            propertyBlock.SetColor("_BaseColor", DefaultColor);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }

        public void SetColor(Color color)
        {
            propertyBlock.SetColor("_BaseColor", color);
            meshRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}
