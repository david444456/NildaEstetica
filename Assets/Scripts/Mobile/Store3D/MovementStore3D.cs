using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectStore3D))]
public class MovementStore3D : MonoBehaviour
{
    [SerializeField] Vector3 directioMovementVector = Vector3.zero;
    [SerializeField] GameObject GOcameraInStore = null;
    [SerializeField] float movementPeriod = 0.2f;
    [SerializeField] float maxTimeToChangePosition = 1.3f;

    #region private var

    private bool m_ITerminatedAnimationMovement = true;
    private float m_timeToChangeAnimation = 0;
    private int m_actualPositionInArratGO = 0;
    private int m_maxCountGameObjects = 0;

    Vector3 pos = Vector3.zero;
    ItemInfo3D[] gameObjectInStore;

    #endregion

    private void Start()
    {
        ObjectStore3D controlObjectStore3D = GetComponent<ObjectStore3D>();
        m_maxCountGameObjects = controlObjectStore3D.GetMaxCountOfGameObjectsInStore();
        gameObjectInStore = new ItemInfo3D[controlObjectStore3D.GetMaxCountOfGameObjectsInStore()];
        gameObjectInStore = controlObjectStore3D.GetObjectsItems();
    }

    public void StartAnimationMovementWithButton(int actualDirection)
    {
        if (!m_ITerminatedAnimationMovement) return;
        if (actualDirection == -1)
        {
            if (m_actualPositionInArratGO > 0)
            {
                ChangePosAndCallACoroutine(-1);
            }
        }
        else if (actualDirection >= 1)
        {
            if (m_actualPositionInArratGO < m_maxCountGameObjects - 1)
            {
                ChangePosAndCallACoroutine(+1);
            }
        }
    }

    public int GetActualIndexPosition() => m_actualPositionInArratGO;

    private void ChangePosAndCallACoroutine(int directionIndex)
    {
        m_actualPositionInArratGO = m_actualPositionInArratGO + directionIndex;
        pos = new Vector3(gameObjectInStore[m_actualPositionInArratGO].transform.localPosition.x, GOcameraInStore.transform.localPosition.y, GOcameraInStore.transform.localPosition.z);
        StartCoroutine(StartAnimationTranformPosition());
    }

    IEnumerator StartAnimationTranformPosition( )
    {
        //for errors, the user cant press movement button when store move
        m_ITerminatedAnimationMovement = false;

        //movement
        while (m_timeToChangeAnimation < maxTimeToChangePosition)
        {
            m_timeToChangeAnimation += Time.deltaTime / movementPeriod;
            GOcameraInStore.transform.localPosition = Vector3.Lerp(GOcameraInStore.transform.localPosition, pos, movementPeriod);

            yield return null;
        }

        //solution error in mobile, not set exactly position
        GOcameraInStore.transform.localPosition = pos;

        //change var
        m_timeToChangeAnimation = 0;
        m_ITerminatedAnimationMovement = true;
    }
}
