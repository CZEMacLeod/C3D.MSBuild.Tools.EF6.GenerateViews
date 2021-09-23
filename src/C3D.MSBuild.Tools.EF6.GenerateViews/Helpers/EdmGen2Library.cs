/**
 * Copyright (C) 2008, Microsoft Corp.  All Rights Reserved
 * 
 * Contributors:
 *	John Radley  (jradley@jsrsoft.co.uk) : Split into two Class files where EdmGen2Library contains all Entity related work.
 */

namespace EdmGen2Library
{
    using System;
    using System.Collections.Generic;
    //using System.Data.Entity.Design;
    //using System.Data.Mapping;
    //using System.Data.Metadata.Edm;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Collections.ObjectModel;
    using Microsoft.DbContextPackage.Utilities;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Design;

    public class EdmGen2Library
    {
        // a class that understands what the different XML namespaces are for the different EF versions. 
        private static NamespaceManager _namespaceManager = new NamespaceManager();

        //public static bool CodeGen(String edmxFile, LanguageOption languageOption, out String codeOut, out List<Object> errors)
        //{
        //    codeOut = String.Empty;

        //    XDocument xdoc = XDocument.Load(edmxFile);
        //    XElement c = GetCsdlFromEdmx(xdoc);
        //    Version v = _namespaceManager.GetVersionFromEDMXDocument(xdoc);

        //    StringWriter sw = new StringWriter();

        //    errors = new List<Object>();

        //    //
        //    // code-gen uses different classes for V1 and V2 of the EF 
        //    //
        //    if (v == EntityFrameworkVersions.Version1)
        //    {
        //        // generate code
        //        EntityClassGenerator codeGen = new EntityClassGenerator(languageOption);
        //        errors = codeGen.GenerateCode(c.CreateReader(), sw) as List<Object>;
        //    }
        //    else if (v == EntityFrameworkVersions.Version2)
        //    {
        //        EntityCodeGenerator codeGen = new EntityCodeGenerator(languageOption);
        //        errors = codeGen.GenerateCode(c.CreateReader(), sw) as List<Object>;
        //    }

        //    else if (v == EntityFrameworkVersions.Version3)
        //    {
        //        EntityCodeGenerator codeGen = new EntityCodeGenerator(languageOption);
        //        errors = codeGen.GenerateCode(c.CreateReader(), sw) as List<Object>;
        //    }

        //    // output errors
        //    if (errors != null)
        //        return true;

        //    codeOut = sw.ToString();
        //    return false;
        //}

        //public static string ToEdmx(String c, String s, String m, String d)
        //{
        //    // This will strip out any of the xml header info from the xml strings passed in 
        //    XDocument cDoc = XDocument.Load(new StringReader(c));
        //    c = cDoc.Root.ToString();

        //    XDocument sDoc = XDocument.Load(new StringReader(s));
        //    s = sDoc.Root.ToString();

        //    XDocument mDoc = XDocument.Load(new StringReader(m));

        //    // re-write the MSL so it will load in the EDM designer
        //    FixUpMslForEDMDesigner(mDoc.Root);
        //    m = mDoc.Root.ToString();

        //    //Designer is optional
        //    if (!String.IsNullOrEmpty(d))
        //    {
        //        XDocument dDoc = XDocument.Load(new StringReader(d));
        //        d = dDoc.Root.ToString();
        //    }

        //    // get the version to use - we use the root CSDL as the version. 
        //    Version v = _namespaceManager.GetVersionFromCSDLDocument(cDoc);
        //    XNamespace edmxNamespace = _namespaceManager.GetEDMXNamespaceForVersion(v);

        //    // the "Version" attribute in the Edmx element
        //    string edmxVersion = v.Major + "." + v.MajorRevision;

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<edmx:Edmx Version=\"" + edmxVersion + "\"");
        //    sb.Append(" xmlns:edmx=\"" + edmxNamespace.NamespaceName + "\">");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("<edmx:Runtime>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("<edmx:StorageModels>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append(s);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("</edmx:StorageModels>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("<edmx:ConceptualModels>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append(c);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("</edmx:ConceptualModels>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("<edmx:Mappings>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append(m);
        //    sb.Append(Environment.NewLine);
        //    sb.Append("</edmx:Mappings>");
        //    sb.Append(Environment.NewLine);
        //    sb.Append("</edmx:Runtime>");
        //    sb.Append(Environment.NewLine);

