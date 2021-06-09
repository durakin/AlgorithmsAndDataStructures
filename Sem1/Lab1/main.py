import boyer_moore_horspool

if __name__ == '__main__':
    string = input("Enter origin string\n")
    substring = input("Enter a substring to find\n")
    casesens = True if input(
        "Enter \"Y\" if case sensitivity required,"
        "otherwise enter anything else\n") == 'Y' else False
    print(boyer_moore_horspool.find_substring(string, substring, casesens))

