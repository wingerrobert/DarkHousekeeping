using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class GenerateCleanSurfaceMap : MonoBehaviour
{
    [SerializeField] GameObject _planeObject;
    [SerializeField] Texture2D _cleanTexture;

    MeshCollider _collider;
    Texture2D _generatedTexture;
    Material _grimeMaterial;
    
    Ray _mouseRay;
    Vector3 _hitPosition;

    int _textureSizeX = 512;
    int _textureSizeY = 512;

    float _pixelRatioX = 1.0f;
    float _pixelRatioY = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _collider = _planeObject.GetComponent<MeshCollider>();
        _grimeMaterial = GetComponent<DecalProjector>().material;
        _generatedTexture = new Texture2D(_textureSizeX, _textureSizeY);
        _generatedTexture.alphaIsTransparency = true;

        // Determine pixel ratio (texure size / bounds of object)
        _pixelRatioX = _textureSizeX / _collider.sharedMesh.bounds.size.x;
        _pixelRatioY = _textureSizeY / _collider.sharedMesh.bounds.size.z;

        FillTextureWithWhite();
        _grimeMaterial.SetTexture("CleanTexture", _generatedTexture);

        StartCoroutine("DrawPixel");
    }

    void FillTextureWithWhite()
    {
        for (int y = 0; y < _generatedTexture.height; y++)
        {
            for (int x = 0; x < _generatedTexture.width; x++)
            {
                Color color = Color.white;
            }
        }
    }

    IEnumerator DrawPixel()
    {
        for (;;)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _mouseRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                RaycastHit info;

                if (_collider.Raycast(_mouseRay, out info, 1.0f))
                {
                    _hitPosition = info.point;
                    Vector3 centerPosition = _collider.transform.InverseTransformPoint(_hitPosition);

                    Vector3 offsetPosition = new Vector3((centerPosition.x + 5.0f) * _pixelRatioX, 0.0f, (centerPosition.z + 5.0f) * _pixelRatioY);

                    DrawTexture(Mathf.RoundToInt(offsetPosition.x), Mathf.RoundToInt(offsetPosition.z));

                    _generatedTexture.Apply();

                    _grimeMaterial.SetTexture("CleanTexture", _generatedTexture);
                }
            }
            yield return null;
        }
    }

    void DrawTexture(int x, int y)
    {
        for (int cleanY = 0; cleanY < _cleanTexture.height; cleanY++)
        {
            for (int cleanX = 0; cleanX < _cleanTexture.width; cleanX++)
            {
                float dirtyInverseLightness = 1 - _generatedTexture.GetPixel(x + cleanX, y + cleanY).r;
                Color currentCleanPixel = _cleanTexture.GetPixel(cleanX, cleanY);

                Color offsetPixel = new Color(
                    currentCleanPixel.r - dirtyInverseLightness,
                    currentCleanPixel.g - dirtyInverseLightness,
                    currentCleanPixel.b - dirtyInverseLightness,
                    1
                );

                _generatedTexture.SetPixel(x + cleanX, y + cleanY, offsetPixel);
            }
        }
        
        //_generatedTexture.SetPixel(x, y, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Camera.main.transform.position, _mouseRay.direction);
        Gizmos.DrawWireSphere(_hitPosition, 0.1f);
    }
}
