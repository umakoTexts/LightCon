using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum LightMode{
	NULL = -1,
	OFF,
	NORMAL,
	BLACK,
	INFRARED
}

public class LightButton : MonoBehaviour {
	
	//	現在のライトモード
	public LightMode _lightMode;
	public bool _keyboardMode = false;
	
	public Light _light;
	
	public Color[] _lightColor = new Color[4];
	public Image _lightIcon = null;
	public Sprite[] _lightIconImage;

	void Start(){
		StartCoroutine(CheckButton());
	}
	
	void Update(){
		if(Input.GetButtonDown("space")){
			Debug.Log("a");
			if((int)_lightMode == 3){
				_lightMode = LightMode.OFF;
			}else{
				_lightMode++;
			}
		}
		switch (_lightMode)
		{
			
			default:break;
		}
	}
	
	IEnumerator CheckButton(){
		while (true)
		{
			if(!_keyboardMode){
				_lightMode = getButtonMode(this.GetComponent<SerialIO>().buttonModeNum);
			}
			_lightIcon.sprite = getLightIconImage(_lightMode);
			_light.color = _lightColor[(int)_lightMode];
			yield return new WaitForSeconds(0.1f);
		}
	}
	
	//	現在のモードを返す
	public LightMode getButtonMode(int buttonModeNum){
		return (LightMode)buttonModeNum;
	}
	
	public Sprite getLightIconImage(LightMode lightMode){
		return _lightIconImage[(int)lightMode];
	}
	public void changeLightIcon(){
		
	}
}
