using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Est.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(SetDestinationCharacter))]
    public class AICharacter : MonoBehaviour
    {
        public bool completeServiceActualGo = false;
        [SerializeField] bool workWithDesactiveObject = false;

        [Header("Other ")]
        [SerializeField] TypeObjectPooling typeObjectPoolingINeed;
        [SerializeField] GameObject gameObjectMeshRenderer = null;
        [SerializeField] Animator animatorCharacter = null;

        [SerializeField] float timeInService = 0;
        [SerializeField] float pathEndThreshold = 0.1f;

        [Header("View")]
        [SerializeField] AudioSource audioSource;

        Vector3 spawnToReturnAfterService = Vector3.zero;
        bool completedService = false;

        NavMeshAgent navMeshAgent;
        SetDestinationCharacter setDestinationCharacter;
        ObjectPooling objectPooling;

        NavMeshPath path;

        public bool CanSetDestinationInCharacterAI = true;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            setDestinationCharacter = GetComponent<SetDestinationCharacter>();

            objectPooling = ControlPoolingTypeInGame.controlPoolingType.GetTypePooling(typeObjectPoolingINeed);

            MoveCharacter();

            path = navMeshAgent.path;
        }

        private void OnEnable()
        {
            if (setDestinationCharacter != null) {
                MoveCharacter();
            }
        }

        private void Update()
        {
            if (navMeshAgent.hasPath)
            {

                //if (typeObjectPoolingINeed == TypeObjectPooling.Car) print("Has path");
                if (!completeServiceActualGo)
                {
                    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + pathEndThreshold && !completedService)
                    {
                        if (workWithDesactiveObject)
                            gameObjectMeshRenderer.SetActive(false);
                        else
                            updateCharacterVoxelToAnimationService();

                        if (typeObjectPoolingINeed == TypeObjectPooling.Car) print("This car no complete service");
                        completedService = true;

                        StartCoroutine(timeCharacterInService());

                    }

                }//after terminated service
                else if (Vector3.Distance(spawnToReturnAfterService, transform.position) <= navMeshAgent.stoppingDistance + pathEndThreshold)
                {
                    //OP
                    objectPooling.TheGoalOfTheGameObjectEnd(gameObject);


                    if (typeObjectPoolingINeed == TypeObjectPooling.Car) print("This car");
                    completedService = false;

                    //GO
                    completeServiceActualGo = false;
                    gameObject.SetActive(false);
                }
            }
            //else if (typeObjectPoolingINeed == TypeObjectPooling.Car) print("I have no path");

            animationControlVelocity();

        }

        SetDestinationData destinationData;

        private void MoveCharacter() {

            if (!CanSetDestinationInCharacterAI) return;

            //new data
            destinationData = setDestinationCharacter.GetDestinationCharacterAI();
            spawnToReturnAfterService = destinationData.directionAICharacter;

            //navmesh
            if (navMeshAgent.enabled) navMeshAgent.SetDestination(spawnToReturnAfterService);
            else print("You not have navmeshAgent");
        }

        private IEnumerator timeCharacterInService() {
            yield return new WaitForSeconds(timeInService);

            if (workWithDesactiveObject)
                gameObjectMeshRenderer.SetActive(true);
            else
            {
                animatorCharacter.SetTrigger("Run");
                updateCharacterVoxelToAnimationAfterService();
            }

            //sound
            if(audioSource != null) audioSource.Play();

            //return to spawn
            completeServiceActualGo = true;
            MoveCharacter();
        }

        private void updateCharacterVoxelToAnimationService() {
            gameObjectMeshRenderer.transform.parent = null;
            gameObjectMeshRenderer.transform.position = destinationData.positionSlotAnimationWorkCorrectly;
            gameObjectMeshRenderer.transform.rotation = Quaternion.Euler( destinationData.rotationSlotAnimationWorkCorrectly);
            animatorCharacter.Play(destinationData.animationTextInService); 
        }

        private void updateCharacterVoxelToAnimationAfterService()
        {
            gameObjectMeshRenderer.transform.parent = gameObject.transform;
            gameObjectMeshRenderer.transform.localPosition = Vector3.zero;
            gameObjectMeshRenderer.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        private void animationControlVelocity() {
            if(animatorCharacter != null) animatorCharacter.SetFloat("forwardSpeed", navMeshAgent.speed);
        }
    }
}
