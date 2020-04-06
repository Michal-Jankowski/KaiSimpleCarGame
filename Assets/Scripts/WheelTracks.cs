using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTracks : MonoBehaviour
{
    private RenderTexture _splatmap;
    public Shader _drawShader;
    private Material myMaterial, drawMaterial;
    public GameObject _terrain;
    public Transform[] _wheel;
    [Range(0, 2)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrength;
    RaycastHit _groundHit;
    int _layerMask;
    // Start is called before the first frame update
    void Start() {
        _layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(_drawShader);

        myMaterial = _terrain.GetComponent<MeshRenderer>().material;
        myMaterial.SetTexture("_Splat", _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _wheel.Length; i++) {

            if (Physics.Raycast(_wheel[i].position , Vector3.down, out _groundHit, 1f, _layerMask)) {

                drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Srength", _brushStrength);
                drawMaterial.SetFloat("_Size", _brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }
}
