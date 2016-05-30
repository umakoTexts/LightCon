using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*	
*	ライトコントローラーが接続されているか確認する
*	指定秒コントローラーの値の変動がなければ、
*	ポップアップを表示して、
*	「キーボード＋マウスモードでプレイ」と「再接続確認を行う」選択肢を出す。
*/	
public class CheckInputFromLight : MonoBehaviour {

	//	確認する時間
	public float checkTime_sec;
	
	//	入力があるか
	public bool isInputFromLight = false;
	
	//	ポップアップウィンドウ
	public GameObject popupWindow = null;
	//	ポップアップウィンドウ格納場所
	private GameObject popupWindow_obj = null;
	//	ポップアップウィンドウ用のCanvas
	public Canvas canvas = null;
	
	private IEnumerator checkValueChange(float checkTime_sec){
		//	経過時間
		float elapsedTime = 0f;
		//	入力があったか
		bool[] isInputed = new bool[4];
		
		while (true){
			//	処理
			//	HACK(田原):もっと綺麗に書けるはず
			//	もし、フラグが立っていなければ引き続き変化をチェックする。
			isInputed[0] = !isInputed[0] ?
				((this.GetComponent<SerialIO>().x1 != 0) ? true : false) : false;
				
			isInputed[1] = !isInputed[1] ?
				((this.GetComponent<SerialIO>().y0 != 0) ? true : false) : false;
			
			isInputed[2] = !isInputed[2] ?
				((this.GetComponent<SerialIO>().y1 != 0) ? true : false) : false;
			
			isInputed[3] = !isInputed[3] ? 
				((this.GetComponent<SerialIO>().isButtonPressed == true) ? true : false) : false;
			
			yield return null;
			
			elapsedTime += Time.deltaTime;	// 指定秒数のカウント
			
			//	指定時間になったら
			if(elapsedTime > checkTime_sec){
				//	入力があったか調べる
				
				int clearNum = 0;	//	入力確認個数
				string message = "次の入力を確認できませんでした。\n";
				
				for (int i = 0; i < isInputed.Length; i++){
					if(isInputed[i] == true){
						clearNum++;
					}else{
						switch (i)
						{
							case 0: message += "X1 "; break;
							case 1: message += "Y0 "; break;
							case 2: message += "Y1 "; break;
							case 3: message += "BUTTON"; break;
							default:break;
						}
					}
					message += "\n";
					Debug.Log("InputFromLight " + i + " :" + isInputed[i]);
				}
				//	ポップアップウィンドウを生成
				createPopupWindow();
				
				isInputFromLight = clearNum == isInputed.Length;
				showResultInPopupWindow(clearNum == isInputed.Length, message);
				//	チェック終了
				yield break;
			}
		}
	}
	
	void createPopupWindow(){
		//	ポップアップウィンドウを生成

		popupWindow_obj = Instantiate(popupWindow) as GameObject;
		popupWindow_obj.transform.SetParent(canvas.transform,false);
	}
	
	public void checkEntry(){
		//	指定秒数コントローラーの値に変化をcheck
		StartCoroutine(checkValueChange(checkTime_sec));
	}
	
	//	ポップアップウィンドウに結果を表示する
	public void showResultInPopupWindow(bool result, string mes){
		string title;
		title = result ? "認識成功" : "認識失敗";
		mes = result ? "コントローラーの認識に成功しました。\nコントローラーモードで開始しますか？"
					 : mes + "もう一度確認しますか？\nしない場合はキーボードモードで開始します。";
		
		//	表示
		popupWindow.transform.Find("Title/Title_Text").GetComponent<Text>().text = title;
		popupWindow.transform.Find("Mes/Mes_Text").GetComponent<Text>().text = mes;
	}
	public void pushPopupButton(bool yes){
		
		if(yes){
			//	yesボタンの時
			if(isInputFromLight){
				//	認識成功なら
				
			}else{
				//	認識失敗なら
				checkEntry();
			}
		}else{
			//	noボタンの時
			if(isInputFromLight){
				//	認識成功なら
				
			}else{
				//	認識失敗なら
			}
		}
	}
}
