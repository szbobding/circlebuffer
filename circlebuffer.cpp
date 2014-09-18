#include <stdio.h>
#include <stdlib.h>
#include <memory.h>

template<typename T> class circlebuffer
{
    int head;
    int tail;
    int size;
    T* data;
    
public:
    circlebuffer(int capacity) : head(0), tail(0), size(capacity)
    {
        data = new T[size];
    }
    
    ~circlebuffer()
    {
        delete[] data;
    }
    
    //  0: success
    // -1: buffer is full
    int push(T* elem)
    {
        int next = (tail + 1) % size;
        if (next == head)
        {
            return -1;
        }

        memcpy(&data[tail], elem, sizeof(T));

        tail = next;

        return 0;
    }

    //  0: success
    // -1: buffer is empty
    int pop(T* elem)
    {
        if (head == tail)
        {
            return -1;
        }
        
        memcpy(elem, &data[head], sizeof(T));

        head = (head + 1) % size;
        
        return 0;
    }
};

int main()
{
    circlebuffer<int>* buffer = new circlebuffer<int>(4);
    int elem = 0;
    int ret = buffer->pop(&elem);
    printf("pop : %d, elem: %d\n", ret, elem);

    int val = 10;
    ret = buffer->push(&val);
    printf("push: %d, elem: %d\n", ret, elem);

    ret = buffer->pop(&elem);
    printf("pop : %d, elem: %d\n", ret, elem);

    delete buffer;

    return 0;
}
