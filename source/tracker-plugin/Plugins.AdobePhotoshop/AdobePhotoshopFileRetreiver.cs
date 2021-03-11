using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobePhotoshop
{
    public class AdobePhotoshopFileRetreiver : IDocumentRetreiver
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
            if (application.ProcessName == "Photoshop")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {

            // Title: 'PotPlayerMini64_QfKvPJj1a1.psd bei 66,7% (RGB/8#)' ProcessName: 'Photoshop
            
            int pTo = application.WindowTitle.LastIndexOf(".");

            if (pTo > 0)
            {
                return application.WindowTitle.Substring(0, pTo + 4).Trim();
            }

            return null;
        }
    }
}
