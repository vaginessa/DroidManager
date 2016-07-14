﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpAdbClient.Tests
{
    internal class DummySyncService : ISyncService
    {
        public Dictionary<string, Stream> UploadedFiles
        { get; private set; } = new Dictionary<string, Stream>();

        public bool IsOpen
        {
            get {  return true; }
        }

        public void Dispose()
        {
        }

        public IEnumerable<FileStatistics> GetDirectoryListing(string remotePath)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
        }

        public void Pull(string remotePath, Stream stream, CancellationToken cancellationToken)
        {
        }

        public void Push(Stream stream, string remotePath, int permissions, DateTime timestamp, CancellationToken cancellationToken)
        {
            this.UploadedFiles.Add(remotePath, stream);
        }

        public FileStatistics Stat(string remotePath)
        {
            throw new NotImplementedException();
        }
    }
}
