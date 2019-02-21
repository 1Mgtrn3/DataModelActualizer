# Database Detective
A tool to update a data model using stored procedures (or packages) code analysis.
The idea is to search the code for any kind of JOIN statement, analyze it for table links (while calculating the popularity of those links) and then - intersect the result with the existing set of foreign keys. The final result creates a better opportunity to analyze the database's data model and optimize the code.

## DBs supported
Currently, Database Detective supports only Oracle Database.

### Prerequisites
ODAC 64-bit is needed. Machine-wide level installation option checked is recommended.
You can download it from the Oracle's website [here](https://www.oracle.com/technetwork/database/windows/downloads/index-090165.html).

Note: currently csproj has only an x64 build option. If you plan to run this on a 32-bit system, you would need to download a 32-bit version of ODAC and to change build options in csproj for Release and Debug. Please check what Oracle.DataAccess libraries do you have in GAC after ODAC installation:

    gacutil /l Oracle.DataAccess

In this configuration, Detective uses the ones with AMD64 architecture, not x86.

The Detective's main product is the storage file. It's a simple SQLite DB so you can query the database using tools like DB Browser for SQLite which you can find here:

[https://sqlitebrowser.org/dl/](https://sqlitebrowser.org/dl/)

### Build
Building this was quite tricky because of ANTLR C#-optimized version code generation issues. If you want to uncomment things like <!--<Antlr4UseCSharpGenerator>True</Antlr4UseCSharpGenerator>--> you better think twice. Later I will upload an updated version of Readme.md with a better explanation of what do those comments mean.
With the current csproj setting building should be easy enough, just import all the NuGet packages VS wants you to, and you should be good to go.

## Using Database Detective
Currently, it only has a UI-version since I want to show a standard workflow for the Database Detective API.
It intended to work like this:
1. Change .config: add your Oracle DB credentials and change the standard SQL queries to fit your schema.
2. Run Database Detective.exe
3. By default, the storage file is already created and set in .config, but you can create a new one and switch to it.
4. Press "Scan ALL_OBJECTS". Wait for it to finish the task. Now if everything worked fine, you could open the storage file with something like DB Browser for SQLite and see that ActualDBObjects is now filled with tables, views, and packages from your Oracle DB. Those are only names and IDs used by Database Detective
5. Press "Download packages".  Wait for it to finish the task and check out the sources in the directory set in .config as "packageDownloadDir".
6. At this point, you can either change the value of "packageSourceDir" to the value equal to packageDownloadDir's or copy download packages from one directory to another because Database Detective processes packages only from packageSourceDir.
7. Press either "Scan Packages" or "Get foreign keys". Actually, you could have pressed "Get foreign keys" before downloading the packages but ... nevermind. Keep in mind that "scanning packages" is a time-consuming operation. During this operation, the program will be analyzing all of your source code for table links. Getting foreign keys is times faster. In the end, you need to press both buttons to get a full picture.
    
Now when all of the processing is complete, you can check out the results. 
1. You can check out the Links table in the storage file. It contains the links between tables and the popularity of them among the code. It is always good to Get foreign keys first and see how many connections are defined using foreign keys and then compare that number to the number after analyzing the packages. There could be a big difference.
Also, you can use the popularity stat to optimize the code. Though the Detective's API has some methods to work with this stat, it is recommended to use DB Browser for SQLite to analyze it.
2. In this version of Database Detective you can do four things apart from exploring the results using DB Browser: 
    1) Find a path from one table to another (always good when you don't know how to link two very different entities. And this works even when no one did that in code too!)
    2) Get a tree structure with some table as a head of it and other linked tables as it's children. You can also change the depth you want to see.
    3) Get the links which have popularity above some limit.
    4) Get N most popular links among your code.
    
    All of the options return a JSON representation of a corresponding type: Node(1,2) or Link(3,4). 

Node is an object type created to represent tables(views) and their links(Links table's primary key - toText to be precise, because it's redundant to place a full Link object here). VertexNode is another type used only for finding a path from one table to another.
## Current limitations
Database Detective can't: 
* Fetch table links from SQL queries written as a text variable (ExecuteImmediate).
* Fetch table links from View's SQL code.

Both limitations will be fixed in future versions.
    
## Author
    Demid Zhukov - 1demidz3@gmail.com
    
## License
This project is licensed under the Apache License Version 2.0 - see the [LICENSE](LICENSE) file for details 



