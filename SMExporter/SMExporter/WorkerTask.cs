using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMExporter
{
    public class WorkerTask
    {
        public TaskType MyTaskType { get; set; }
        public List<string> FilesList { get; set; }

        public WorkerTask()
        {
            MyTaskType = TaskType.READY;
            FilesList = new List<string>();
        }

        public WorkerTask(TaskType taskType)
        {
            MyTaskType = taskType;
        }
    }
}
