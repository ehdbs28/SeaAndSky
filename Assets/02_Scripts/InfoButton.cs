using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InfoButton : MonoBehaviour
{
    [System.Serializable]
    struct LightRadious{
        public float min;
        public float max;
    };
    [System.Serializable]
    struct DissolveValue{
        public float min;
        public float max;
    };

    [field:SerializeField] private LightRadious _lightRadious;
    [field:SerializeField] private DissolveValue _dissolveValue;

    private Light2D _light;
    private Material _shader;

    private void Awake() {
        _light = transform.Find("Point Light 2D").GetComponent<Light2D>();
        _shader = transform.Find("InfoGraphic").GetComponent<SpriteRenderer>().material;
    }

    public void OnButton(){
        StopAllCoroutines();
        StartCoroutine(SetLightRadious(true));
        StartCoroutine(SetDissolveValue(true));
    }

    public void OffButton(){
        StopAllCoroutines();
        StartCoroutine(SetLightRadious(false));
        StartCoroutine(SetDissolveValue(false));
    }

    private IEnumerator SetLightRadious(bool isOnButton, float speed = 1f){
        if(isOnButton){
            while(_light.pointLightOuterRadius <= _lightRadious.max){
                _light.pointLightOuterRadius += 0.1f;
                yield return new WaitForSeconds(speed / 1000f);
            }
        }
        else{
            while(_light.pointLightOuterRadius >= _lightRadious.min){
                _light.pointLightOuterRadius -= 0.1f;
                yield return new WaitForSeconds(speed / 1000f);
            }
        }
    }

    private IEnumerator SetDissolveValue(bool isOnButton, float speed = 1f){
        if(isOnButton){
            while(_shader.GetFloat("_CutOffHeight") <= _dissolveValue.max){
                _shader.SetFloat("_CutOffHeight", _shader.GetFloat("_CutOffHeight") + 0.05f);
                yield return new WaitForSeconds(speed / 100f);
            }
        }
        else{
            while(_shader.GetFloat("_CutOffHeight") >= _dissolveValue.min){
                _shader.SetFloat("_CutOffHeight", _shader.GetFloat("_CutOffHeight") - 0.05f);
                yield return new WaitForSeconds(speed / 100f);
            }
        }
    }
}
