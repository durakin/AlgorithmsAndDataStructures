def find_substring(origin_string, origin_substring, is_case_sensitive):
    shift = dict()
    obj_string = origin_string[
                 :] if is_case_sensitive else origin_string.casefold()
    obj_substring = origin_substring[
                    :] if is_case_sensitive else origin_substring.casefold()

    for i in range(len(obj_substring)):
        index = ((-i - 2) % (len(obj_substring)))
        if obj_substring[index] not in shift.keys():
            shift[obj_substring[index]] = i + 1

    position = 0
    while position < len(obj_string) - len(obj_substring) + 1:
        for i in reversed(range(len(obj_substring))):
            if obj_substring[i] != obj_string[position + i]:
                position += shift.get(
                    obj_string[position + len(obj_substring) - 1],
                    len(obj_substring))
                break
            return position
    return None
