﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Pathfinding2D : MonoBehaviour
{
    public Transform seeker;
    private Grid2D grid;
    private Node2D seekerNode, targetNode;
    public List<Node2D> myPath;

    private void Start()
    {
        FindGrid();
    }

    private void FindGrid() {
        if (grid != null) return;
        GameObject GridOwner = GameObject.FindGameObjectWithTag("GridPathfinding");
        grid = GridOwner.GetComponent<Grid2D>();
    }

    public bool FindPath(Vector3 startPos, Vector3 targetPos)
    {
        FindGrid();

        //get player and target position in grid coords
        seekerNode = grid.NodeFromWorldPoint(startPos);
        targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node2D> openSet = new List<Node2D>();
        HashSet<Node2D> closedSet = new HashSet<Node2D>();
        openSet.Add(seekerNode);
        
        //calculates path for pathfinding
        while (openSet.Count > 0)
        {
            //iterates through openSet and finds lowest FCost
            Node2D node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost <= node.FCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            //If target found, retrace path
            if (node == targetNode)
            {
                RetracePath(seekerNode, targetNode);
                return true;
            }
            
            //adds neighbor nodes to openSet
            foreach (Node2D neighbour in grid.GetNeighbors(node))
            {
                if (neighbour.obstacle || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        //Path not found
        return false;
    }

    //reverses calculated path so first node is closest to seeker
    private void RetracePath(Node2D startNode, Node2D endNode)
    {
        List<Node2D> newPath = new List<Node2D>();
        Node2D currentNode = endNode;

        while (currentNode != startNode)
        {
            newPath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        newPath.Reverse();

        myPath = newPath;
    }


    //gets distance between 2 nodes for calculating cost
    private int GetDistance(Node2D nodeA, Node2D nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}