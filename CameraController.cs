using UnityEngine;
using System.Collections;

public delegate void deactivateInfo ();

public class CameraController : MonoBehaviour
{
	public bool rotation = true;
		public Texture2D cursorText;
		public int xDelayCursor = 0;
		public int yDelayCursor = 0;
		[Range (1f,10f)]
		public float
				smoothing = 5f;
	[Range (0.05f,10f)]
		public float
				Sens = 0.5f;
		float upwardCamPos;
		public deactivateInfo deac;
	public float ZoomMax=300f,ZoomMin=10f;
		//здесь все для кликанья на обьектах
		Ray clickRay;
		RaycastHit target;
		GameObject targetOb;
		bool targetLook = false;
		bool flag = false;
		//

//		public Transform station;
		Transform cam;
		Vector3 locPos;

//	Vector3 rotation;
//		StationController stationController;
		Transform Pod;
		[SerializeField]
		float
				xRot;
		[SerializeField]
		float
				yRot;
		[SerializeField]
		float
				zZoom;
		Vector3 zoom;
		Camera cameraComp;
		[SerializeField]
		float
				velocity;
		Vector3 lastpos;

		//для вычисления линейной функции по которой меняется уголОбзора камеры в зависимости от скорости ее движения.
		public float MaxSpeedOfDistortion = 20000f;
		float K;
		float B = 50;

		

	void Start ()
		{
		Screen.orientation = ScreenOrientation.Landscape;
				Cursor.SetCursor (cursorText, new Vector2 (xDelayCursor, yDelayCursor), CursorMode.Auto);
				K = 80 / (MaxSpeedOfDistortion - 2.6f);
				Pod = transform.parent.transform;
				cam = transform;
				lastpos = Pod.position;
				locPos = cam.localPosition;
				
				cameraComp = GetComponent <Camera> ();
				upwardCamPos = cam.localPosition.y;
				zoom.Set (0, upwardCamPos, cam.localPosition.z);
		prevRotation = Pod.rotation;

	

	
		}
	float addX,addY;
	Quaternion prevRotation;
		

	void MouseLook ()//метод следования камеры за кораблем
		{

        xRot += Input.GetAxis("Mouse X");
        yRot -= Input.GetAxis("Mouse Y");

        Pod.localRotation = Quaternion.Lerp(Pod.localRotation, Quaternion.Euler(yRot * Sens, xRot * Sens, 0), smoothing * Time.deltaTime);




        //		if (Input.touchCount > 0) {
        //			if (Input.GetTouch (0).phase == TouchPhase.Began) {

        //				prevRotation = Pod.rotation; 
        //			} 

        ////
        ////				xRot += Input.GetAxis ("Mouse X");
        ////				yRot -= Input.GetAxis ("Mouse Y");
        ////
        //			addY = Input.GetTouch (0).deltaPosition.x * Sens;
        //			addX = Input.GetTouch (0).deltaPosition.y * Sens / 5;


        //			addY = Mathf.Clamp (addY, -15, 15);
        //			yRot += addY;
        //			xRot += addX;
        //			xRot = Mathf.Clamp (xRot, -90, 90);
        //		}

        ////				Debug.Log ("rotate");
        ////		Pod.rotation = Quaternion.Lerp (Pod.rotation, Quaternion.Euler (Pod.eulerAngles.x + xRot ,Pod.eulerAngles.y+ yRot , 0), smoothing * Time.deltaTime);
        //		Pod.rotation = Quaternion.Lerp (Pod.rotation, Quaternion.Euler (xRot, yRot, 0), smoothing * Time.deltaTime);
        ////		Pod.rotation = Pod.rotation*Quaternion.Euler(xRot,yRot,0);
        ////		Pod.eulerAngles =Vector3.Lerp(Pod.eulerAngles, new Vector3(Pod.eulerAngles.x+xRot,Pod.eulerAngles.y+ yRot,0), Time.deltaTime*smoothing);


    }

    void MouseLookAtTarget (GameObject targeting)//метод наблюдения за целью
		{
//				Debug.Log ("Looking");
//		Pod.rotation = Quaternion.Lerp (Pod.rotation, Quaternion.LookRotation (targeting.transform.position - cam.position) , smoothing * Time.deltaTime);
				cam.rotation = Quaternion.Lerp (cam.rotation, Quaternion.LookRotation (targeting.transform.position - cam.position), smoothing * Time.deltaTime);

		}
	[SerializeField]
	float prevDistanceBetweenTouches = 0f;
	[SerializeField]
	float speedOfZoom;
		

