using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DOGPlatform
{
    public partial class FormDataImportLayer : Form
    {
        public FormDataImportLayer(string _sXCM, TypeInputFile _fileType)
        {
            InitializeComponent();
          initializaForm(_sXCM, _fileType);
        }
        string sXCM = "";
        string filePathGeoEarthText = "";
        TypeInputFile fileType;

        void initializaForm(string _sXCM, TypeInputFile _fileType)
        {
            fileType = _fileType;
        }
    }
}
