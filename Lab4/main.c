#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

enum Constants
{
    INPUT_SIZE = 100
};

void SwapInt(int* a, int* b)
{
    int box;
    box = *b;
    *b = *a;
    *a = box;
}

int CycleInputInt(char* stringToOutput, bool(* pChecker)(int))
{
    int number;
    int position;
    char input[INPUT_SIZE];

    while (true)
    {
        printf("%s\n", stringToOutput);
        fflush(stdout);
        char* fgetsRet = fgets(input, INPUT_SIZE, stdin);
        if (fgetsRet == NULL)
        {
            printf("Wrong format!\n");
            continue;
        }
        int inputLength = strlen(input) - 1;
        input[inputLength] = '\0';
        int sscanfRet = sscanf(input, "%d%n", &number, &position);
        if (position != inputLength)
        {
            printf("Wrong format!\n");
            continue;
        }
        if (pChecker && !pChecker(number))
        {
            printf("Wrong format!\n");
            continue;
        }
        if (sscanfRet == 1) break;
        printf("Wrong format!\n");
    }
    return number;
}

bool ArraySizeInputChecker(int arraySize)
{
    return arraySize > 0;
}

bool AnyIntInputChecker(int _)
{
    return true;
}

typedef struct
{
    int* content;
    int size;
} DynArray;

void PrintArray(DynArray* object)
{
    for (int i = 0; i < object->size; i++)
    {
        printf("%d ", object->content[i]);
    }
    printf("\n");
}

void ArrayCopy(DynArray* origin, DynArray* object)
{
    free(object->content);
    object->size = origin->size;
    object->content = (int*) malloc(object->size * sizeof(int));
    for (int i = 0; i < object->size; i++)
    {
        object->content[i] = origin->content[i];
    }
}

void ShellSort (DynArray* object)
{
    int h, i, j, tmp;

    // Выбор шага
    for (h = object->size / 3; h > 0; h /= 2)
        // Перечисление элементов, которые сортируются на определённом шаге
        for (i = h; i < object->size; i++)
            // Перестановка элементов внутри подсписка, пока i-тый не будет отсортирован
            for (j = i - h; j >= 0 && object->content[j] > object->content[j + h]; j -= h)
            {
                SwapInt(&object->content[j], &object->content[j+h]);
            }
}

void InsertionSort(DynArray* object)
{
    for (int i = 0; i < object->size; i++)
    {
        int box;
        box = object->content[i];
        int j;
        j = 0;
        while (object->content[j] < box)
        {
            j++;
        }
        for (int k = i - 1; k >= j; k--)
        {
            object->content[k + 1] = object->content[k];
        }
        object->content[j] = box;
    }
}

void SelectionSort(DynArray* object)
{
    for (int i = 0; i < object->size; i++)
    {
        int jBox;
        jBox = i;
        int box;
        box = object->content[i];
        for (int j = i; j < object->size; j++)
        {
            if (object->content[j] < box)
            {
                jBox = j;
                box = object->content[j];
            }
        }
        SwapInt(&(object->content[i]), &(object->content[jBox]));
    }
}

int main()
{
    DynArray object;
    object.size = CycleInputInt("Enter size of array", ArraySizeInputChecker);
    object.content = (int*) malloc(object.size * sizeof(int));
    printf("Enter elements, one by one\n");
    for (int i = 0; i < object.size; i++)
    {
        object.content[i] = CycleInputInt("Enter next element",
                                          AnyIntInputChecker);
    }

    printf("Origin array:\n");
    PrintArray(&object);
    printf("Shell sorted:\n");
    ShellSort(&object);
    PrintArray(&object);
    /*printf("Selection sorted:\n");
    ArrayCopy(&object, &coppy);
    SelectionSort(&coppy);
    PrintArray(&coppy);
*/
    //free(object.content);
    //free(coppy.content);
}