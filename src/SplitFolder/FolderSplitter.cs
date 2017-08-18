using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SplitFolder
{
    public static class FolderSplitter
    {
        /// <summary>
        /// Split a folder in two, given the new folder name and the items that will move
        /// </summary>
        /// <param name="originalFolderPath">Original folder full path</param>
        /// <param name="newFolderName">Name of the new folder. It will be created beside the original folder.</param>
        /// <param name="items">Items' names. These items will end up into the newly created folder.</param>
        /// <returns>
        ///     The full path of the new folder is returned
        ///     If nothing is moved (i.e.: no files are splitted), the original folder full path is returned
        /// </returns>
        public static string Split(string originalFolderPath, string newFolderName, IEnumerable<string> items)
        {
            if(!items.Any())
                return originalFolderPath;

            var originalDirectoryInfo = new DirectoryInfo(originalFolderPath);
            var newFolderPath = Path.Combine(originalDirectoryInfo.Parent.FullName, newFolderName);
            var newDirectoryInfo = Directory.CreateDirectory(newFolderPath);

            foreach (var entry in items)
            {
                var sourceFullPath = Path.Combine(originalFolderPath, entry);
                if(!File.Exists(sourceFullPath) && !Directory.Exists(sourceFullPath))
                    continue;
                var destFullPath = Path.Combine(newFolderPath, entry);
                Directory.Move(sourceFullPath, destFullPath);
            }

            return newDirectoryInfo.FullName;
        }

    }
}