        //    if (String.IsNullOrEmpty(d))
        //    {
        //        //If no designer section supplied, then add this default

        //        sb.Append("<edmx:Designer xmlns=\"" + edmxNamespace.NamespaceName + "\">");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<Connection><DesignerInfoPropertySet><DesignerProperty Name=\"MetadataArtifactProcessing\" Value=\"EmbedInOutputAssembly\" /></DesignerInfoPropertySet></Connection>");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<edmx:Options />");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("<edmx:Diagrams />");
        //        sb.Append(Environment.NewLine);
        //        sb.Append("</edmx:Designer>");
        //        sb.Append(Environment.NewLine);
        //    }
        //    else
        //    {
        //        sb.Append(d);
        //    }

        //    sb.Append("</edmx:Edmx>");
        //    sb.Append(Environment.NewLine);

        //    return sb.ToString();
        //}

        //public static void FromEdmx(String edmxFile, out String csdlOut, out String ssdlOut, out String mslOut, out String desOut)
        //{
        //    XDocument xdoc = XDocument.Load(edmxFile);

        //    // select the csdl element
        //    XElement csdl = GetCsdlFromEdmx(xdoc);
        //    csdlOut = csdl.ToString();

        //    // select the ssdl element
        //    XElement ssdl = GetSsdlFromEdmx(xdoc);
        //    ssdlOut = ssdl.ToString();

        //    // select the msl element
        //    XElement msl = GetMslFromEdmx(xdoc);
        //    mslOut = msl.ToString();

        //    //Designer section. Library supports optional agrument, leaving user to ignore or process.
        //    XElement des = GetDesignerFromEdmx(xdoc);
        //    desOut = des.ToString();
        //}

        //public static bool ModelGen(string connectionString, string provider, string modelName, Version version, bool includeForeignKeys, out String modelOut, out List<Object> errors)
        //{
        //    modelOut = String.Empty;
        //    errors = new List<Object>();
        //    IList<EdmSchemaError> ssdlErrors = null;
        //    IList<EdmSchemaError> csdlAndMslErrors = null;

        //    // generate the SSDL
        //    string ssdlNamespace = modelName + "Model.Store";
        //    EntityStoreSchemaGenerator essg = new EntityStoreSchemaGenerator(provider, connectionString, ssdlNamespace);
        //    essg.GenerateForeignKeyProperties = includeForeignKeys;

        //    ssdlErrors = essg.GenerateStoreMetadata(new List<EntityStoreSchemaFilterEntry>(), version);

        //    // detect if there are errors or only warnings from ssdl generation
        //    bool hasSsdlErrors = false;
        //    bool hasSsdlWarnings = false;
        //    if (ssdlErrors != null)
        //    {
        //        foreach (EdmSchemaError e in ssdlErrors)
        //        {
        //            if (e.Severity == EdmSchemaErrorSeverity.Error)
        //            {
        //                hasSsdlErrors = true;
        //            }
        //            else if (e.Severity == EdmSchemaErrorSeverity.Warning)
        //            {
        //                hasSsdlWarnings = true;
        //            }
        //        }
        //    }

        //    // write out errors & warnings
        //    if (hasSsdlErrors && hasSsdlWarnings)
        //    {
        //        errors.AddRange(ssdlErrors);
        //    }

        //    // if there were errors abort.  Continue if there were only warnings
        //    if (hasSsdlErrors)
        //    {
        //        return true;
        //    }

        //    // write the SSDL to a string
        //    StringWriter ssdl = new StringWriter();
        //    XmlWriter ssdlxw = XmlWriter.Create(ssdl);
        //    essg.WriteStoreSchema(ssdlxw);
        //    ssdlxw.Flush();

        //    // generate the CSDL
        //    string csdlNamespace = modelName + "Model";
        //    string csdlEntityContainerName = modelName + "Entities";
        //    EntityModelSchemaGenerator emsg = new EntityModelSchemaGenerator(essg.EntityContainer, csdlNamespace, csdlEntityContainerName);
        //    emsg.GenerateForeignKeyProperties = includeForeignKeys;
        //    csdlAndMslErrors = emsg.GenerateMetadata(version);