	void MouseZoom ()
		{
        //speedOfZoom = -(prevDistanceBetweenTouches -Vector2.Distance (Input.touches [0].position , Input.touches [1].position))*Time.deltaTime;
        //prevDistanceBetweenTouches = Vector2.Distance (Input.touches [0].position , Input.touches [1].position);

        //zZoom = Mathf.Clamp (( zZoom + speedOfZoom * Sens * 10), -ZoomMax, -ZoomMin);

        //		zoom.Set (0, upwardCamPos, zZoom);



        zZoom = Mathf.Clamp((zZoom + Input.GetAxis("Mouse ScrollWheel") * Sens * 10), -ZoomMax, -ZoomMin);

        zoom.Set(0, upwardCamPos, zZoom);
    }


	void FixedUpdate ()
		{



				if (Input.GetKey (KeyCode.Escape))
						Application.Quit ();
				velocity = Vector3.Distance (Pod.position, lastpos) / Time.deltaTime;
				lastpos = Pod.position;
				cameraComp.fieldOfView = Mathf.Clamp ((velocity * K + B), 50, 130);
    }



    void LateUpdate()
    {
        //здесь блок кода для наведения на цель
        #region Target look 
        ////здесь проверяется включен ли флаг нацеливания и есть ли обьект на который нацеливаться и меняется цвет текста над обьектом на желтый, и выставляется флаг который означает следит ли в данный момент камера за кем то или нет
        //if (targetLook && targetOb != null && !Input.GetMouseButton(2))
        //{

        //    if (targetOb.GetComponent<InfoBot1>())
        //        targetOb.GetComponent<InfoBot1>().colorOfLabel = Color.yellow;
        //    else
        //    {
        //        targetOb.AddComponent<InfoBot1>();
        //        targetOb.GetComponent<InfoBot1>().colorOfLabel = Color.yellow;
        //    }
        //    MouseLookAtTarget(targetOb);
        //    flag = true;

        //    //если же нету обьектов для слежки и флаг того что за кем то камера следит положительный,значит надо деактивировать слежку за кем то и сбросить флаг обратно чтобы деактивация не происходила каждый кадр
        //}
        //else if (flag)
        //{

        //    deac();
        //    flag = false;
        //}

        ////здесь происходит сброс поворота камеры когда цель больше не указана, сначала проверяется больше ли углы поворота чем на 1 градус - если больше доворачивают камеру до состояния идентити через Lerp -так как Lerp не доводит поворот до ровного 0, то проверяется затем довелся ли поворот до значений меньше 1 градуса и тогда когда уже визуально не видно поворот без сглаживаний(Lerp) сбрасывается на 0 
        //if (!flag && (Mathf.Abs(cam.localRotation.eulerAngles.x) > 1 || Mathf.Abs(cam.localRotation.eulerAngles.y) > 1 || Mathf.Abs(cam.localRotation.eulerAngles.z) > 1))
        //{

        //    cam.localRotation = Quaternion.Lerp(cam.localRotation, Quaternion.identity, smoothing * Time.deltaTime);
        //}
        //else if (!flag && cam.localRotation != Quaternion.identity && (Mathf.Abs(cam.localRotation.eulerAngles.x) < 1 || Mathf.Abs(cam.localRotation.eulerAngles.y) < 1 || Mathf.Abs(cam.localRotation.eulerAngles.z) < 1))
        //{
        //    cam.localRotation = Quaternion.identity;
        //}

        //// при нажатии средней кнопки мыши происходит сброс всех целей а затем наведение на цель если она есть под указателем мыши
        //if (Input.GetMouseButtonDown(2) && !Input.GetMouseButton(1))
        //{
        //    deac();
        //    targetLook = false;
        //    xRot = Pod.rotation.x / Sens;
        //    yRot = Pod.rotation.y / Sens;
        //    //			transform.rotation = Quaternion.Lerp (transform.rotation,new Quaternion(transform.rotation.x,transform.rotation.y,station.rotation.z,transform.rotation.w),smoothing*Time.deltaTime);
        //    clickRay = cameraComp.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        //    if (Physics.Raycast(clickRay, out target, 1000000, ~(1 << 8)))
        //    {
        //        targetOb = target.transform.root.transform.gameObject;
        //        targetLook = true;
        //    }
        //}


        #endregion

        //при нажатии ПКМ вращение вокруг корабля и сброс наведения на цель
        if (Input.GetMouseButton(1))
        {


            MouseLook();

        }
        //здесь при вращении колесика мыши работает зум
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            MouseZoom();
        //после вычисления удаления камеры от корабля плавно перемещаем ее на новое место



        //		Pod.rotation = new Quaternion (Pod.rotation.x, Pod.rotation.y, 0,Pod.rotation.w);

        cam.localPosition = Vector3.Lerp(cam.localPosition, zoom, smoothing * Time.deltaTime);


    }

