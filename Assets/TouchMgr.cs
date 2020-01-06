using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class TouchMgr : MonoBehaviour {


    private Camera ARCamera;        // ARCore 카메라
    public GameObject placeObject;  // 터치 시 평면에 생성할 프리팹
    

    public Text measureLabel;       // 거리를 출력할 텍스트
    private Anchor prevAnchor;      // 이전 위치를 저장
   
	// Use this for initialization
	void Start () {

        

        // ARCore Device 프리팹 하위에 있는 카메라를 찾아서 변수에 할당
        ARCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 0) return;

        // 첫 번째 터치 정보 추출
        Touch touch = Input.GetTouch(0);
        
        // ARCore에서 제공하는 RaycastHit과 유사한 구조체
        TrackableHit hit;

        // 검출 대상을 평면 또는 Feature Point로 한정
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;
        
        // 터치한 지점으로 레이 발사
        if(touch.phase == TouchPhase.Began && Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // 객체를 고정할 앵커를 생성
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                                   
            // 이전 앵커 확인
            if(prevAnchor == null)
            {
                prevAnchor = anchor;
            }

            
            // 두 앵커간 거리 계산
            float dist = Vector3.Distance(prevAnchor.transform.position, anchor.transform.position);
            measureLabel.text = dist.ToString("#0.00m");
            prevAnchor = anchor;
            

            // 객체를 생성
            GameObject obj = Instantiate(placeObject, hit.Pose.position, Quaternion.identity, anchor.transform);

            // 생성한 객체가 사용자쪽으로 바라보도록 회전값 계산
            var rot = Quaternion.LookRotation(ARCamera.transform.position - hit.Pose.position);

            // 사용자 쪽 회전값 적용
            obj.transform.rotation = Quaternion.Euler(ARCamera.transform.position.x, rot.eulerAngles.y, ARCamera.transform.position.z);


            /*
            // Create the Anchor.
            Anchor anchor1 = hitResult.createAnchor();
            AnchorNode anchorNode = new AnchorNode(anchor);
            anchorNode.setParent(arFragment.getArSceneView().getScene());

            // Create the transformable andy and add it to the anchor.
            TransformableNode node = new TransformableNode(arFragment.getTransformationSystem());
            // Maxscale must be greater than minscale
            node.getScaleController().setMaxScale(0.02f);
            node.getScaleController().setMinScale(0.01f);

            node.setParent(anchorNode);
            node.setRenderable(renderable);
            node.select();
            */


        }
	}


    /*
    public void OnClickChange()
    {
        objNum++;

        objNum = objNum % 4;

        switch(objNum)
        {
            case 0:
                placeObject = placeObject1;
                break;
            case 1:
                placeObject = placeObject2;
                break;
            case 2:
                placeObject = placeObject3;
                break;
            case 3:
                placeObject = placeObject4;
                break;
        }
        

    }*/
}
