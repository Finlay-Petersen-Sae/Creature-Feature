using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// A simple free camera to be added to a Unity game object.
/// 
/// Keys:
///	wasd / arrows	- movement
///	q/e 			- up/down (local space)
///	r/f 			- switch between cats
///	pageup/pagedown	- up/down (world space)
///	hold shift		- enable fast movement mode
///	right mouse  	- enable free look
///	mouse			- free look / rotation
///     
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Normal speed of camera movement.
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Speed of camera movement when shift is held down,
    /// </summary>
    public float fastMovementSpeed = 100f;

    /// <summary>
    /// Sensitivity for free look.
    /// </summary>
    public float freeLookSensitivity = 3f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel.
    /// </summary>
    public float zoomSensitivity = 10f;

    /// <summary>
    /// Amount to zoom the camera when using the mouse wheel (fast mode).
    /// </summary>
    public float fastZoomSensitivity = 50f;

    /// <summary>
    /// Set to true when free looking (on right mouse button).
    /// </summary>
    private bool looking = true;

    public GameObject PauseMenu;
    public bool isPaused = false;
    private PathDataManager PDM;

    [Header("Debug For Follow Camera")]
    public GameObject targetCat;
    public int curCatIndex;
    public GameObject CatInfoCanvas;
    public Text CatNameText, CatHealthText, CatHungerText, CatThirstText, CatCleanslinessText;

    private void Start()
    {
        PDM = FindObjectOfType<PathDataManager>();
    }

    void Update()
    {
        curCatIndex = Mathf.Clamp(curCatIndex, 0, PDM.CatsObj.Count);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                isPaused = true;

            }
        }
        if (targetCat == null)
        {
            CatInfoCanvas.SetActive(false);
            var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
            }



            if (looking)
            {
                float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
                float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
                transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
            }

            float axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0)
            {
                var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
                transform.position = transform.position + transform.forward * axis * zoomSensitivity;
            }
        }
        else
        {
            CatInfoCanvas.SetActive(true);
            transform.position = new Vector3(targetCat.transform.position.x, 10, targetCat.transform.position.z);
            transform.LookAt(targetCat.transform);

            if (Input.GetMouseButton(0))
            {
                targetCat = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
           
            if (curCatIndex != PDM.CatsObj.Count)
            {
                curCatIndex += 1;
            }
            else
            {
                curCatIndex = 0;
            }
            targetCat = PDM.CatsObj[curCatIndex];
            UpdateCanvas();

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (curCatIndex != 0)
            {
                curCatIndex -= 1;
            }
            else
            {
                curCatIndex = 9;
            }
            targetCat = PDM.CatsObj[curCatIndex];
            UpdateCanvas();
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
        }

     
     

        //else if (Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    StopLooking();
        //}
    }

    public void Save()
    {
        PDM.WriteToSerialization();
        SaveSystem.SaveData();
    }

    void UpdateCanvas()
    {
        CatNameText.text = "Name: " + PDM.CatsObj[curCatIndex].GetComponent<CatStats>().catName;
        CatHealthText.text = "Health: " + PDM.CatsObj[curCatIndex].GetComponent<CatStats>().curHealth;
        CatHungerText.text = "Hunger: " + PDM.CatsObj[curCatIndex].GetComponent<CatStats>().curHunger;
        CatThirstText.text = "Thirst: " + PDM.CatsObj[curCatIndex].GetComponent<CatStats>().curThirst;
        CatCleanslinessText.text = "Cleansliness: " + PDM.CatsObj[curCatIndex].GetComponent<CatStats>().curCleansliness;
    }

    //void OnDisable()
    //{
    //    StopLooking();
    //}

    ///// <summary>
    ///// Enable free looking.
    ///// </summary>
    //public void StartLooking()
    //{
    //    looking = true;
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    ///// <summary>
    ///// Disable free looking.
    ///// </summary>
    //public void StopLooking()
    //{
    //    looking = false;
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}
}