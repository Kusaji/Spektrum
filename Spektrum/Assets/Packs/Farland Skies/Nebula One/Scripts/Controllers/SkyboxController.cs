using Borodar.FarlandSkies.Core.Helpers;
using UnityEngine;

namespace Borodar.FarlandSkies.NebulaOne
{
    [ExecuteInEditMode]
    [HelpURL("http://www.borodar.com/stuff/farlandskies/nebulaone/docs/QuickStart.v1.3.1.pdf")]
    public class SkyboxController : Singleton<SkyboxController>
    {
        public Material SkyboxMaterial;

        // Starfield

        [SerializeField]
        private Cubemap _starfieldCubemap;
        [SerializeField]
        private Color _backgroundColor = Color.black;
        [SerializeField]
        private Color _starsTint = Color.gray;
        [SerializeField]
        [Range(0,1)]
        [Tooltip("Lower threshold of the brightness of stars")]
        private float _starsBrightnessMin = 0f;
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Upper threshold of the brightness of stars")]
        private float _starsBrightnessMax = 1f;

        // Nebula Colors

        [SerializeField]
        [Tooltip("Applies the color filter to mild ripples across the whole skybox, regardless of nebula density")]
        private Color _ambientTint = new Color(0f, .63f, 1f, .03f);
        [SerializeField]
        [Tooltip("Refers to nebula basement haze, which is not affected by ripple distortion")]
        private Color _basementTint = new Color(0f, .63f, 1f, .5f);
        [SerializeField]
        [Tooltip("Applies to first layer of cloudy ripples. Usually, one of most dominant nebula colors")]
        private Color _ripplesTint1 = new Color(0f, .63f, 1f, .26f);
        [SerializeField]
        [Tooltip("Applies to second layer of cloudy ripples. Usually, one of most dominant nebula colors")]
        private Color _ripplesTint2 = new Color(.32f, .15f, .34f, .32f);

        // Nebula Density

        [SerializeField]
        [Tooltip("Defines the shape of the nebula. The brighter the area on the cubemap, the denser and opaque nebula is in this area")]
        private Cubemap _densityCubemap;

        [SerializeField]
        private Vector3 _densityRotation = Vector3.zero;
        private Matrix4x4 _densityRotationMatrix = Matrix4x4.identity;

        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Lower threshold of the nebula's density. Allows adjusting overall nebula thickness without modifying the cubemap")]
        private float _densityThresholdLow = 0.4f;
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("Upper threshold of the nebula's density. Allows adjusting overall nebula thickness without modifying the cubemap")]
        private float _densityThresholdHigh = 0.8f;

        // Nebula Diffusion

        [SerializeField]
        [Tooltip("Determines the appearance of the nebula in the details, as well as final ripple distortion values.")]
        private Cubemap _diffusionCubemap;

        [SerializeField]
        [Tooltip("Affects two layers of nebula cloudy ripples and allows to diversify nebula appearance")]
        private Vector3 _ripplesDistortion = new Vector3(0.2f, 0.1f, 0f);

        // General

        [SerializeField]
        [Range(0, 10f)]
        [Tooltip("Adjusts the brightness of the skybox")]
        private float _exposure = 1f;

        //---------------------------------------------------------------------
        // Properties
        //---------------------------------------------------------------------

