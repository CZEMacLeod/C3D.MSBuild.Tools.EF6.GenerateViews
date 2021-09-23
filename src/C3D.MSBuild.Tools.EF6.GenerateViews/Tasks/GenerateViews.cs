using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Design;
//using System.Data.Metadata.Edm;
using System.IO;
using System.Linq;

namespace System.Data.Entity.Design
{
    public enum LanguageOption
    {
        GenerateVBCode,
        GenerateCSharpCode
    }
}

namespace C3D.MSBuild.Tools.EF6.Tasks
{
    public class GenerateViews : Task
    {

        /// <summary>
        /// List of Source Files to scan. This will likely be the contents of the Migrations directory restricted to *.resx and *.edmx files
        /// </summary>
        [Required]
        public ITaskItem[] SourceFiles { get; set; }

        /// <summary>
        /// The folder in which to generate the Views Class file.
        /// </summary>
        /// <remarks>Normally IntermediateOutputPath</remarks>
        [Required]
        public string OutputDirectory { get; set; }

        [Required]
        public string AssemblyName { get; set; }

        public string DefaultNamespace { get; set; }

        //public bool UseEF6 { get; set; }
        public bool Debug { get; set; }

        /// <summary>
        /// The language to generate code in.
        /// </summary>
        /// <remarks>
        /// See Language and DefaultLanguageSourceExtension
        /// </remarks>
        [Required]
        public string Language { get; set; }

        /// <summary>
        /// The file extension to use.
        /// </summary>
        /// <remarks>
        /// See Language and DefaultLanguageSourceExtension
        /// </remarks>
        public string Extension { get; set; }

        /// <summary>
        /// The Source File from SourceFiles that was used.
        /// </summary>
        [Output]
        public ITaskItem SourceFile { get; set; }

        [Output]
        public ITaskItem OutputFile { get; set; }

        private LanguageOption LanguageOption
        {
            get
            {
                switch (Language.ToUpperInvariant())
                {
                    case "VB":
                        return System.Data.Entity.Design.LanguageOption.GenerateVBCode;
                    case "CS":
                    case "C#":
                    default:
                        return System.Data.Entity.Design.LanguageOption.GenerateCSharpCode;
                }
            }
        }

        private string OutputExtension
        {
            get
            {
                if (!string.IsNullOrEmpty(Extension))
                    return Extension.TrimStart('.');    // remove leading . if specified
                switch (LanguageOption)
                {
                    case System.Data.Entity.Design.LanguageOption.GenerateVBCode:
                        return "vb";
                    case System.Data.Entity.Design.LanguageOption.GenerateCSharpCode:
                    default:
                        return "cs";
                }
            }
        }

        public override bool Execute()
        {
            if (Debug) System.Diagnostics.Debugger.Launch();

            SourceFile = SourceFiles.OrderByDescending(s => s.ItemSpec).FirstOrDefault(s => System.IO.Path.GetExtension(s.ItemSpec).ToLowerInvariant() == ".edmx");
            if (SourceFile == null)
            {
                Log.LogWarning("No edmx file specified");
                return true;
            }

            OutputFile = new TaskItem(Path.Combine(OutputDirectory, AssemblyName + ".Views." + OutputExtension));

            String viewsOut = String.Empty;
            string containerName = AssemblyName;
            IList<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError> errors = null;
            if (EdmGen2Library.EdmGen2Library.ValidateAndGenerateViewsEF6(SourceFile.ItemSpec,
                LanguageOption,
                SourceFile.GetMetadata("CustomToolNamespace") ?? DefaultNamespace,
                true,
                out viewsOut,
                out containerName,
                out errors))
            {
                //If errors, will return true
                if (errors.Count != 0)
                {
                    DisplayErrors(errors);
                    return false;
                }
            }

            OutputFile = new TaskItem(Path.Combine(OutputDirectory, containerName + ".Views." + OutputExtension));

            // write out to a file if no errors
            File.WriteAllText(OutputFile.ItemSpec, viewsOut);

            if (errors.Count != 0)
            {
                DisplayErrors(errors);
                return false;
            }

            return true;
        }

        private void DisplayErrors(IEnumerable<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError> errors)
        {
            foreach (var error in errors)
            {
                Log.LogError(null, null, null, SourceFile.ItemSpec, error.Line, error.Column, error.Line, error.Column, error.Message);
            }
        }
    }
}
