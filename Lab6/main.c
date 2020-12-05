#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

enum Constants
{
    INPUT_SIZE = 100
};

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

bool PositiveArrayInputChecker(int arraySize)
{
    return arraySize > 0;
}

bool AnyIntInputChecker(int _)
{
    return true;
}

typedef struct
{
    bool* content;
    int size;
} DynVisited;

typedef struct
{
    int from;
    int to;
} Edge;

typedef struct
{
    Edge* content;
    int size;
} DynEdgeArray;

void Dfs(DynVisited* visited, DynEdgeArray* edgeArray, )
{
    for(int i = 0; i < visited->size; i++)
    {
        for(int j = 0; j < edgeArray->size; j++)
        {

        }
    }
}

int main()
{
    DynVisited visited;
    visited.size = CycleInputInt(
            "Enter number of vertices of graph. They will be numerated starting from 1",
            PositiveArrayInputChecker);
    visited.content = (bool*) malloc(visited.size * sizeof(bool));
    for (int i = 0; i < visited.size; i++)
    {
        visited.content = false;
    }
    int edgesNumber;
    edgesNumber = CycleInputInt("Enter number of edges of graph",
                                PositiveArrayInputChecker);
    printf("VERTICES INPUT.\n");
    char edgeString[INPUT_SIZE];
    Edge* edges;
    edges = (Edge*) malloc(edgesNumber * sizeof(Edge));
    for (int i = 0; i < edgesNumber; i++)
    {
        sprintf(edgeString, "%d", i);
        int from = CycleInputInt(strcat(strcat("Vertex № ", edgeString),
                                        " FROM:"), PositiveArrayInputChecker);
        int to = CycleInputInt(strcat(
                strcat("Vertex № ", PositiveArrayInputChecker),
                " TO:"), PositiveArrayInputChecker);
        if (from > visited.size || to > visited.size || from == to)
        {
            printf("Wrong format!");
            i--;
            continue;
        }
    }



}