    //
    //		
    //	void Update ()
    //		{
    //здесь блок кода для наведения на цель
    //				#region Target look 
    //				//здесь проверяется включен ли флаг нацеливания и есть ли обьект на который нацеливаться и меняется цвет текста над обьектом на желтый, и выставляется флаг который означает следит ли в данный момент камера за кем то или нет
    //				if (targetLook && targetOb != null && !Input.GetMouseButton (2)) {

    //						MouseLookAtTarget (targetOb);
    //						flag = true;

    //						//если же нету обьектов для слежки и флаг того что за кем то камера следит положительный,значит надо деактивировать слежку за кем то и сбросить флаг обратно чтобы деактивация не происходила каждый кадр
    //				} else if (flag) {

    //						deac ();
    //						flag = false;
    //				}

    //				//здесь происходит сброс поворота камеры когда цель больше не указана, сначала проверяется больше ли углы поворота чем на 1 градус - если больше доворачивают камеру до состояния идентити через Lerp -так как Lerp не доводит поворот до ровного 0, то проверяется затем довелся ли поворот до значений меньше 1 градуса и тогда когда уже визуально не видно поворот без сглаживаний(Lerp) сбрасывается на 0 
    //				if (!flag && (Mathf.Abs (cam.localRotation.eulerAngles.x) > 1 || Mathf.Abs (cam.localRotation.eulerAngles.y) > 1 || Mathf.Abs (cam.localRotation.eulerAngles.z) > 1)) {

    //						cam.localRotation = Quaternion.Lerp (cam.localRotation, Quaternion.identity, smoothing * Time.deltaTime);
    //				} else if (!flag && cam.localRotation != Quaternion.identity && (Mathf.Abs (cam.localRotation.eulerAngles.x) < 1 || Mathf.Abs (cam.localRotation.eulerAngles.y) < 1 || Mathf.Abs (cam.localRotation.eulerAngles.z) < 1)) {
    //						cam.localRotation = Quaternion.identity;
    //				}

    //				// при нажатии средней кнопки мыши происходит сброс всех целей а затем наведение на цель если она есть под указателем мыши
    //				if (Input.GetMouseButtonDown (2) && !Input.GetMouseButton (1)) {
    //						deac ();
    //						targetLook = false;
    //						xRot = Pod.rotation.x / Sens;
    //						yRot = Pod.rotation.y / Sens;
    ////			transform.rotation = Quaternion.Lerp (transform.rotation,new Quaternion(transform.rotation.x,transform.rotation.y,station.rotation.z,transform.rotation.w),smoothing*Time.deltaTime);
    //						clickRay = cameraComp.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));

    //						if (Physics.Raycast (clickRay, out target, 1000000, ~(1 << 8))) {
    //								targetOb = target.transform.root.transform.gameObject;
    //								targetLook = true;
    //						}
    //				}


    //#endregion

    //при нажатии ПКМ вращение вокруг корабля и сброс наведения на цель
    //				if (Input.GetMouseButton (1)) {
    //
    //						


    //				}
    //здесь при вращении колесика мыши работает зум
    //				if (Input.GetAxis ("Mouse ScrollWheel") != 0)

    //		if(Input.touchCount>1)
    //		{
    //			if (!touches2) {
    ////				Debug.Log ("began");

    //				prevDistanceBetweenTouches = Vector2.Distance (Input.touches [0].position, Input.touches [1].position);
    //				speedOfZoom = 0f;
    //			}
    //			touches2 = true;
    //		}




    //		if (Input.touchCount == 2) {

    //			MouseZoom ();
    //		} else {
    //			touches2 = false;
    //			if(rotation) MouseLook ();
    //		}
    //				//после вычисления удаления камеры от корабля плавно перемещаем ее на новое место



    ////		Pod.rotation = new Quaternion (Pod.rotation.x, Pod.rotation.y, 0,Pod.rotation.w);

    //		cam.localPosition = Vector3.Lerp (cam.localPosition, new Vector3(cam.localPosition.x,cam.localPosition.y, zoom.z), smoothing * Time.deltaTime);


    //		}
    //	[SerializeField]
    //	bool touches2 = false;

}


