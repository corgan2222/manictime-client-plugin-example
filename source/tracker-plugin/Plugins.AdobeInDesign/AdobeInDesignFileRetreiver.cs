using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobeInDesign
{
    public class AdobeInDesignFileRetreiver : IDocumentRetreiver
    {
        public DocumentInfo GetDocument(ApplicationInfo application)
        {
            // first we use processName to see if it is Notepad. If it is not Notepad, we just return null and tracker will move on to the next plugin

            if (!CheckForProcess(application))
                return null;

            // get the filename
            string filename = GetFilename(application);


            if (filename != null)
                return new DocumentInfo() { DocumentName = filename, DocumentGroupName = filename, DocumentType = DocumentTypes.File };

            return null;
        }

        private bool CheckForProcess(ApplicationInfo application)
        {
            if (application.ProcessName == "InDesign")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {

            
            //Title: '*01_ 90x120_H_PF1-3.indd @ 25 % [Umgewandelt]' ProcessName: 'InDesign
            
            int pTo = application.WindowTitle.LastIndexOf("@");

            if (pTo > 0)
            {
                return application.WindowTitle.Substring(0, pTo - 1).Trim();
            }

            return null;
        }
    }
}
