using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>{
    // A few notes on this Heap:
    // The formula to calculate the index of the parent of an item in the array items:
    // (index - 1) / 2
    // The formula to calculate the indexes of the children of an item in the array:
    // index * 2 + 1 (left) and index * 2 + 2 (right)

    T[] items; // Array with items
    int itemCount; // Variable to store how many items this Heap currently contains

    // constructor
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public int Count
    {
        get
        {
            return itemCount;
        }
    }

    // Method to add new items to this Heap
    public void Add(T item)
    {
        item.HeapIndex = itemCount;
        items[itemCount] = item;
        itemCount++;
        SortUp(item);
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        itemCount--;
        items[0] = items[itemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);

        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    void SortDown(T item)
    {
        while (true)
        {
            int leftChildIndex = item.HeapIndex * 2 + 1;
            int rightChildIndex = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (leftChildIndex < itemCount) // If item has a left child
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < itemCount) // If item has a Right child
                {
                    if (items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0) // If the right child has a higher priority
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0) // If the parent has a lower priority than its child with the highest priority
                {
                    Swap(item, items[swapIndex]);
                }
                else // If the parent has a higher priority than both its children, it is in the correct position
                {
                    return;
                }
            }
            else // If the item has no children, it is in the correct position
            {
                return;
            }
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            if(item.CompareTo(items[parentIndex]) > 0)
            {
                Swap(item, items[parentIndex]);
                // After the swapping, which item is the new parent
                parentIndex = (item.HeapIndex - 1) / 2;
            }
            else
            {
                return;
            }
        }
    }

    void Swap(T itemA, T itemB)
    {
        // First the indexes stored in the items have to be swapped:
        int tmp = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = tmp;

        // Then the actual swapping in the array items:
        items[itemA.HeapIndex] = itemA;
        items[itemB.HeapIndex] = itemB;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
