using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherControl : MonoBehaviour
{
    private int _choice = -1;
    [SerializeField] private Color[] _fogColours;
    [SerializeField] private Light _mainLight;
    [SerializeField] private Texture[] _textures;
    [SerializeField] private Material _skyBoxBlend;

    public float cycleDelay = 60.0f;

	private void Start()
	{
        RenderSettings.fogColor = _fogColours[0];
        _mainLight.color = _fogColours[0];
        StartCoroutine(FadeSky());
    }

    private IEnumerator FadeSky()
    {
        while (true)
        {
            if (_choice < _fogColours.Length - 1) _choice++;
            else _choice = 0;

            if (_choice != _fogColours.Length - 1)
            {
                _skyBoxBlend.SetTexture("_FrontTex", _textures[_choice * 6]);
                _skyBoxBlend.SetTexture("_BackTex", _textures[(_choice * 6) + 1]);
                _skyBoxBlend.SetTexture("_LeftTex", _textures[(_choice * 6) + 2]);
                _skyBoxBlend.SetTexture("_RightTex", _textures[(_choice * 6) + 3]);
                _skyBoxBlend.SetTexture("_UpTex", _textures[(_choice * 6) + 4]);
                _skyBoxBlend.SetTexture("_DownTex", _textures[(_choice * 6) + 5]);

                _skyBoxBlend.SetTexture("_FrontTex2", _textures[(_choice * 6) + 6]);
                _skyBoxBlend.SetTexture("_BackTex2", _textures[(_choice * 6) + 7]);
                _skyBoxBlend.SetTexture("_LeftTex2", _textures[(_choice * 6) + 8]);
                _skyBoxBlend.SetTexture("_RightTex2", _textures[(_choice * 6) + 9]);
                _skyBoxBlend.SetTexture("_UpTex2", _textures[(_choice * 6) + 10]);
                _skyBoxBlend.SetTexture("_DownTex2", _textures[(_choice * 6) + 11]);
            }

            else
            {
                _skyBoxBlend.SetTexture("_FrontTex", _textures[_choice * 6]);
                _skyBoxBlend.SetTexture("_BackTex", _textures[(_choice * 6) + 1]);
                _skyBoxBlend.SetTexture("_LeftTex", _textures[(_choice * 6) + 2]);
                _skyBoxBlend.SetTexture("_RightTex", _textures[(_choice * 6) + 3]);
                _skyBoxBlend.SetTexture("_UpTex", _textures[(_choice * 6) + 4]);
                _skyBoxBlend.SetTexture("_DownTex", _textures[(_choice * 6) + 5]);

                _skyBoxBlend.SetTexture("_FrontTex2", _textures[0]);
                _skyBoxBlend.SetTexture("_BackTex2", _textures[1]);
                _skyBoxBlend.SetTexture("_LeftTex2", _textures[2]);
                _skyBoxBlend.SetTexture("_RightTex2", _textures[3]);
                _skyBoxBlend.SetTexture("_UpTex2", _textures[4]);
                _skyBoxBlend.SetTexture("_DownTex2", _textures[5]);
            }

            float change = 0.0f;
            while (change < 1.0f)
            {
                change += 0.01f;

                _skyBoxBlend.SetFloat("_Blend", change);
                RenderSettings.skybox = _skyBoxBlend;

                if (_choice != _fogColours.Length - 1)
                    RenderSettings.fogColor = _mainLight.color = Color.Lerp(_fogColours[_choice], _fogColours[_choice + 1], change);

                else RenderSettings.fogColor = _mainLight.color = Color.Lerp(_fogColours[_choice], _fogColours[0], change);
                yield return new WaitForSeconds(cycleDelay / 100.0f);
            }
        }
    }
}
