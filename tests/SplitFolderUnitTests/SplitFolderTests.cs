using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SplitFolder;
using Xunit;

namespace SplitFolderUnitTests
{
    public class SplitFolderTests
    {
        string BasePath = Path.Combine(Path.GetTempPath(), "SPLIT_TESTS");

        public SplitFolderTests()
        {
            Cleanup(BasePath);
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(1,0)]
        [InlineData(5,0)]
        [InlineData(10,0)]
        [InlineData(100,0)]
        [InlineData(0,1)]
        [InlineData(0,5)]
        [InlineData(0,10)]
        [InlineData(0, 100)]
        [InlineData(5,1)]
        [InlineData(10,5)]
        [InlineData(100,10)]
        [InlineData(100, 100)]        
        public void FolderWithFiles_SplitFolder_FilesAreMoved(int fileCount, int dirCount)
        {
            // Arrange
            var folderName = Path.Combine(BasePath, $"20170101. {fileCount:000}_{dirCount:000}_Test");
            var newFolderName = "SplittedFolder";

            Console.WriteLine($"Splitting {folderName} into {newFolderName}...");
            Setup(folderName, fileCount, dirCount);

            var itemsMoving = new List<string>();
            int movingFilesCount = fileCount / 2;
            for (var i = movingFilesCount; i < fileCount; i++)
            {
                itemsMoving.Add($"file_{i}.txt");
            }
            int movingDirsCount = dirCount / 2;
            for (var i = movingDirsCount; i < dirCount; i++)
            {
                itemsMoving.Add($"dir_{i}");
            }


            // Act
            Console.WriteLine($"Splitting into '{newFolderName}'...");
            var newFolder = FolderSplitter.Split(folderName, newFolderName, itemsMoving);


            // Assert
            var newFolderFiles = Directory.GetFiles(newFolder);
            var newFolderDirs = Directory.GetDirectories(newFolder);
            foreach (var itemMoving in itemsMoving)
            {
                var isDir = itemMoving.StartsWith("dir");
                if (!isDir)
                {
                    Assert.Contains(Path.Combine(newFolder, itemMoving), newFolderFiles);
                    Assert.DoesNotContain(Path.Combine(folderName, itemMoving), newFolderFiles);
                }
                else
                {
                    Assert.Contains(Path.Combine(newFolder, itemMoving), newFolderDirs);
                    Assert.DoesNotContain(Path.Combine(folderName, itemMoving), newFolderDirs);

                }
            }


            // Cleanup
            Directory.Delete(newFolder, true);
            Cleanup(folderName);
        }



        private void Setup(string folder, int numberOfFiles, int numberOfDirs)
        {
            Cleanup(folder);
            Directory.CreateDirectory(folder);

            for (var i = 0; i < numberOfFiles; i++)
            {
                var filePath = Path.Combine(folder, "file_" + i + ".txt");
                var contents = @"This is the contents of file " + i;
                File.WriteAllText(filePath, contents);
            }

            for (var i = 0; i < numberOfDirs; i++)
            {
                var dirPath = Path.Combine(folder, "dir_" + i);
                Directory.CreateDirectory(dirPath);
            }
        }

        private void Cleanup(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }

    }
}
