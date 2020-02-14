using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuickSortScript : MonoBehaviour
{
    public int NumberOfCubes = 4;

    public GameObject[] Cubes;
    GameObject pivot;
    GameObject leftCube;
    GameObject rightCube;
    int Cube0Val;
    int Cube1Val;
    int Cube2Val;
    int Cube3Val;
    int left;
    int right;
    //Text term;
    string correctMove;
    GameObject rightPointer;
    GameObject leftPointer;
    // Start is called before the first frame update
    void Start()
    {
        Cubes = new GameObject[NumberOfCubes];
        Cubes[0] = GameObject.Find("Cube0");
        
        Cubes[1] = GameObject.Find("Cube1");

       
        Cubes[2] = GameObject.Find("Cube2");

       
        Cubes[3] = GameObject.Find("Cube3");
        
        rightPointer = GameObject.Find("Right Pointer");
        leftPointer = GameObject.Find("Left Pointer");
        
        StartCoroutine(Sort(Cubes, 0, 1, Cubes.Length - 1));

    }

    void Update()
    {

    }
  
    IEnumerator Sort(GameObject[] Cubes, int pivot, int left, int right)
    {
        Debug.Log("Waiting for player to make move..");

        
        // if cubes are sorted needs work
      if (isSorted(Cubes))
        {
            Debug.Log("Cubes are sorted");
            // Canvas finish game

            yield break;
        }
        pivot = Cubes[pivot];
       
        //pivot.GetComponent<VRTK_InteractableObject>().enabled = true;
        Color pivotColor = Color.Lerp(Color.white, Color.green, 1f);
        pivot.GetComponent<Renderer>().material.color = pivotColor;

        leftCube = Cubes[left];
        rightCube = Cubes[right];
        Debug.Log(left);
        Debug.Log(right);

        Vector3 rightPos = rightCube.transform.position;
        float rightPosX = rightCube.transform.position.x;
        Vector3 leftPos = leftCube.transform.position;
        float leftPosX = leftCube.transform.position.x;
        float rightPointerPos = rightPointer.transform.localPosition.x;
        float leftPointerPos = leftPointer.transform.localPosition.x;
        // set positons of right and left pointer
        rightPointer.transform.position = new Vector3(rightPosX, rightPointer.transform.position.y, rightPointer.transform.position.z);
        leftPointer.transform.position = new Vector3(leftPosX, leftPointer.transform.position.y, leftPointer.transform.position.z);
        
        //left pointer
        Color leftColor = Color.Lerp(Color.white, Color.magenta, 1f);
        leftCube.GetComponent<Renderer>().material.color = leftColor;

        //right pointer
        Color rightColor = Color.Lerp(Color.white, Color.blue, 1f);
        rightCube.GetComponent<Renderer>().material.color = rightColor;
        int pVal = pivot.GetComponent<Value>().val;
        int rpVal = rightCube.GetComponent<Value>().val;
        int lpVal = leftCube.GetComponent<Value>().val;
        Debug.Log("pointer value =" + pVal);
        Debug.Log("left cube value =" + lpVal);
        Debug.Log("right cube value =" + rpVal);
        if (lpVal < pVal && rpVal < pVal)
        {
            correctMove = "move pivot cube";
        }
        else if(lpVal < pVal && rpVal > pVal)
        {
            correctMove = "shift both pointers";
        }
        else if (lpVal < pVal)
        {
            correctMove = "left shift";
        }
        
        else if (rpVal > pVal)
        {
            correctMove = "right shift";
        }
        else correctMove = "swap";

        Debug.Log("Correct move = " + correctMove);
      
        yield return new WaitUntil(() => !Mathf.Approximately(rightCube.transform.position.x, rightPosX) && 
        !Mathf.Approximately(leftCube.transform.position.x, leftPosX)
        /*|| rightPointer.transform.hasChanged || leftPointer.transform.hasChanged*/);
        
        // Checks if cubes were swapped
        if ((rightCube.transform.position.x >= leftPosX - 0.1 
            || rightCube.transform.position.x <= leftPosX + 0.1) && 
           (leftCube.transform.position.x >= rightPosX - 0.1
            || leftCube.transform.position.x <= rightPosX + 0.1))
        {
            if (correctMove.Equals("swap"))
            {
                Debug.Log("Correct move made");

                //swap cubes
                GameObject temp = Cubes[right];
                Cubes[right] = Cubes[left];
                Cubes[left] = temp;
                StartCoroutine(Sort(Cubes, left, right));
            }
            else
            {
                Debug.Log("Incorrect move made");
                LeanTween.moveLocalX(leftCube,
                    leftPosX, 1);
                StartCoroutine(Sort(Cubes, left, right));
            }
        }
        else if(correctMove.Equals("move pivot cube"))
        {
            moveElem(Cubes, pivot, right + 1);
            StartCoroutine(Sort(Cubes, 0, 1, left));
            StartCoroutine(Sort(Cubes, 0, 1, left));

            StartCoroutine(Sort(Cubes, left + 1, Cubes.Length-1));
            //move pivot cube and partition
            
        }

        else if (leftPointer.transform.hasChanged && correctMove.Equals("left shift"))
        {
            left += 1;
            StartCoroutine(Sort(Cubes, left, right));
        }
        else if (rightPointer.transform.hasChanged && correctMove.Equals("right shift"))
        {
            right -= 1;
            StartCoroutine(Sort(Cubes, left, right));
        }
    }

    bool isSorted(GameObject[] Cubes) {
        
        for(int i = 0; i < Cubes.Length - 1; i++)
        {
            if(Cubes[i].GetComponent<Value>().val > Cubes[i + 1].GetComponent<Value>().val)
            {
                return false;
            }
        }
        return true;
    }

    //GameObject[] moveElem(GameObject[] Cubes, int oldIndex, int newIndex)
    //{
       // GameObject elem = Cubes[oldIndex];
       // for(int i = oldIndex+1; i < newIndex; i++)
      //  {
       /*     Cubes[i - 1] = Cubes[i];
        }
        Cubes[newIndex] = elem;
    }*/

   /* public void setPosX(, float x)
    {

    }*/
  
    
    /*void QuickSort(GameObject[] cubes, int left, int right)
    {
        
        if (left < right)
        {
            
            Partition(cubes, left, right);
           
            if (pivot > 1)
            {
                
                QuickSort(cubes, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                
                QuickSort(cubes, pivot + 1, right);
            }
        }

    }

    private void Partition(GameObject[] cubes, int left, int right)
    {
        //start partition
        GameObject pivotObj = cubes[left];
        Vector3 tempPosition;
        while (true)
        {

            while (cubes[left].transform.localScale.y < pivotObj.transform.localScale.y)
            {
                left++;
            }

            while (cubes[right].transform.localScale.y > pivotObj.transform.localScale.y)
            {
                right--;
            }

            if (left < right)
            {
                if (cubes[left].transform.localScale.y == cubes[right].transform.localScale.y)
                {
                    pivot = right;
                    break;
                }

                GameObject temp = cubes[left];
                cubes[left] = cubes[right];
                cubes[right] = temp;

                tempPosition = cubes[left].transform.localPosition;

                cubes[left].transform.localPosition =
                    new Vector3(cubes[right].transform.localPosition.x,
                    tempPosition.y, tempPosition.z);

                cubes[right].transform.localPosition =
                    new Vector3(tempPosition.x,
                    cubes[left].transform.localPosition.y,
                    cubes[left].transform.localPosition.z);


            }
            else
            {
                pivot = right;
                break;
            }
        }

        //end partition
    } 
    */
  /*  void initializeRandom()
    {
        Cubes = new GameObject[NumberOfCubes];
        for(int i = 0; i < NumberOfCubes; i++)
        {
            int randomNumber = Random.Range(1, CubeHeightMax + 1);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(0.9f, randomNumber, 1);
            cube.transform.position = new Vector3(2*i, randomNumber/2.0f + 4, 0);

            cube.transform.parent = this.transform;

            Cubes[i] = cube;
        }

        transform.position = new Vector3(-NumberOfCubes / 2f, -CubeHeightMax / 2.0f + 6, 0);

    }*/
    // Update is called once per frame
   
}
