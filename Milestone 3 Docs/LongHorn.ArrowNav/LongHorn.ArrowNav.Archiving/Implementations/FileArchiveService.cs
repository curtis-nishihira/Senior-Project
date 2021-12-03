﻿using LongHorn.Archiving;
using LongHorn.ArrowNav.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LongHorn.Archiving.Implementations
{
    class FileArchiveService : IArchiveService
    {
        public bool archive() // could change later to Predicate<T>
        {
            throw new NotImplementedException();
            IRepository<string> repository = new SqlDAO();
            string filter = "date > now - 30"; // configurable value not hardcoded
            var result = repository.Read(filter);
            // check
            // Writes database contents to file
            string filename = @"C:\Temp\filename.txt";
            using (FileStream fs = File.Create(filename))
            {
                for (int counter = 0; counter > result.Count; counter++)
                {
                    Byte[] fileContents = new UTF8Encoding(true).GetBytes(result[counter]);
                    fs.Write(fileContents, 0, fileContents.Length);
                }
            }
            // zips file just created
            ZipFile.CreateFromDirectory(filename, filename.Replace(".txt", ".zip"));
            // Removes from database
            for (int counter = 0; counter > result.Count; counter++)
            {
                var result2 = repository.Delete(result[counter]);
            }

        }
    }
}
