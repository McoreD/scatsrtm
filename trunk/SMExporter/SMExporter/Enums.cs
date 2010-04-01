using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMExporter
{
    public enum TaskType
    {
        EXPORT_TO_ACCESS = 0,
        EXPORT_TO_PLAINTEXT = 1,
        ANALYSE_SM_FILE,
        ANALYSE_SM_FOLDER,
        READY
    }

    public enum Status
    {
        PROCESSING_FILE
    }
}
