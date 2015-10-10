using UnityEngine;

public class CircularQueue
{
    private int head;
    private int tail;
    private int elementCount;
    private int elementSize;

    struct Element
    {
        public byte[] data;
        public int length;
    }

    private Element[] elements;

    public CircularQueue(int _elementCount, int _elementSize)
    {
        head = 0;
        tail = 0;
        elementCount = _elementCount;
        elementSize = _elementSize;

        elements = new Element[_elementCount];
        for (int i=0; i<_elementCount; ++i)
        {
            elements[i].data = new byte[_elementSize];
        }
    }

    public bool Enqueue(byte[] element, int usedSize)
    {
        int next = (tail + 1) % elementCount;
        if (next == head)
        {
            Debug.LogError("[CircleQueue.Enqueue] is full.");
            return false;
        }

        elements[tail].length = usedSize;
        element.CopyTo(elements[tail].data, 0);

        tail = next;
        
        return true;
    }

    public bool Dequeue(byte[] element, ref int usedSize)
    {
        if (head == tail)
        {
            return false;
        }

        usedSize = elements[head].length;
        elements[head].data.CopyTo(element, 0);

        head = (++head) % elementCount;

        return true;
    }

    public void Clear()
    {
        head = 0;
        tail = 0;

        Debug.LogWarning("[CircleQueue] cleared.");
    }

    public int Total()
    {
        return elementCount;
    }

    public int Count()
    {
        int count = tail - head;
        return count < 0 ? (elementCount + count) : count;
    }
}
