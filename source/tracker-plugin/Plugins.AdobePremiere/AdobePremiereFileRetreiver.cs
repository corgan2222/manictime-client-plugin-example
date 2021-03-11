﻿using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobePremiere
{
    public class AdobePremiereFileRetreiver : IDocumentRetreiver
    {
        public DocumentInfo GetDocument(ApplicationInfo application)
        {
            // first we use processName to see if it is Notepad. If it is not Notepad, we just return null and tracker will move on to the next plugin

            if (!CheckForProcess(application))
                return null;

            // get the filename
            string filename = GetFilename(application);

            // DocumentName - this will be displayed in the details view (bottom left part of ManicTime)
            // DocumentGroupName = this will be displayed on the bottom right side (group name)
            // DocumentType = file, url...


            // Notepad will only display filename, not the full path, so we will use the same filename for DocumentName and DocumentGroupName
            // if we could get the full path, for example c:\dir\filename.txt, then we could use 
            // 
            // DocumentName = c:\dir\filename.txt
            // DocumentGroupName = filename.txt

            if (filename != null)
                return new DocumentInfo() { DocumentName = filename, DocumentGroupName = filename, DocumentType = DocumentTypes.File };

            return null;
        }

        private bool CheckForProcess(ApplicationInfo application)
        {
            if (application.ProcessName == "Adobe Premiere Pro")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {
            // notepad title looks like  'filename - Notepad'. We are only after the first part, 'filename'
            //var i = application.WindowTitle.LastIndexOfAny(new []{ '-', '–' });

            int pFrom = application.WindowTitle.LastIndexOf("\\") + "\\".Length;
            int pTo = application.WindowTitle.LastIndexOf(".");

            if (pFrom > 0)
            {
                return application.WindowTitle.Substring(pFrom, pTo - pFrom).Trim();
            }

            return null;
        }
    }
}
