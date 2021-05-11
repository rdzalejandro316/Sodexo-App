using System;
using System.Collections.Generic;
using System.Text;

namespace SodexoApp.Helpers
{
    public class AccDir
    {
        public bool lRun { get; set; }
        public bool lNew { get; set; }
        public bool lEdit { get; set; }
        public bool lDelete { get; set; }
        public bool lSearch { get; set; }
        public bool lRenum { get; set; }
        public bool lPrint { get; set; }
        public bool lExport { get; set; }
        public string codeOpcion { get; set; }
        public AccDir(bool _lRun, bool _lNew, bool _lEdit, bool _lDelete, bool _lSearch, bool _lRenum, bool _lPrint, bool _lExport)
        {
            lRun = _lRun; lNew = _lNew; lEdit = _lEdit; lDelete = _lDelete; lSearch = _lSearch; lRenum = _lRenum; lPrint = _lPrint; lExport = _lExport;
        }
    }
}
