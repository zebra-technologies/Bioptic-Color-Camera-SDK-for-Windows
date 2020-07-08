using System;
using System.Windows.Forms;

using System.Reflection;


namespace CameraSDKSampleApp
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            // Get the AssemblyInfo class.
            AssemblyInfo info = new AssemblyInfo();

            // Display the values.
            fistLine.Text = info.Description + ", Version" + info.FileVersion;
            secondLine.Text =info.Title + ", Version" + info.AssemblyVersion;
            thirdLine.Text = info.Copyright;
           
        }        
    }

    #region reading_assemblyInfo_class
    public class AssemblyInfo
    {   
        public string Title, Description, Copyright, AssemblyVersion, FileVersion;

        // Return a particular assembly attribute value.
        public static T GetAssemblyAttribute<T>(Assembly assembly)
            where T : Attribute
        {
            // Get attributes of this type.
            object[] attributes =
                assembly.GetCustomAttributes(typeof(T), true);

            // If we didn't get anything, return null.
            if ((attributes == null) || (attributes.Length == 0))
                return null;

            // Convert the first attribute value into
            // the desired type and return it.
            return (T)attributes[0];
        }

        // Constructors.
        public AssemblyInfo()
            : this(Assembly.GetExecutingAssembly())
        {
        }

        public AssemblyInfo(Assembly assembly)
        {
            // Get values from the assembly.
            AssemblyTitleAttribute titleAttr = GetAssemblyAttribute<AssemblyTitleAttribute>(assembly);
            if (titleAttr != null) Title = titleAttr.Title;

            AssemblyDescriptionAttribute assemblyAttr = GetAssemblyAttribute<AssemblyDescriptionAttribute>(assembly);
            if (assemblyAttr != null) Description =assemblyAttr.Description;

            AssemblyCopyrightAttribute copyrightAttr = GetAssemblyAttribute<AssemblyCopyrightAttribute>(assembly);
            if (copyrightAttr != null) Copyright = copyrightAttr.Copyright;

            AssemblyVersion = assembly.GetName().Version.ToString();

            AssemblyFileVersionAttribute fileVersionAttr = GetAssemblyAttribute<AssemblyFileVersionAttribute>(assembly);
            if (fileVersionAttr != null) FileVersion =
                fileVersionAttr.Version;
        }
    }

    #endregion


}