        //    // detect if there are errors or only warnings from csdl/msl generation
        //    bool hasCsdlErrors = false;
        //    bool hasCsdlWarnings = false;
        //    if (csdlAndMslErrors != null)
        //    {
        //        foreach (EdmSchemaError e in csdlAndMslErrors)
        //        {
        //            if (e.Severity == EdmSchemaErrorSeverity.Error)
        //            {
        //                hasCsdlErrors = true;
        //            }
        //            else if (e.Severity == EdmSchemaErrorSeverity.Warning)
        //            {
        //                hasCsdlWarnings = true;
        //            }
        //        }
        //    }

        //    // write out errors & warnings
        //    if (hasCsdlErrors || hasCsdlWarnings)
        //    {
        //        errors.AddRange(csdlAndMslErrors);
        //    }

        //    // if there were errors, abort.  Don't abort if there were only warnigns.  
        //    if (hasCsdlErrors)
        //    {
        //        return true;
        //    }

        //    // write CSDL to a string
        //    StringWriter csdl = new StringWriter();
        //    XmlWriter csdlxw = XmlWriter.Create(csdl);
        //    emsg.WriteModelSchema(csdlxw);
        //    csdlxw.Flush();

        //    // write MSL to a string
        //    StringWriter msl = new StringWriter();
        //    XmlWriter mslxw = XmlWriter.Create(msl);
        //    emsg.WriteStorageMapping(mslxw);
        //    mslxw.Flush();

        //    // write csdl, ssdl & msl to the EDMX file. A default Designer section will be added
        //    modelOut = ToEdmx(csdl.ToString(), ssdl.ToString(), msl.ToString(), String.Empty);
        //    return false;
        //}

        //public static bool ValidateAndGenerateViews(String edmxFile, LanguageOption languageOption, bool generateViews, out String viewsOut, out IList<EdmSchemaError> errors)
        //{
        //    viewsOut = String.Empty;

        //    XDocument xdoc = XDocument.Load(edmxFile);
        //    XElement c = GetCsdlFromEdmx(xdoc);
        //    XElement s = GetSsdlFromEdmx(xdoc);
        //    XElement m = GetMslFromEdmx(xdoc);
        //    Version v = _namespaceManager.GetVersionFromEDMXDocument(xdoc);

        //    // load the csdl
        //    XmlReader[] cReaders = { c.CreateReader() };
        //    IList<EdmSchemaError> cErrors = null;
        //    EdmItemCollection edmItemCollection =
        //        MetadataItemCollectionFactory.CreateEdmItemCollection(cReaders, out cErrors);

        //    // load the ssdl 
        //    XmlReader[] sReaders = { s.CreateReader() };
        //    IList<EdmSchemaError> sErrors = null;
        //    StoreItemCollection storeItemCollection =
        //        MetadataItemCollectionFactory.CreateStoreItemCollection(sReaders, out sErrors);

        //    // load the msl
        //    XmlReader[] mReaders = { m.CreateReader() };
        //    IList<EdmSchemaError> mErrors = null;
        //    StorageMappingItemCollection mappingItemCollection = MetadataItemCollectionFactory.CreateStorageMappingItemCollection(edmItemCollection, storeItemCollection, mReaders, out mErrors);

        //    // either pre-compile views or validate the mappings
        //    IList<EdmSchemaError> viewGenerationErrors = null;
        //    if (generateViews)
        //    {
        //        // generate views                
        //        EntityViewGenerator evg = new EntityViewGenerator(languageOption);
        //        using (var writer = new StringWriter())
        //        {
        //            viewGenerationErrors = evg.GenerateViews(mappingItemCollection, writer, v);
        //            viewsOut = writer.ToString();
        //        }
        //    }
        //    else
        //    {
        //        viewGenerationErrors = EntityViewGenerator.Validate(mappingItemCollection, v);
        //    }

        //    // write errors
        //    errors = cErrors.Concat(sErrors).Concat(mErrors).Concat(viewGenerationErrors).ToList().AsReadOnly();
        //    if (errors.Count > 0)
        //        return true;

        //    return false;
        //}

