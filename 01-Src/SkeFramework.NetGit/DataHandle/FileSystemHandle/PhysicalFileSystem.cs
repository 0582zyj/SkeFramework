﻿using SkeFramework.NetGit.DataCommon;
using SkeFramework.NetGit.DataHandle.FileStreamHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SkeFramework.NetGit.DataHandle.FileSystemHandle
{
    /// <summary>
    /// 物理文件系统
    /// </summary>
    public class PhysicalFileSystem
    {
        /// <summary>
        /// 默认流的缓冲区大小
        /// </summary>
        public const int DefaultStreamBufferSize = 8192;

        public virtual void DeleteDirectory(string path, bool recursive = true)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(path);

            if (recursive)
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Attributes = FileAttributes.Normal;
                    file.Delete();
                }

                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    this.DeleteDirectory(subDirectory.FullName);
                }
            }

            directory.Delete();
        }

        public virtual void CopyDirectoryRecursive(string srcDirectoryPath, string dstDirectoryPath)
        {
            DirectoryInfo srcDirectory = new DirectoryInfo(srcDirectoryPath);

            if (!this.DirectoryExists(dstDirectoryPath))
            {
                this.CreateDirectory(dstDirectoryPath);
            }

            foreach (FileInfo file in srcDirectory.EnumerateFiles())
            {
                this.CopyFile(file.FullName, Path.Combine(dstDirectoryPath, file.Name), overwrite: true);
            }

            foreach (DirectoryInfo subDirectory in srcDirectory.EnumerateDirectories())
            {
                this.CopyDirectoryRecursive(subDirectory.FullName, Path.Combine(dstDirectoryPath, subDirectory.Name));
            }
        }

        public virtual bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public virtual bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public virtual void CopyFile(string sourcePath, string destinationPath, bool overwrite)
        {
            File.Copy(sourcePath, destinationPath, overwrite);
        }

        public virtual void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public virtual string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public virtual byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public virtual IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }

        public virtual void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public virtual bool TryWriteAllText(string path, string contents)
        {
            try
            {
                this.WriteAllText(path, contents);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch //(SecurityException)
            {
                return false;
            }
        }

        public Stream OpenFileStream(string path, FileMode fileMode, FileAccess fileAccess, FileShare shareMode, bool callFlushFileBuffers)
        {
            return this.OpenFileStream(path, fileMode, fileAccess, shareMode, FileOptions.None, callFlushFileBuffers);
        }

        public virtual void MoveAndOverwriteFile(string sourceFileName, string destinationFilename)
        {
            File.Move(sourceFileName, destinationFilename);
        }

        public virtual Stream OpenFileStream(string path, FileMode fileMode, FileAccess fileAccess, FileShare shareMode, FileOptions options, bool callFlushFileBuffers)
        {
            if (callFlushFileBuffers)
            {
                return new FlushToDiskFileStream(path, fileMode, fileAccess, shareMode, DefaultStreamBufferSize, options);
            }

            return new FileStream(path, fileMode, fileAccess, shareMode, DefaultStreamBufferSize, options);
        }

        public virtual void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        //public virtual bool TryCreateDirectoryWithAdminAndUserModifyPermissions(string directoryPath, out string error)
        //{
        //    return ScalarPlatform.Instance.FileSystem.TryCreateDirectoryWithAdminAndUserModifyPermissions(directoryPath, out error);
        //}

        //public virtual bool TryCreateOrUpdateDirectoryToAdminModifyPermissions( string directoryPath, out string error)
        //{
        //    return ScalarPlatform.Instance.FileSystem.TryCreateOrUpdateDirectoryToAdminModifyPermissions( directoryPath, out error);
        //}

        public virtual IEnumerable<DirectoryItemInfo> ItemsInDirectory(string path)
        {
            DirectoryInfo ntfsDirectory = new DirectoryInfo(path);
            foreach (FileSystemInfo ntfsItem in ntfsDirectory.GetFileSystemInfos())
            {
                DirectoryItemInfo itemInfo = new DirectoryItemInfo()
                {
                    FullName = ntfsItem.FullName,
                    Name = ntfsItem.Name,
                    IsDirectory = (ntfsItem.Attributes & FileAttributes.Directory) != 0
                };

                if (!itemInfo.IsDirectory)
                {
                    itemInfo.Length = ((FileInfo)ntfsItem).Length;
                }

                yield return itemInfo;
            }
        }

        public virtual IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public virtual IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }

        public virtual FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public virtual void SetAttributes(string path, FileAttributes fileAttributes)
        {
            File.SetAttributes(path, fileAttributes);
        }

        public virtual string[] GetFiles(string directoryPath, string mask)
        {
            return Directory.GetFiles(directoryPath, mask);
        }

        public bool TryWriteTempFileAndRename(string destinationPath, string contents, out Exception handledException)
        {
            handledException = null;
            string tempFilePath = destinationPath + ".temp";

            string parentPath = Path.GetDirectoryName(tempFilePath);
            this.CreateDirectory(parentPath);

            try
            {
                using (Stream tempFile = this.OpenFileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None, callFlushFileBuffers: true))
                using (StreamWriter writer = new StreamWriter(tempFile))
                {
                    writer.Write(contents);
                    tempFile.Flush();
                }

                this.MoveAndOverwriteFile(tempFilePath, destinationPath);
                return true;
            }
            catch (Win32Exception e)
            {
                handledException = e;
                return false;
            }
            catch (IOException e)
            {
                handledException = e;
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                handledException = e;
                return false;
            }
        }

        /// <summary>
        /// Recursively deletes a directory and all contained contents.
        /// </summary>
        public bool TryDeleteDirectory(string path, out Exception exception)
        {
            try
            {
                this.DeleteDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                // The directory does not exist - follow the
                // convention of this class and report success
            }
            catch (Exception e) when (e is IOException ||
                                      e is UnauthorizedAccessException ||
                                      e is ArgumentException)
            {
                exception = e;
                return false;
            }

            exception = null;
            return true;
        }

        /// <summary>
        /// Attempts to delete a file
        /// </summary>
        /// <param name="path">Path of file to delete</param>
        /// <returns>True if the delete succeed, and false otherwise</returns>
        /// <remarks>The files attributes will be set to Normal before deleting the file</remarks>
        public bool TryDeleteFile(string path)
        {
            Exception exception;
            return this.TryDeleteFile(path, out exception);
        }

        /// <summary>
        /// Attempts to delete a file
        /// </summary>
        /// <param name="path">Path of file to delete</param>
        /// <param name="exception">Exception thrown, if any, while attempting to delete file (or reset file attributes)</param>
        /// <returns>True if the delete succeed, and false otherwise</returns>
        /// <remarks>The files attributes will be set to Normal before deleting the file</remarks>
        public bool TryDeleteFile(string path, out Exception exception)
        {
            exception = null;
            try
            {
                if (this.FileExists(path))
                {
                    this.SetAttributes(path, FileAttributes.Normal);
                    this.DeleteFile(path);
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                // SetAttributes could not find the file
                return true;
            }
            catch (IOException e)
            {
                exception = e;
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                exception = e;
                return false;
            }
        }

        /// <summary>
        /// Attempts to delete a file
        /// </summary>
        /// <param name="path">Path of file to delete</param>
        /// <param name="metadataKey">Prefix to be used on keys when new entries are added to the metadata</param>
        /// <param name="metadata">Metadata for recording failed deletes</returns>
        /// <remarks>The files attributes will be set to Normal before deleting the file</remarks>
        public bool TryDeleteFile(string path, string metadataKey, EventMetadata metadata)
        {
            Exception deleteException = null;
            if (!this.TryDeleteFile(path, out deleteException))
            {
                metadata.Add($"{metadataKey}_DeleteFailed", "true");
                if (deleteException != null)
                {
                    metadata.Add($"{metadataKey}_DeleteException", deleteException.ToString());
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Retry delete until it succeeds (or maximum number of retries have failed)
        /// </summary>
        /// <param name="tracer">ITracer for logging and telemetry, can be null</param>
        /// <param name="path">Path of file to delete</param>
        /// <param name="retryDelayMs">
        /// Amount of time to wait between each delete attempt.  If 0, there will be no delays between attempts
        /// </param>
        /// <param name="maxRetries">Maximum number of retries (if 0, a single attempt will be made)</param>
        /// <param name="retryLoggingThreshold">
        /// Number of retries to attempt before logging a failure.  First and last failure is always logged if tracer is not null.
        /// </param>
        /// <returns>True if the delete succeed, and false otherwise</returns>
        /// <remarks>The files attributes will be set to Normal before deleting the file</remarks>
        public bool TryWaitForDelete(
            string path,
            int retryDelayMs,
            int maxRetries,
            int retryLoggingThreshold)
        {
            int failureCount = 0;
            while (this.FileExists(path))
            {
                Exception exception = null;
                if (!this.TryDeleteFile(path, out exception))
                {
                    if (failureCount == maxRetries)
                    {
                        
                        {
                            EventMetadata metadata = new EventMetadata();
                            if (exception != null)
                            {
                                metadata.Add("Exception", exception.ToString());
                            }

                            metadata.Add("path", path);
                            metadata.Add("failureCount", failureCount + 1);
                            metadata.Add("maxRetries", maxRetries);
                        }

                        return false;
                    }
                    else
                    {
                        if ( failureCount % retryLoggingThreshold == 0)
                        {
                            EventMetadata metadata = new EventMetadata();
                            metadata.Add("Exception", exception.ToString());
                            metadata.Add("path", path);
                            metadata.Add("failureCount", failureCount + 1);
                            metadata.Add("maxRetries", maxRetries);
                        }
                    }

                    ++failureCount;

                    if (retryDelayMs > 0)
                    {
                        Thread.Sleep(retryDelayMs);
                    }
                }
            }

            return true;
        }
    }
}