        // Background

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                SkyboxMaterial.SetColor("_BackgroundColor", _backgroundColor);
            }
        }

        // Stars

        public Cubemap StarfieldCubemap
        {
            get { return _starfieldCubemap; }
            set
            {
                _starfieldCubemap = value;
                SkyboxMaterial.SetTexture("_StarfieldCube", _starfieldCubemap);
            }
        }

        public Color StarsTint
        {
            get { return _starsTint; }
            set
            {
                _starsTint = value;
                SkyboxMaterial.SetColor("_StarsTint", _starsTint);
            }
        }

        public float StarsBrightnessMin
        {
            get { return _starsBrightnessMin; }
            set
            {
                _starsBrightnessMin = value;
                SkyboxMaterial.SetFloat("_StarsBrightnesslMin", _starsBrightnessMin);
            }
        }

        public float StarsBrightnessMax
        {
            get { return _starsBrightnessMax; }
            set
            {
                _starsBrightnessMax = value;
                SkyboxMaterial.SetFloat("_StarsBrightnesslMax", _starsBrightnessMax);
            }
        }

        // Nebula Colors

        public Color AmbientTint
        {
            get { return _ambientTint; }
            set
            {
                _ambientTint = value;
                SkyboxMaterial.SetColor("_AmbientTint", _ambientTint);
            }
        }

        public Color BasementTint
        {
            get { return _basementTint; }
            set
            {
                _basementTint = value;
                SkyboxMaterial.SetColor("_BasementTint", _basementTint);
            }
        }

        public Color RipplesTint1
        {
            get { return _ripplesTint1; }
            set
            {
                _ripplesTint1 = value;
                SkyboxMaterial.SetColor("_RipplesTint1", _ripplesTint1);
            }
        }

        public Color RipplesTint2
        {
            get { return _ripplesTint2; }
            set
            {
                _ripplesTint2 = value;
                SkyboxMaterial.SetColor("_RipplesTint2", _ripplesTint2);
            }
        }

        // Nebula Density

        public Cubemap DensityCubemap
        {
            get { return _densityCubemap; }
            set
            {
                _densityCubemap = value;
                SkyboxMaterial.SetTexture("_DensityCube", _densityCubemap);
            }
        }

        public Vector3 DensityRotation
        {
            get { return _densityRotation; }
            set
            {
                _densityRotation = value;
                _densityRotationMatrix.SetTRS(Vector3.zero, Quaternion.Euler(_densityRotation), Vector3.one);
                SkyboxMaterial.SetMatrix("_DensityRotation", _densityRotationMatrix);
            }
        }

        public float DensityThresholdLow
        {
            get { return _densityThresholdLow; }
            set
            {
                _densityThresholdLow = value;
                SkyboxMaterial.SetFloat("_DensityThresholdLow", _densityThresholdLow);
            }
        }

        public float DensityThresholdHigh
        {
            get { return _densityThresholdHigh; }
            set
            {
                _densityThresholdHigh = value;
                SkyboxMaterial.SetFloat("_DensityThresholdHigh", _densityThresholdHigh);
            }
        }

        // Nebula Diffusion

        public Cubemap DiffusionCubemap
        {
            get { return _diffusionCubemap; }
            set
            {
                _diffusionCubemap = value;
                SkyboxMaterial.SetTexture("_DiffusionCube", _diffusionCubemap);
            }
        }

        public Vector3 RipplesDistortion
        {
            get { return _ripplesDistortion; }
            set
            {
                _ripplesDistortion = value;
                SkyboxMaterial.SetVector("_RipplesDistortion", _ripplesDistortion);
            }
        }

        // General

        public float Exposure
        {
            get { return _exposure; }
            set
            {
                _exposure = value;
                SkyboxMaterial.SetFloat("_Exposure", _exposure);
            }
        }

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        protected void Awake()
        {
            if (SkyboxMaterial != null)
            {
                RenderSettings.skybox = SkyboxMaterial;
                UpdateSkyboxProperties();
            }
            else
            {
                Debug.LogWarning("SkyboxController: Skybox material is not assigned.");
            }
        }

        protected void OnValidate()
        {
            UpdateSkyboxProperties();
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void UpdateSkyboxProperties()
        {
            if (SkyboxMaterial == null) return;

            // Background
            SkyboxMaterial.SetColor("_BackgroundColor", _backgroundColor);
            // Stars
            SkyboxMaterial.SetTexture("_StarfieldCube", _starfieldCubemap);
            SkyboxMaterial.SetColor("_StarsTint", _starsTint);
            SkyboxMaterial.SetFloat("_StarsBrightnesslMin", _starsBrightnessMin);
            SkyboxMaterial.SetFloat("_StarsBrightnesslMax", _starsBrightnessMax);
            // Nebula Colors
            SkyboxMaterial.SetColor("_AmbientTint", _ambientTint);
            SkyboxMaterial.SetColor("_BasementTint", _basementTint);
            SkyboxMaterial.SetColor("_RipplesTint1", _ripplesTint1);
            SkyboxMaterial.SetColor("_RipplesTint2", _ripplesTint2);
            // Nebula Density
            SkyboxMaterial.SetTexture("_DensityCube", _densityCubemap);
            _densityRotationMatrix.SetTRS(Vector3.zero, Quaternion.Euler(_densityRotation), Vector3.one);
            SkyboxMaterial.SetMatrix("_DensityRotation", _densityRotationMatrix);
            SkyboxMaterial.SetFloat("_DensityThresholdLow", _densityThresholdLow);
            SkyboxMaterial.SetFloat("_DensityThresholdHigh", _densityThresholdHigh);
            // Nebula Duffusion
            SkyboxMaterial.SetTexture("_DiffusionCube", _diffusionCubemap);
            SkyboxMaterial.SetVector("_RipplesDistortion", _ripplesDistortion);
            // General
            SkyboxMaterial.SetFloat("_Exposure", _exposure);
        }
    }
}
