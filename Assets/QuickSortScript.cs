﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuickSortScript : MonoBehaviour
{
    public int NumberOfCubes = 4;

    public GameObject[] Cubes;
    GameObject pivotCube;
    GameObject leftCube;
    GameObject rightCube;
    int leftVal;
    int rightVal;
    int pivotVal;
    //Text term;
    string correctMove;
    GameObject rightPointer;
    GameObject leftPointer;
    public float leftPosX { get; set; }
    public float rightPosX { get; set; }
    public int pivot { get; set; }
    

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

    public IEnumerator Sort(GameObject[] Cubes, int pivot, int left, int right)
    {
        leftVal = left;
        rightVal = right;
        pivotVal = pivot;
        Debug.Log("Waiting for player to make move..");

        // if cubes are sorted needs work
        if (isSorted(Cubes))
        {
            Debug.Log("Cubes are sorted");
            // Canvas finish game
            // https://medium.com/re-write/making-triggered-text-appear-in-vr-an-adventure-8abf896d06a6

            yield break;
        }
        Debug.Log("index values = " +pivot + " " + left + " " + right);
        pivotCube = Cubes[pivot];
        leftCube = Cubes[left];
        rightCube = Cubes[right];

        // allow objects to be interactable, test
        //pivotCube.GetComponent<VRTK_InteractableObject>().enabled = true;
        Color pivotCubeColor = Color.Lerp(Color.white, Color.green, 1f);
        Color leftCubeColor = Color.Lerp(Color.white, Color.magenta, 1f);
        Color rightCubeColor = Color.Lerp(Color.white, Color.blue, 1f);
        pivotCube.GetComponent<Renderer>().material.color = pivotCubeColor;
        leftCube.GetComponent<Renderer>().material.color = leftCubeColor;
        rightCube.GetComponent<Renderer>().material.color = rightCubeColor;

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

        int pVal = pivotCube.GetComponent<Value>().val;
        int rpVal = rightCube.GetComponent<Value>().val;
        int lpVal = leftCube.GetComponent<Value>().val;
        Debug.Log("cube values = " + pVal + " " + lpVal + " " + rpVal);
        
        if (lpVal < pVal && rpVal < pVal)
        {
            correctMove = "move pivotCube cube";
        }
        else if (lpVal < pVal && rpVal > pVal)
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

        // change to when user has clicked "continue" button on canvas to move on

    }

    public bool isSorted(GameObject[] Cubes)
    {

        for (int i = 0; i < Cubes.Length - 2; i++)
        {
         
            if (Cubes[i].GetComponent<Value>().val > Cubes[i + 1].GetComponent<Value>().val)
            {
                return false;
            }
        }
        return true;
    }

    public void moveElem(GameObject[] Cubes, int index1, int index2)
    {
        GameObject temp = Cubes[index1];
        Cubes[index1] = Cubes[index2];
        Cubes[index2] = temp;
    }

    public void continueGame()
    {
        Debug.Log("Continuing");
        // Checks if cubes were swapped
        if (Mathf.Approximately(rightCube.transform.position.x, leftPosX ) &&
           Mathf.Approximately(leftCube.transform.position.x, rightPosX)
            && correctMove.Equals("swap"))
        {
            Debug.Log("Correct move made");

            //swap cubes
            moveElem(Cubes, leftVal, rightVal);
            StartCoroutine(Sort(Cubes, pivotVal, leftVal + 1, rightVal - 1));
        }
        // if user clickes "move pivotCube" button
        else if (correctMove.Equals("move pivotCube cube"))
        {
            // physically move pivotCube
            //LeanTween.moveLocalX(pivotCube,
            //       rightCube, 1);
            // move values in array
            moveElem(Cubes, pivotVal, rightVal);
            // partition left
            StartCoroutine(Sort(Cubes, 0, 1, leftVal));

            //partition right
            StartCoroutine(Sort(Cubes, rightVal + 1, rightVal + 2, Cubes.Length - 1));
        }
        else if (leftPointer.transform.hasChanged &&
        rightPointer.transform.hasChanged && correctMove.Equals("shift both pointers"))
        {
            StartCoroutine(Sort(Cubes, pivotVal, leftVal + 1, rightVal - 1));
        }

        else if (leftPointer.transform.hasChanged && correctMove.Equals("left shift"))
        {

            StartCoroutine(Sort(Cubes, pivot, leftVal + 1, rightVal));
        }
        else if (rightPointer.transform.hasChanged && correctMove.Equals("right shift"))
        {
            StartCoroutine(Sort(Cubes, pivotVal, leftVal, rightVal - 1));
        }
        else
        {
            Debug.Log("Incorrect move made");
            //LeanTween.moveLocalX(leftCube,
            //    leftPosX, 1);
            StartCoroutine(Sort(Cubes, pivotVal, leftVal, rightVal));
        }
    }


}
