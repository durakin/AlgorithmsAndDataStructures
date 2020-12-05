#include <stdio.h>
#include <stdbool.h>

bool SplitSequence(FILE* origin, FILE* a, FILE* b, int step)
{
    int i;
    i = 0;
    int current;

    while (fscanf(origin, "%d", &current) != EOF)
    {
        if (!((i / step) % 2))
        {
            fprintf(a, "%d\n", current);
        }
        else
        {
            fprintf(b, "%d\n", current);
        }
        i++;
    }
    return i > step;
}

void MergeSequences(FILE* a, FILE* b, FILE* result, int step)
{
    FILE* switchFile;
    switchFile = NULL;
    int tempA;
    int tempB;
    int next;
    int aPassed;
    int bPassed;
    bool aFinish;
    bool bFinish;
    int lastScan;
    aFinish = false;
    bFinish = false;
    aPassed = -1;
    bPassed = -1;

    while (!aFinish || !bFinish)
    {
        if (!aFinish && switchFile != a)
        {
            lastScan = fscanf(a, "%d", &tempA);
            if (lastScan == EOF)
            {
                aFinish = true;
            }
            aPassed++;
        }
        if (!bFinish && switchFile != b)
        {
            lastScan = fscanf(b, "%d", &tempB);
            if (lastScan == EOF)
            {
                bFinish = true;
            }
            bPassed++;
        }
        if (aFinish && bFinish)
        {
            break;
        }
        if (aFinish)
        {
            fprintf(result, "%d\n", tempB);
            switchFile = NULL;
            continue;
        }
        if (bFinish)
        {
            fprintf(result, "%d\n", tempA);
            switchFile = NULL;
            continue;
        }
        if (aPassed / step == bPassed / step)
        {
            if (tempA < tempB)
            {
                next = tempA;
                switchFile = b;
            }
            else
            {
                next = tempB;
                switchFile = a;
            }
        }
        else
        {
            if (aPassed > bPassed)
            {
                next = tempB;
                switchFile = a;
            }
            else
            {
                next = tempA;
                switchFile = b;
            }
        }
        fprintf(result, "%d\n", next);
    }
}

int main()
{
    int step = 1;
    int intToPrint;
    bool notSorted;
    FILE* originFile = fopen("origin.txt", "r");
    printf("Origin set:\n");
    while (fscanf(originFile, "%d", &intToPrint) != EOF)
    {
        printf("%d ", intToPrint);
    }
    fclose(originFile);

    do
    {
        originFile = fopen("origin.txt", "r");
        FILE* aFile = fopen("a.txt", "w");
        FILE* bFile = fopen("b.txt", "w");

        notSorted = SplitSequence(originFile, aFile, bFile, step);

        fclose(originFile);
        fclose(aFile);
        fclose(bFile);

        aFile = fopen("a.txt", "r");
        bFile = fopen("b.txt", "r");

        FILE* destFile = fopen("origin.txt", "w");

        MergeSequences(aFile, bFile, destFile, step);

        fclose(aFile);
        fclose(bFile);
        fclose(destFile);

        step *= 2;
    } while (notSorted);
    printf("\nSorted set:\n");
    originFile = fopen("origin.txt", "r");
    while (fscanf(originFile, "%d", &intToPrint) != EOF)
    {
        printf("%d ", intToPrint);
    }
    fclose(originFile);
    return 0;
}