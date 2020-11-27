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

bool SplitSequence(FILE* origin, FILE* a, FILE* b, int step)
{
    printf("Split started\n");
    int i;
    i = 0;
    int current;
    while (fscanf(origin, "%d", &current) != EOF)
    {
        if (!((i / step) % 2))
        {
            fprintf(a, "%d\n", current);
            printf("%d\n", current);
        }
        else
        {
            fprintf(b, "%d\n", current);
            printf("%d\n", current);
        }
        i++;
    }
    return (i >= step) ? true : false;
}

bool MergeSequences(FILE* a, FILE* b, FILE* result, int step)
{
    printf("Merge started\n");
    int seriesMerged = 0;
    FILE* switchFile;
    FILE* contrSwitch;
    int tempA;
    int tempB;
    int temp;
    int next;
    bool aFinish;
    bool bFinish;
    int aScan;
    int bScan;
    aFinish = false;
    bFinish = false;
    while (!aFinish && !bFinish)
    {

        if (!aFinish)
        {
            aScan = fscanf(a, "%d", &tempA);
            if (aScan == EOF)
            {
                aFinish = true;
            }
        }
        if (!bFinish)
        {
            bScan = fscanf(b, "%d", &tempB);
            if (bScan == EOF)
            {
                bFinish = true;
            }
        }


        if (!aFinish && (tempA > tempB || bFinish))
        {
            temp = tempB;
            next = tempA;
            switchFile = a;
            contrSwitch = b;
        }
        else
        {
            temp = tempA;
            next = tempB;
            switchFile = b;
            contrSwitch = a;
        }

        if (aFinish && bFinish)
        {
            continue;
        }

        int lastScan;
        fprintf(result, "%d\n", next);
        printf("%d\n", next);

        for (int j = 1; j < step; j++)
        {
            lastScan = fscanf(switchFile, "%d", &next);
            if (lastScan == EOF)
            {
                if (switchFile == a)
                {
                    aFinish = true;
                }
                else
                {
                    bFinish = true;
                }
                break;
            }
            else
            {
                fprintf(result, "%d\n", next);
                printf("%d\n", next);
            }
        }


        if (contrSwitch == a && aFinish)
        {
            continue;
        }
        if (contrSwitch == b && bFinish)
        {
            continue;
        }

        fprintf(result, "%d\n", temp);
        printf("%d\n", temp);

        for (int j = 1; j < step; j++)
        {
            lastScan = fscanf(contrSwitch, "%d", &next);
            if (lastScan == EOF)
            {
                if (contrSwitch == a)
                {
                    aFinish = true;
                }
                else
                {
                    bFinish = true;
                }
                break;
            }
            fprintf(result, "%d\n", next);
            printf("%d\n", next);

        }
    }
    /*while (fscanf(switchFile, "%d", &temp) != EOF)
    {
        fprintf(result, "%d\n", temp);
    }
*/
    return true;

}

int main()
{

    int a = 0;
    int step = 1;
    bool notSorted;

    do
    {
        FILE* originFile = fopen("origin.txt", "r");
        FILE* aFile = fopen("a.txt", "w");
        FILE* bFile = fopen("b.txt", "w");

        notSorted = SplitSequence(originFile, aFile, bFile, step);

        fclose(originFile);
        fclose(aFile);
        fclose(bFile);

        aFile = fopen("a.txt" , "r");
        bFile = fopen("b.txt", "r");

        FILE *destFile = fopen("origin.txt", "w");

        MergeSequences(aFile, bFile, destFile, step);

        fclose(aFile);
        fclose(bFile);
        fclose(destFile);

        step*=2;
        a++;
    }
    while (notSorted);
    printf("%d", a);
}