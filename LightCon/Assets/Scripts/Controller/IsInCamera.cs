using UnityEngine;
using System.Collections;

public class IsInCamera : MonoBehaviour {

	//	ライトに付けているカメラのタグ名
	private const string LIGHT_CONTROLLER_CAMERA_TAG_NAME = "LightControllerCamera";
	public LightButton _lightButton;
	
	//	カメラに表示されているか
	public bool _isRendered = false;
	
	private Renderer _myRenderer = null;
	private Color _myColor;
	
	void Start () {
		_myRenderer = this.GetComponent<Renderer>();
		_myColor = _myRenderer.material.color;
	}
	
	void Update () {
		if(_isRendered){
			Debug.Log("カメラに写っている");
			if(_myRenderer.material.color.a > 0){
				if( _lightButton._lightMode == LightMode.INFRARED)
					_myRenderer.material.color -= new Color(0,0,0, Time.deltaTime);
			}else{
				_myRenderer.material.color = new Color(_myColor.r, _myColor.g, _myColor.b, 0);
			}
		}else{
			if(_myRenderer.material.color.a < _myColor.a){
				_myRenderer.material.color += new Color(0,0,0, Time.deltaTime * 2);
			}else{
				_myRenderer.material.color = _myColor;
			}
		}
		_isRendered = false;
		Debug.Log(_myRenderer.material.color);
	}
	
	private void OnWillRenderObject(){
		//	カメラに映った時だけ _isRendered を有効に
		if(Camera.current.tag == LIGHT_CONTROLLER_CAMERA_TAG_NAME){
			_isRendered = true;
		}
	}
}
