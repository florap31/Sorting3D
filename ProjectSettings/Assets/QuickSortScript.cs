using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortScript : MonoBehaviour
{
    public int NumberOfCubes = 4;

    public GameObject[] Cubes;
    GameObject pivot;
    GameObject leftPointer;
    GameObject rightPointer;
    int Cube0Val;
    int Cube1Val;
    int Cube2Val;
    int Cube3Val;
    int left;
    int right;
    string correctMove;
  
    // Start is called before the first frame update
    void Start()
    {
        Cubes = new GameObject[NumberOfCubes];
        Cubes[0] = GameObject.Find("Cube0");
        Debug.Log(Cubes[0].GetComponent<Value>().val);

        Cubes[1] = GameObject.Find("Cube1");
       

        Cubes[2] = GameObject.Find("Cube2");
        

        Cubes[3] = GameObject.Find("Cube3");
        

        setRight(Cubes.Length - 1);
        setLeft(0);
        Sort(Cubes);

        Debug.Log("sort");

    }

    int getLeft()
    {
        return left;
    }
    void setLeft(int num)
    {
        left = num;
    }
    int getRight()
    {
        return right;
    }
    void setRight(int num)
    {
        right = num;
    }

    bool LPClicked = false;
    bool RPClicked = false;
    IEnumerator Sort(GameObject[] Cubes)
    {
        if(Cubes.Length <= 1)
        {
            //Canvas finish game
            
        }
        pivot = Cubes[getLeft()];
       
        //pivot.GetComponent<VRTK_InteractableObject>().enabled = true;
        Color pivotColor = Color.Lerp(Color.white, Color.green, 1f);
        pivot.GetComponent<Renderer>().material.color = pivotColor;

        leftPointer = Cubes[getLeft() + 1];
        rightPointer = Cubes[getRight()];
        Debug.Log(getRight());

        Vector3 rightPos = rightPointer.transform.position;

        Vector3 leftPos = leftPointer.transform.position;

        //left pointer
        Color leftColor = Color.Lerp(Color.white, Color.magenta, 1f);
        leftPointer.GetComponent<Renderer>().material.color = leftColor;

        //right pointer
        Color rightColor = Color.Lerp(Color.white, Color.blue, 1f);
        rightPointer.GetComponent<Renderer>().material.color = rightColor;
        int pVal = pivot.GetComponent<Value>().val;
        int rpVal = rightPointer.GetComponent<Value3>().val;
        int lpVal = leftPointer.GetComponent<Value1>().val;
        if (lpVal < pVal)
        {
            correctMove = "left shift";
        }

        else if (rpVal > pVal)
        {
            correctMove = "right shift";
        }
        else correctMove = "swap";

        Debug.Log("Waiting for player to make move..");
        yield return new WaitUntil(() => rightPointer.transform.hasChanged && leftPointer.transform.hasChanged || LPClicked || RPClicked);
        
        if (rightPointer.transform.position == leftPos && leftPointer.transform.position == rightPos)
        {
            if (correctMove == "swap")
            {
                setLeft(getLeft() + 1);
                setRight(getRight() - 1);
            }
            else
            {
                //reset positions
            }
        }

        else if (LPClicked && correctMove.Equals("left shift"))
        {
            setLeft(getLeft() + 1);
        }
        else if (RPClicked && correctMove.Equals("right shift"))
        {
            setRight(getRight() - 1);
        }


    }


    
  
    
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
    void Update()
    {
        
    }
}
