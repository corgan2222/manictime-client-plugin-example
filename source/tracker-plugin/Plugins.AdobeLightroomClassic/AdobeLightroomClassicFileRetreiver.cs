using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobeLightroomClassic
{
    public class AdobeLightroomClassicFileRetreiver : IDocumentRetreiver
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
            if (application.ProcessName == "Lightroom")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {

            // Title: 'Fotos-2-2-v10 - Adobe Photoshop Lightroom Classic - Entwickeln' ProcessName: 'Lightroom

            int pFromLast = application.WindowTitle.LastIndexOf("-") + "-".Length;
            int pTo = application.WindowTitle.IndexOf(" - ");

            if (pTo > 0)
            {
                return application.WindowTitle.Substring(0, pTo).Trim() + "-" + application.WindowTitle.Substring(pFromLast).Trim();
            }

            return null;
        }
    }
}