        public static bool ValidateAndGenerateViewsEF6(String edmxFile, 
                                                       LanguageOption languageOption, 
                                                       string defaultNamespace,
                                                       bool generateViews, 
                                                       out String viewsOut, 
                                                       out string containerName,
                                                       out IList<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError> errors)
        {
            viewsOut = String.Empty;

            XDocument xdoc = XDocument.Load(edmxFile);
            XElement c = GetCsdlFromEdmx(xdoc);
            XElement s = GetSsdlFromEdmx(xdoc);
            XElement m = GetMslFromEdmx(xdoc);
            Version v = _namespaceManager.GetVersionFromEDMXDocument(xdoc);

            // load the csdl
            XmlReader[] cReaders = { c.CreateReader() };
            var edmItemCollection = new System.Data.Entity.Core.Metadata.Edm.EdmItemCollection(cReaders);
            containerName = edmItemCollection.OfType<System.Data.Entity.Core.Metadata.Edm.EntityContainer>().FirstOrDefault()?.ToString();

            // load the ssdl 
            XmlReader[] sReaders = { s.CreateReader() };
            var storeItemCollection = new System.Data.Entity.Core.Metadata.Edm.StoreItemCollection(sReaders);

            // load the msl
            XmlReader[] mReaders = { m.CreateReader() };
            var mappingItemCollection = new System.Data.Entity.Core.Mapping.StorageMappingItemCollection(edmItemCollection, storeItemCollection, mReaders);

            // either pre-compile views or validate the mappings
            errors = new List<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError>();
            if (generateViews)
            {
                // generate views
                var views = mappingItemCollection.GenerateViews(errors);

                foreach (var error in errors)
                {
                    if (error.Severity == System.Data.Entity.Core.Metadata.Edm.EdmSchemaErrorSeverity.Error)
                    {
                        return true;
                    }
                }

                var contextTypeName = (string.IsNullOrEmpty(defaultNamespace) ? string.Empty : defaultNamespace + ".") + containerName;

                IViewGenerator evg = languageOption == LanguageOption.GenerateCSharpCode ?
                    (IViewGenerator)new CSharpViewGenerator() :
                    new VBViewGenerator();

                evg.ContextTypeName = contextTypeName;
                evg.MappingHashValue = mappingItemCollection.ComputeMappingHashValue();
                evg.Views = views;

                viewsOut = evg.TransformText();
            }

            if (errors.Count > 0)
                return true;

            return false;
        }


        #region Code to extract the csdl, ssdl & msl sections from an EDMX file

        private static XElement GetCsdlFromEdmx(XDocument xdoc)
        {
            Version version = _namespaceManager.GetVersionFromEDMXDocument(xdoc);
            string csdlNamespace = _namespaceManager.GetCSDLNamespaceForVersion(version).NamespaceName;
            return (from item in xdoc.Descendants(XName.Get("Schema", csdlNamespace)) select item).First();
        }

        private static XElement GetSsdlFromEdmx(XDocument xdoc)
        {
            Version version = _namespaceManager.GetVersionFromEDMXDocument(xdoc);
            string ssdlNamespace = _namespaceManager.GetSSDLNamespaceForVersion(version).NamespaceName;
            return (from item in xdoc.Descendants(XName.Get("Schema", ssdlNamespace)) select item).First();
        }

        private static XElement GetMslFromEdmx(XDocument xdoc)
        {
            Version version = _namespaceManager.GetVersionFromEDMXDocument(xdoc);
            string mslNamespace = _namespaceManager.GetMSLNamespaceForVersion(version).NamespaceName;
            return (from item in xdoc.Descendants(XName.Get("Mapping", mslNamespace)) select item).First();
        }

        private static XElement GetDesignerFromEdmx(XDocument xdoc)
        {
            Version version = _namespaceManager.GetVersionFromEDMXDocument(xdoc);
            string desNamespace = _namespaceManager.GetDESNamespaceForVersion(version).NamespaceName;
            var des = (from item in xdoc.Descendants(XName.Get("Designer", desNamespace)) select item).First();
            return des;
        }

        #endregion

        #region "fix-up" code to fix up MSL so that it will load in the EDMX designer

