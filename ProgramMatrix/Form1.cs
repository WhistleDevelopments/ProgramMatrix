using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramMatrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MatrixSpoof(string exe)
        {
            PermissionSet ps = new PermissionSet(PermissionState.None);
            // ps.AddPermission(new System.Security.Permissions.*); // Add Whatever Permissions you want to grant here
            ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            ps.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
            ps.AddPermission(new UIPermission(PermissionState.Unrestricted));
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationName = "WHISTLE_MATRIX";
            setup.ApplicationBase = Application.StartupPath+"";
            setup.ShadowCopyDirectories = Application.StartupPath;
            setup.ShadowCopyFiles = "true";
            Evidence ev = new Evidence();
            
            AppDomain sandbox = AppDomain.CreateDomain("WHISTLE_MATRIX",
                ev,
                setup,
                ps);
            sandbox.ApplyPolicy("WHISTLE_MATRIX");
            sandbox.ExecuteAssembly(exe);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MatrixSpoof("ProgramMatrix.exe");
        }
    }
}
