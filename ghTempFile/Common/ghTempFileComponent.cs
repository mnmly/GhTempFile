using System;
using System.IO;
using System.Text.RegularExpressions;

using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace MNML
{
    public class ghTempFileComponent : GH_Component
    {
        string PreviousFilename = "";
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>

        public ghTempFileComponent()
          : base("Temp File", "Temp File",
              "Provides Temp File",
              "MNML", "Communication")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Refresh", "R", "Generate new temp file name", GH_ParamAccess.item, false);
            pManager.AddTextParameter("Extension", "E", "File extension", GH_ParamAccess.item, "tmp");
            pManager[0].Optional = true;
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "Temporary Path", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            bool refresh = false;
            string extension = "";
            DA.GetData(0, ref refresh);
            DA.GetData(1, ref extension);
            Regex regex = new Regex(extension + "$");
            string filename = "";
            if (PreviousFilename == "" || !regex.IsMatch(PreviousFilename))
            {
                refresh = true;
            }
            if (refresh)
            {
                filename = GetTempFilePathWithExtension(extension);

            } else
            {
                filename = PreviousFilename;
            }
            PreviousFilename = filename;
            DA.SetData(0, filename);
        }

        public string GetTempFilePathWithExtension(string extension)
        {
            var path = Path.GetTempPath();
            Regex regex = new Regex("^\\.");
            var fileName = Guid.NewGuid().ToString() + "." + regex.Replace(extension, "");
            return Path.Combine(path, fileName);
        }


        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4026e078-b400-401c-ae3c-c484a4faf980"); }
        }
    }
}