        //
        // This will re-write MSL to remove some syntax that the EDM Designer 
        // doesn't support.  Specifically, the designer doesn't support 
        //     - the "TypeName" attribute in "EntitySetMapping" elements
        //     - the "StoreEntitySet" attribute in "EntityTypeMapping" and 
        //       "EntitySetMapping" elements.   
        //
        private static void FixUpMslForEDMDesigner(XElement mappingRoot)
        {

            XName n1 = XName.Get("EntityContainerMapping", mappingRoot.Name.NamespaceName);
            XName n2 = XName.Get("EntitySetMapping", mappingRoot.Name.NamespaceName);
            XName n3 = XName.Get("EntityTypeMapping", mappingRoot.Name.NamespaceName);

            foreach (XElement e1 in mappingRoot.Elements(n1))
            {
                // process EntitySetMapping nodes
                foreach (XElement e2 in e1.Elements(n2))
                {
                    XAttribute typeNameAttribute = null;
                    XAttribute storeEntitySetAttribute = null;

                    foreach (XAttribute a in e2.Attributes())
                    {
                        if (a.Name.Equals(XName.Get("TypeName", "")))
                        {
                            typeNameAttribute = a;
                            break;
                        }
                    }

                    if (typeNameAttribute != null)
                    {
                        FixUpEntitySetMapping(typeNameAttribute, e2);
                    }

                    // process EntityTypeMappings
                    foreach (XElement e3 in e2.Elements(n3))
                    {
                        foreach (XAttribute a in e3.Attributes())
                        {
                            if (a.Name.Equals(XName.Get("StoreEntitySet", "")))
                            {
                                storeEntitySetAttribute = a;
                                break;
                            }
                        }

                        if (storeEntitySetAttribute != null)
                        {
                            FixUpEntityTypeMapping(storeEntitySetAttribute, e3);
                        }
                    }
                }
            }
        }

        private static void FixUpEntitySetMapping(
            XAttribute typeNameAttribute, XElement entitySetMappingNode)
        {
            XName xn = XName.Get("EntityTypeMapping", entitySetMappingNode.Name.NamespaceName);

            typeNameAttribute.Remove();
            XElement etm = new XElement(xn);
            etm.Add(typeNameAttribute);

            // move the "storeEntitySet" attribute into the new 
            // EntityTypeMapping node
            foreach (XAttribute a in entitySetMappingNode.Attributes())
            {
                if (a.Name.LocalName == "StoreEntitySet")
                {
                    a.Remove();
                    etm.Add(a);
                    break;
                }
            }

            // now move all descendants into this node
            ReparentChildren(entitySetMappingNode, etm);

            entitySetMappingNode.Add(etm);
        }

        private static void FixUpEntityTypeMapping(
            XAttribute storeEntitySetAttribute, XElement entityTypeMappingNode)
        {
            XName xn = XName.Get("MappingFragment", entityTypeMappingNode.Name.NamespaceName);
            XElement mf = new XElement(xn);

            // move the StoreEntitySet attribute into this node
            storeEntitySetAttribute.Remove();
            mf.Add(storeEntitySetAttribute);

            // now move all descendants into this node
            ReparentChildren(entityTypeMappingNode, mf);

            entityTypeMappingNode.Add(mf);
        }

        private static void ReparentChildren(
            XContainer originalParent, XContainer newParent)
        {
            // re-parent all descendants from originalParent into newParent
            List<XNode> childNodes = new List<XNode>();
            foreach (XNode d in originalParent.Nodes())
            {
                childNodes.Add(d);
            }
            foreach (XNode d in childNodes)
            {
                d.Remove();
                newParent.Add(d);
            }
        }
        #endregion

        public static string GetFileNameWithNewExtension(FileInfo file, string extension)
        {
            string prefix = file.Name.Substring(0, file.Name.Length - file.Extension.Length);
            return prefix + extension;
        }

        public static string GetFileExtensionForLanguageOption(LanguageOption langOption)
        {
            if (langOption == LanguageOption.GenerateCSharpCode)
            {
                return ".cs";
            }
            else
            {
                return ".vb";
            }
        }

