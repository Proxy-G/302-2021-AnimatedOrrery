using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powers_MouseLookTut : MonoBehaviour
{
    public CanvasGroup normalUI;
    
    private bool tutCompleted = false;
    private Image tutImage;
    private Vector2 camDirection = Vector2.zero;

    private float tutAlpha;
    private float normAlpha;

    private void Start()
    {
        //Get the tut mouse image component and set the tut alpha variable.
        tutImage = GetComponent<Image>();
        tutAlpha = tutImage.color.a;

        //Disable the normal UI for the camera rotate tutorial
        normalUI.alpha = 0;
        normalUI.interactable = false;
    }
    void Update()
    {
        //Check if player is holding down right click
        if (Input.GetMouseButton(1) && !tutCompleted)
        {
            //Get mouse movement
            camDirection.x -= Input.GetAxis("Mouse Y");
            camDirection.y += Input.GetAxis("Mouse X");

            //If player has moved mouse, complete tutorial
            if (camDirection.magnitude > 5f) tutCompleted = true;
        }

        if (tutCompleted)
        {
            //If tut alpha is not 0, lerp it towards 0.
            if(tutAlpha != 0)
            {
                //Set tut alpha to current image alpha
                tutAlpha = tutImage.color.a;

                //Lerp alpha down to 0 if not close, otherwise set to 0
                if (tutAlpha > 0.01f) tutAlpha = Powers_AnimMath.Lerp(tutImage.color.a, 0, 0.05f, false);
                else tutAlpha = 0;

                //Set image color alpha to temp alpha
                tutImage.color = new Color(tutImage.color.r, tutImage.color.g, tutImage.color.b, tutAlpha);
            }
                
            //If image color alpha is 0 and canvas group alpha is not 1, start work on norm UI alpha.
            else if (tutImage.color.a == 0 && normalUI.alpha != 1)
            {
                //Set tut alpha to current image alpha
                normAlpha = normalUI.alpha;

                //Lerp alpha down to 0 if not close, otherwise set to 0
                if (normAlpha < 0.99f) normAlpha = Powers_AnimMath.Lerp(normalUI.alpha, 1, 0.05f, false);
                else normAlpha = 1;

                //Set image color alpha to temp alpha
                normalUI.alpha = normAlpha;
            }

            //If normal UI alpha is now 1, the tut is complete. Set canvas group interactable to true and destroy this.
            else if (tutImage.color.a == 0 && normalUI.alpha == 1)
            {
                normalUI.interactable = true;
                Destroy(this);
            }
        }
    }
}
