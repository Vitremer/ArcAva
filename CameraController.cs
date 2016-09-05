using UnityEngine;
using System.Collections;



public class CameraController : MonoBehaviour
{



    public float camStop = 0.1f;
    Collider camCollider;
    public bool rotation = true;
    public Texture2D cursorText;
    public int xDelayCursor = 0;
    public int yDelayCursor = 0;
    [Range(1f, 10f)]
    public float
            smoothing = 5f;
    [Range(0.05f, 10f)]
    public float
                Sens = 0.5f;

    bool underground = false;
    float yPosLerp;
    float upwardCamPos;
    float initialYCamPos;

    public float ZoomMax = 300f, ZoomMin = 10f;
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
    float zoomCorrection = 0f;
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


    public Transform carrot;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        Cursor.SetCursor(cursorText, new Vector2(xDelayCursor, yDelayCursor), CursorMode.Auto);
        K = 80 / (MaxSpeedOfDistortion - 2.6f);
        Pod = transform.parent.transform;
        cam = transform;
        lastpos = Pod.position;
        locPos = cam.localPosition;

        cameraComp = GetComponent<Camera>();
        upwardCamPos = cam.localPosition.y;
        initialYCamPos = upwardCamPos;

        zoom.Set(0, upwardCamPos, cam.localPosition.z);
        prevRotation = Pod.rotation;

        camCollider = GetComponent<SphereCollider>();



    }
    float addX, addY;
    Quaternion prevRotation;


    void MouseLook()//метод следования камеры за кораблем
    {
        if (underground)
        {

            if (Input.GetAxis("Mouse Y") < 0) yRot -= Input.GetAxis("Mouse Y");
        }

        else
        {

            yRot -= Input.GetAxis("Mouse Y");

        }



        xRot += Input.GetAxis("Mouse X");


        Pod.localRotation = Quaternion.Lerp(Pod.localRotation, Quaternion.Euler(yRot * Sens, xRot * Sens, 0), smoothing * Time.deltaTime);

        //carrot.RotateAround(Pod.position, Pod.up, xRot * Sens);
        //carrot.RotateAround(Pod.position, Pod.right, yRot * Sens);



    }


    [SerializeField]
    float prevDistanceBetweenTouches = 0f;
    [SerializeField]
    float speedOfZoom;


    void MouseZoom()
    {


        zZoom = Mathf.Clamp((zZoom + Input.GetAxis("Mouse ScrollWheel") * Sens * 10), -ZoomMax, -ZoomMin);


    }

    float groundCurrentY = 0f;

    void CheckUnderground()
    {


        RaycastHit hitUp = new RaycastHit();

        Ray rayUp = new Ray(carrot.position + Vector3.down * (camStop + (groundCurrentY - initialYCamPos)), Vector3.up);
        Debug.DrawRay(carrot.position + Vector3.down * (camStop+(groundCurrentY-initialYCamPos)), Vector3.up, Color.red);
        if (Physics.Raycast(rayUp, out hitUp, 10000, (1 << 8)))
        {
            groundCurrentY = Pod.InverseTransformPoint(hitUp.point).y + camStop;
            upwardCamPos = Pod.InverseTransformPoint(hitUp.point).y + camStop;
            underground = true;

         
        }
        else
        {
            upwardCamPos = initialYCamPos;
            underground = false;
        }
            //RaycastHit hitUp = new RaycastHit();

            //Ray rayUp = new Ray(carrot.position + Vector3.down.normalized * (camStop - 0.02f), Vector3.up);
            //Debug.DrawRay(carrot.position + Vector3.down.normalized * camStop, Vector3.up, Color.red);
            //if (Physics.Raycast(rayUp, out hitUp, 10000, (1 << 8)))
            //{
            //    Debug.Log("Undergorund");
            //    //upwardCamPos = Pod.InverseTransformPoint(hitUp.point).y;
            //    groundCurrentY = Pod.InverseTransformPoint(hitUp.point).y + camStop;
            //    underground = true;
            //}
            //else
            //{
            //    //groundCurrentY = initialYCamPos;
            //    //upwardCamPos = initialYCamPos;
            //    underground = false;
            //}


        }

    void Update()
    {


        cam.localPosition = Vector3.Lerp(cam.localPosition, carrot.localPosition, smoothing * Time.deltaTime);
        //cam.localPosition = carrot.localPosition;

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();




        CheckUnderground();

        //при нажатии ПКМ вращение вокруг цели
        if (Input.GetMouseButton(1))
        {


            MouseLook();

        }

        //здесь при вращении колесика мыши работает зум

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            MouseZoom();
        //после вычисления удаления камеры от корабля плавно перемещаем ее на новое место



        zoom.Set(0, upwardCamPos, zZoom);
        cam.LookAt(Pod.transform.position);

        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(Pod.position, transform.position - Pod.position);

        if (Physics.Raycast(ray, out hit, (transform.position - Pod.position).magnitude + 5f, (1 << 10)))

        {

            carrot.localPosition = Vector3.Lerp(carrot.localPosition, Pod.InverseTransformPoint(hit.point), smoothing * Time.deltaTime);

        }
        else
        {
            //carrot.localPosition = new Vector3(carrot.localPosition.x, Mathf.Max(carrot.localPosition.y, Pod.InverseTransformPoint(hitUp.point).y + camStop), carrot.localPosition.z);

            //carrot.localPosition = Vector3.Lerp(carrot.localPosition, Pod.InverseTransformPoint(hitUp.point), smoothing * Time.deltaTime);

            if (underground && carrot.localPosition.y < groundCurrentY)
            {
                carrot.localPosition = new Vector3(carrot.localPosition.x, groundCurrentY, zZoom-zoomCorrection);
            }
            else
            carrot.localPosition =zoom;
            
        }

    
    }

    /*если палка от камеры вниз ниже земли, так как луч стреляет от конца палки вверх, тогда камера должна оставаться на уровне земля плюс длина палки, 
     * 
     * 
     */
}


