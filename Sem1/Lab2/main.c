#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

enum Constants
{
    INPUT_SIZE = 100
};

int DefaultComp(const int* i, const int* j)
{
    return *i - *j;
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

void arrayCopy(DynArray* origin, DynArray* object)
{
    object->size = origin->size;
    object->content = (int*) malloc(object->size * sizeof(int));
    for (int i = 0; i < object->size; i++)
    {
        object->content[i] = origin->content[i];
    }
}

double arrayAverage(DynArray* object)
{
    double result = 0;
    for (int i = 0; i < object->size; i++)
    {
        result += object->content[i];
    }
    result /= object->size;
    return result;
}

double arrayMidpoint(DynArray* object)
{
    double result;
    result = 0;
    DynArray copy;
    arrayCopy(object, &copy);
    qsort(copy.content, copy.size, sizeof(int),
          (int (*)(const void*, const void*)) DefaultComp);
    if (object->size % 2 == 0)
    {
        result += (copy.content[(object->size / 2) - 1] +
                   copy.content[object->size / 2]);
        result/=2;
    }
    else
    {
        result = copy.content[object->size / 2];
    }
    free(copy.content);
    return result;
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
    printf("Average: %0.2f\n", arrayAverage(&object));
    printf("Midpoint: %0.2f\n", arrayMidpoint(&object));
    free(object.content);
}
