using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoVerticalSize : MonoBehaviour {

	public int childHeight;

	// Use this for initialization
	void Start () {
		AdjustSize();
	}
	
	public void AdjustSize(){
		RectTransform rect = gameObject.GetComponent<RectTransform>();
		Vector2 size = rect.sizeDelta;
		size.y = this.transform.childCount * childHeight;
		rect.sizeDelta = size;
	}
}