        public static bool GetError(Object e, out String message)
        {
            message = String.Empty;
            var error = e as EdmSchemaError;
            if (error != null)
            {
                message = error.Message;
                var hasError = error.Severity == EdmSchemaErrorSeverity.Error;
                return hasError;
            }
            return false;
        }
    }

    public class NamespaceManager
    {
        private static Version v1 = EntityFrameworkVersions.Version1;
        private static Version v2 = EntityFrameworkVersions.Version2;
        private static Version v3 = EntityFrameworkVersions.Version3;

        private Dictionary<Version, XNamespace> _versionToCSDLNamespace = new Dictionary<Version, XNamespace>()
        {
        { v1, XNamespace.Get("http://schemas.microsoft.com/ado/2006/04/edm") },
        { v2, XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/edm") },
        { v3, XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm") }
        };

        private Dictionary<Version, XNamespace> _versionToSSDLNamespace = new Dictionary<Version, XNamespace>()
        {
        { v1, XNamespace.Get("http://schemas.microsoft.com/ado/2006/04/edm/ssdl") },
        { v2, XNamespace.Get("http://schemas.microsoft.com/ado/2009/02/edm/ssdl") },
        { v3, XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm/ssdl") }
        };

        private Dictionary<Version, XNamespace> _versionToMSLNamespace = new Dictionary<Version, XNamespace>()
        {
        { v1, XNamespace.Get("urn:schemas-microsoft-com:windows:storage:mapping:CS") },
        { v2, XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/mapping/cs") },
        { v3, XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/mapping/cs") }
        };

        private Dictionary<Version, XNamespace> _versionToEDMXNamespace = new Dictionary<Version, XNamespace>()
        {
        { v1, XNamespace.Get("http://schemas.microsoft.com/ado/2007/06/edmx") },
        { v2, XNamespace.Get("http://schemas.microsoft.com/ado/2008/10/edmx") },
        { v3, XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edmx") }
        };

        private Dictionary<XNamespace, Version> _namespaceToVersion = new Dictionary<XNamespace, Version>();

        internal NamespaceManager()
        {
            foreach (KeyValuePair<Version, XNamespace> kvp in _versionToCSDLNamespace)
            {
                _namespaceToVersion.Add(kvp.Value, kvp.Key);
            }

            foreach (KeyValuePair<Version, XNamespace> kvp in _versionToSSDLNamespace)
            {
                _namespaceToVersion.Add(kvp.Value, kvp.Key);
            }

            foreach (KeyValuePair<Version, XNamespace> kvp in _versionToMSLNamespace)
            {
                _namespaceToVersion.Add(kvp.Value, kvp.Key);
            }

            foreach (KeyValuePair<Version, XNamespace> kvp in _versionToEDMXNamespace)
            {
                _namespaceToVersion.Add(kvp.Value, kvp.Key);
            }
        }

        internal Version GetVersionFromEDMXDocument(XDocument xdoc)
        {
            XElement el = xdoc.Root;
            if (el.Name.LocalName.Equals("Edmx") == false)
            {
                throw new ArgumentException("Unexpected root node local name for edmx document");
            }
            return this.GetVersionForNamespace(el.Name.Namespace);
        }

        internal Version GetVersionFromCSDLDocument(XDocument xdoc)
        {
            XElement el = xdoc.Root;
            if (el.Name.LocalName.Equals("Schema") == false)
            {
                throw new ArgumentException("Unexpected root node local name for csdl document");
            }
            return this.GetVersionForNamespace(el.Name.Namespace);
        }

        internal XNamespace GetMSLNamespaceForVersion(Version v)
        {
            XNamespace n;
            _versionToMSLNamespace.TryGetValue(v, out n);
            return n;
        }

        internal XNamespace GetCSDLNamespaceForVersion(Version v)
        {
            XNamespace n;
            _versionToCSDLNamespace.TryGetValue(v, out n);
            return n;
        }

        internal XNamespace GetSSDLNamespaceForVersion(Version v)
        {
            XNamespace n;
            _versionToSSDLNamespace.TryGetValue(v, out n);
            return n;
        }

        internal XNamespace GetEDMXNamespaceForVersion(Version v)
        {
            XNamespace n;
            _versionToEDMXNamespace.TryGetValue(v, out n);
            return n;
        }

        //Designer namespace is same as EDMX
        internal XNamespace GetDESNamespaceForVersion(Version v)
        {
            XNamespace n;
            _versionToEDMXNamespace.TryGetValue(v, out n);
            return n;
        }

        internal Version GetVersionForNamespace(XNamespace n)
        {
            Version v;
            _namespaceToVersion.TryGetValue(n, out v);
            return v;
        }
    }
}