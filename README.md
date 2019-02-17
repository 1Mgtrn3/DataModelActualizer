# DataModelActualizer
A tool to update a datamodel using stored procedures (or packages) code analysis.
The idea is to search the code for any kind of JOIN statement, analyze it for table links and then - intersect the result with the existing set of foreign keys. The final result helps to create a better opportunity to analyze the database's data model.

## DBs supported
Currently, DataModelActualizer supports only Oracle Database.

### Prerequisites
ODAC 64-bit is needed. Machine-wide level installation option checked is recommended.
Here you can download it: https://www.oracle.com/technetwork/database/windows/downloads/index-090165.html
Note: currently csproj has only an x64 build option. If you plan to run this on a 32-bit system, you would need to download a 32-bit version of ODAC and to change build options in csproj for Release and Debug.

### Build
Building this was quite tricky because of ANTLR C#-optimized version code generation issues. If you want to uncomment things like <!--<Antlr4UseCSharpGenerator>True</Antlr4UseCSharpGenerator>--> you better think twice. Later I will upload an updated version of Readme.md with a better explanation of what do those comments mean.
With the current csproj setting building should be easy enough, just import all the NuGet packages VS wants you to, and you should be good to go.

## Using DataModelActualizer
Currently, it only has a UI-version since I want to show a standard workflow for the DataModelActualizer API.
It intended to work like this:
1. Change .config: add your Oracle DB credentials and change the standard SQL queries to fit your schema.
2. Run DataModelActualizer.exe
3. By default, the storage file is already created and set in .config but you can create a new one and switch to it.
4. Press "Scan ALL_OBJECTS". Wait for it to finish the task. Now if everything worked fine, you could open the storage file with something like DB Browser for SQLite and see that ActualDBObjects is now filled with tables, views, and packages from your Oracle DB. Those are only names and IDs used by DataModelActualizer
5. Press "Download packages".  Wait for it to finish the task and check out the sources in the directory set in .config as "packageDownloadDir".
6. At this point, you can either change the value of "packageSourceDir" to the value equal to packageDownloadDir's or copy download packages from one directory to another because DataModelActualizer processes packages only from packageSourceDir.
7. Press either "Scan Packages" or "Get foreign keys". Actually, you could have pressed "Get foreign keys" before downloading the packages but ... nevermind. Keep in mind that "scanning packages" is a time-consuming operation. During this operation, the program will be analyzing all of your source code for table links. Getting foreign keys is times faster. In the end, you need to press both buttons to get a full picture.
    
Now when all of the processing is complete, you can see the results. 
1. You can check out the Links table in the storage file. It contains the links between tables. It is always good to Get foreign keys first and see how many connections are defined using foreign keys and then compare that number to the number after analyzing the packages. There could be a big difference.
2. In this version of DataModelActualizer you can do two things apart from exploring the results using DB Browser: 
	* Finding a path from one table to another (always good when you don't know how to link two very different entities. And this works even when no one did that in code too!)
	* Get a tree structure with some table as a head of it and other linked tables as it's children. You can also change the depth you want to see.
	
	Both options give a JSON representation of a Node - an object type created to represent tables(views) and their links. VertexNode is another type used only for finding a path from one table to another.
## Current limitations
    DataModelActualizer can't: 
    * Fetch table links from SQL queries written as a text variable (ExecuteImmediate).
    * Fetch table links from View's SQL code.
    
    Both limitations will be fixed in future versions.
    
## Author
    Demid Zhukov - 1demidz3@gmail.com
    
## License
This project is licensed under the Apache License Version 2.0 - see the [LICENSE.md](LICENSE.md) file for details 



