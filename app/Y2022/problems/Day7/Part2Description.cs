using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day7;

public class Part2Description : Description
{
    public override string Text =>
@"Given an output from various commands, analyse the directory structure and find the smallest directory greater than the additional size needed.
1. Commands are executed on its own line and starts with ""$"".
2. ""$ cd /"" changes the current directory to the root directory.
3. ""$ cd .."" changes the current directory to its parent directory.
4. ""$ cd xyz"" changes the current directory to its sub directory named xyz.
5. ""$ ls"" lists the contents of the current directory.
6. The contents of the directory are each displayed on their own line.
7. File contents are formatted as: ""123 abc"" where 123 is the file size and abc is the file name.
8. Sub directory contents are formatted as: ""dir xyz"" where dir is fixed and xyz is the directory name.
9. Directory size includes all the files as well as the total sizes of each sub directory.
10. The total disk size = 70000000.
11. The required size = 30000000.
12. The remaining disk space = total disk size - total size of root directory.
13. The additional size needed = required size - remaining disk space.
14. Find the smallest directory with a total size >= additional size needed.";

    public override string Example =>
@"Given:
$ cd /
$ ls
dir d1
dir d2
$ cd d1
$ ls
10000 d1_f1.txt
dir d1_s1
dir d1_s2
$ cd d1_s1
$ ls
60000 d1_s1_f1.txt
$ cd ..
$ cd d1_s2
$ ls
1000000 d1_s2_f1.txt
$ cd /
$ cd d2
$ ls
39930000 d2_f2.txt

Output: 1000000";

    public override string Explanation =>
@"Based on the command output, the directory structure could resemble something like this:
|------------------------------------------|------------|
| Structure                                | Total Size |
|------------------------------------------|------------|
| /                                        |   41000000 |
| |-- d1                                   |    1070000 |
| |   |-- d1_f1.txt (size: 10000)          |      10000 |
| |   |-- d1_s1                            |      60000 |
| |   |   |-- d1_s1_f1.txt (size: 60000)   |            |
| |   |                                    |            |
| |   |-- d1_s2                            |    1000000 |
| |       |-- d1_s2_f1.txt (size: 1000000) |            |
| |                                        |            |
| |-- d2                                   |   39930000 |
|    |-- d2_f2.txt (size: 39930000)        |            |
|                                          |            |
|------------------------------------------|------------|

The individual directories based on the structure above is as follows:
|-----------|------------|
| Directory | Total Size |
|-----------|------------|
| / (root)  |   41000000 |
| d1        |    1070000 |
| d1_s1     |      60000 |
| d1_s2     |    1000000 |
| d2        |   39930000 |
|-----------|------------|

Total disk size: 70000000
Required size: 30000000
Total size of root directory: 41000000
Remaining disk space: 29000000 (total disk size - total size of root directory)
Additional size needed: 1000000 (required size - remaining disk space)
Total size of smallest directory >= additional size needed: 1000000 (d1_s2)";
}