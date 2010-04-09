/*
 * Created by SharpDevelop.
 * User: e80655
 * Date: 9/04/2010
 * Time: 11:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SCATS_Monitor
{
    public class WorkerAdmin
    {
        public List<BackgroundWorker> Workers { get; set; }

        public WorkerAdmin()
        {
            this.Workers = new List<BackgroundWorker>();
        }
    }
}
