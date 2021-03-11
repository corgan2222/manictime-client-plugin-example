using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobeAfterEffects
{
    public class AdobeAfterEffectsFileRetreiver : IDocumentRetreiver
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
            if (application.ProcessName == "AfterFX")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {

            int pFrom = application.WindowTitle.IndexOf(" - ") ;
            //int pTo = application.WindowTitle.LastIndexOf(".") + 3;

            if (pFrom > 0)
            {
                return application.WindowTitle.Substring(pFrom).Trim();
            }

            return null;
        }
    }
}
