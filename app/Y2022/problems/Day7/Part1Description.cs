using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day7;

public class Part1Description : Description
{
    public override string Text =>
@"Given an output from various commands, analyse the directory structure and calculate the sum of all directories with a maximum size of 100000.
1. Commands are executed on its own line and starts with ""$"".
2. ""$ cd /"" changes the current directory to the root directory.
3. ""$ cd .."" changes the current directory to its parent directory.
4. ""$ cd xyz"" changes the current directory to its sub directory named xyz.
5. ""$ ls"" lists the contents of the current directory.
6. The contents of the directory are each displayed on their own line.
7. File contents are formatted as: ""123 abc"" where 123 is the file size and abc is the file name.
8. Sub directory contents are formatted as: ""dir xyz"" where dir is fixed and xyz is the directory name.
9. Directory size includes all the files as well as the total sizes of each sub directory.
10. When solving for this problem, a directory and a sub directory should be treated as separate directories, even though the size of the directory already contains the size of the sub directory.";

    public override string Example =>
@"Given:
$ cd /
$ ls
dir d1
dir d2
$ cd d1
$ ls
30000 d1_f1.txt
dir d1_s1
dir d1_s2
$ cd d1_s1
$ ls
60000 d1_s1_f1.txt
$ cd ..
$ cd d1_s2
$ ls
10000 d1_s2_f1.txt
$ cd /
$ cd d2
$ ls
200000 d2_f2.txt

Output: 170000";

    public override string Explanation =>
@"Based on the command output, the directory structure could resemble something like this:
|----------------------------------------|------------|
| Structure                              | Total Size |
|----------------------------------------|------------|
| /                                      |     300000 |
| |-- d1                                 |     100000 |
| |   |-- d1_f1.txt (size: 30000)        |            |
| |   |-- d1_s1                          |      60000 |
| |   |   |-- d1_s1_f1.txt (size: 60000) |            |
| |   |                                  |            |
| |   |-- d1_s2                          |      10000 |
| |       |-- d1_s2_f1.txt (size: 10000) |            |
| |                                      |            |
| |-- d2                                 |     200000 |
|    |-- d2_f2.txt (size: 200000)        |            |
|                                        |            |
|----------------------------------------|------------|

The individual directories based on the structure above is as follows:
|-----------|------------|
| Directory | Total Size |
|-----------|------------|
| / (root)  |     300000 |
| d1        |     100000 |
| d1_s1     |      60000 |
| d1_s2     |      10000 |
| d2        |     200000 |
|-----------|------------|

There are 3 directories under 100000: d1 (100000), d1_s1 (60000), d1_s2 (10000) which has a combined total of 170000.

Note that sub directories are treated separately even though they are included in the total size of their parent directories.";
}