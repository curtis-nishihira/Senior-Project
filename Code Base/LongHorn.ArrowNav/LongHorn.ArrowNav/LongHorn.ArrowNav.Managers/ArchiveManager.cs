using LongHorn.Archiving;
using System;

namespace LongHorn.ArrowNav.Managers
{
    public class ArchiveManager
    {
        public ArchiveManager()
        {
        }

        public string Archive()
        {
            FileArchiveService archiveService = new FileArchiveService();
            archiveService.TimerStart();
            var result = archiveService.Archive();
            var totalSeconds = archiveService.TimerEnd();
            if (totalSeconds <= 60)
            {
                if (result.Equals("Successful Removal"))
                {
                    return "Archive Successful";
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return "Took longer than 60 seconds.";
            }
        }

    }
}
