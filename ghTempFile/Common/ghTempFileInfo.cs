using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace MNML
{
    public class ghTempFileInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "ghTempFile";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("8318db7c-09f5-49ea-8a7d-962b2939e6bc");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
