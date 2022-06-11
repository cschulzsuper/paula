﻿using Super.Paula.Application.Storage.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Paula.Application.Storage
{
    public interface IFileBlobHandler
    {
        ValueTask<Stream> ReadAsync(string container, string uniqueName);

        ValueTask<FileBlobResponse> WriteAsync(Stream stream, string container);

        ValueTask<FileBlobResponse> WriteAsync(Stream stream, string container, string uniqueName, string? btag);

        ValueTask DeleteAsync(string container, string uniqueName, string btag);
    }
}