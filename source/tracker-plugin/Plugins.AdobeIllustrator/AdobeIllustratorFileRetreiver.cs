using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.AdobeIllustrator
{
    public class AdobeIllustratorFileRetreiver : IDocumentRetreiver
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
            if (application.ProcessName == "Illustrator")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {
            
            //11.03.2021 09:59:09 Title: 'knaak_photography_logo_quer_neu_white.ai @ 120,6 % (CMYK/Vorschau) ' ProcessName: 'Illustrator
            
            int pTo = application.WindowTitle.IndexOf("@");

            if (pTo > 0)
            {
                return application.WindowTitle.Substring(0, pTo).Trim();
            }

            return null;
        }
    }
}
