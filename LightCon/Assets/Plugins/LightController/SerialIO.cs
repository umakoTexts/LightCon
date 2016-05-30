using UnityEngine;
using System.Collections;

public class SerialIO : MonoBehaviour {

	public SerialHandler serialHandler;
	public float x0 = 0;
	public float y0 = 0;
	public float x1 = 0;
	public float y1 = 0;

    //  ボタンの入力
    public int buttonNum = 0;
	//	ボタンが入力されている間はtrue
	public bool isButtonPressed = false;
	public int buttonModeNum;

	void Start () {
		serialHandler.OnDataReceived += OnDataReceived;
	}
	
	void Update () {
	
	}

	void OnDataReceived(string message)
	{
		var data = message.Split(
			new string[]{","}, System.StringSplitOptions.None);
        //toshiki
        //if (data.Length < 4) return;
        if (data.Length < 5) return;

		try {
			x0 = float.Parse(data[0]);
			y0 = float.Parse(data[1]);
			x1 = float.Parse(data[2]);
			y1 = float.Parse(data[3]);
            //toshiki
            buttonNum = int.Parse(data[4]);
			buttonModeNum = int.Parse(data[5]);
			isButtonPressed = (buttonNum == 1) ? true: false;
			
			//GetComponent<Play>().isBulletFire = true;
			//Debug.LogWarning(angleX);
			//Debug.LogWarning(angleY);
		} catch (System.Exception e) {
			Debug.LogWarning(e.Message);
		}
	}

}
