using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BTLGeek.Manager;
using BTLGeek.Constants;

namespace BTLGeek.UI
{
	public class ToTitleScene : MonoBehaviour
	{
		public void OnClick()
		{
			ResultSceneController.Instance.FadeStart(SceneName.TITLE_SCENE, FADE_TYPE.FADE_OUT);
			SoundManager.Instance.PlaySeByName(SEPath.DECISION5);
			EventSystem.current.gameObject.SetActive(false);
		}
	}
}
