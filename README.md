# folder-splitter
Library to split a folder in two, moving selected files/folders to a new folder.

# Overview
This library is a simple utility intended to be used as a folder splitter, the same way Picasa used to allow a "Split folder here" command.
Given a folder, you want to choose a bunch of items into this folder and move them to a new one. The items you want to move
are probably selected in a previous step, where you sorted items (for example, by date)
The main goal I pursue is to split a folder starting at a date and move the items to a new ~~event~~ folder

The code is plain C#. It is developed using .NET Core 2.0

# Build
To build, please install .Net Core SDK version 2.0
Clone the repo and then move to the folder. Type:

``dotnet build``

to check its functionality, move to the test project folder and run XUnit:

```
cd tests\SplitFolderUnitTests
dotnet xunit
```

# Examples
Please see the unit tests to learne its usage.

```cs
var newFolder = FolderSplitter.Split(originalFolderPath, newFolderName, items);
```

Parameters are:
* the full path of the folder you want to split.
* the new folder name (it will created besides your original folder).
* a list of names of the files/folders you intend to move.
