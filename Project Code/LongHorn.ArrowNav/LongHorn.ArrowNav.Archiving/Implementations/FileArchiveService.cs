using LongHorn.Archiving;
using LongHorn.ArrowNav.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace LongHorn.Archiving
{
    public class FileArchiveService : IArchiveService
    {
        public bool archive() // could change later to Predicate<T>
        {
            IRepository<string> repository = new LoggingRepository();
            // statement: select * from Logging where UtcTimeStamp BETWEEN DATEADD(DAY,-30,GETUTCDATE()) and GETUTCDATE();
            // var result = repository.Read(filter);
            // check
            // Writes database contents to file
            List<string> result = new List<string>()
            {
                "carrot",
                "fox",
                "explorer"
            };
            string filename = @"C:\Users\curti\OneDrive\Desktop\filename.txt";
            using (FileStream fs = File.Create(filename))
            {
                for (int counter = 0; counter < result.Count; counter++)
                {
                    Byte[] fileContents = new UTF8Encoding(true).GetBytes(result[counter]+"\n");
                    fs.Write(fileContents, 0, fileContents.Length);
                }
            }
            // zips file just created
            string zipPath = @"C:\Users\curti\OneDrive\Desktop\test.zip";

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(filename, Path.GetFileName(filename));
            }
            return true;
            // Removes from database
            //for (int counter = 0; counter > result.Count; counter++)
            //{
            //    var result2 = repository.Delete(result[counter]);
            //}

        }
    }
}
