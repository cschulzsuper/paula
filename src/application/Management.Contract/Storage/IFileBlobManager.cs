﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Paula.Application.Storage
{
    public interface IFileBlobManager
    {
        ValueTask<Stream> ReadAsync(string path);

        ValueTask<string> WriteAsync(Stream stream, string path);

        ValueTask<string> ReplaceAsync(Stream stream, string path, string btag);

        ValueTask RemoveAsync(string path, string btag);
    }
}