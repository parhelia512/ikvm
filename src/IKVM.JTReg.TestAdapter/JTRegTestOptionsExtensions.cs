﻿using System.Linq;
using System.Xml.Linq;

using IKVM.JTReg.TestAdapter.Core;

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace IKVM.JTReg.TestAdapter
{

    /// <summary>
    /// Provides options from a RunSettings file.
    /// </summary>
    public static class JTRegTestOptionsExtensions
    {

        const string JTRegConfigurationElementName = "JTRegConfiguration";
        const string PartitionCountElementName = "PartitionCount";
        const string TimeoutFactorElementName = "TimeoutFactor";
        const string ConcurrencyElementName = "Concurrency";
        const string ExcludeListFilesElementName = "ExcludeListFile";
        const string AdditionalExcludeListFilesElementName = "AdditionalExcludeListFile";
        const string IncludeListFilesElementName = "IncludeListFile";
        const string AdditionalIncludeListFilesElementName = "AdditionalIncludeListFile";

        /// <summary>
        /// Returns the <see cref="JTRegTestOptions"/> loaded from the specified <see cref="IRunSettings"/>.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static JTRegTestOptions ToJTRegOptions(this IRunSettings self)
        {
            var x = self?.SettingsXml != null ? XDocument.Parse(self.SettingsXml)?.Root?.Element(JTRegConfigurationElementName) : null;
            var o = new JTRegTestOptions();
            if (x != null)
            {
                if ((int?)x.Element(PartitionCountElementName) is int partitionCount)
                    o.PartitionCount = partitionCount;

                if ((float?)x.Element(TimeoutFactorElementName) is float timeoutFactor)
                    o.TimeoutFactor = timeoutFactor;

                if ((int?)x.Element(ConcurrencyElementName) is int concurrency)
                    o.Concurrency = concurrency;

                var excludeListFilesElements = x.Elements(ExcludeListFilesElementName);
                if (excludeListFilesElements != null && excludeListFilesElements.Any())
                {
                    o.ExcludeListFiles.Clear();
                    o.ExcludeListFiles.AddRange(excludeListFilesElements.Select(i => i.Value));
                }

                var additionalExcludeListFilesElements = x.Elements(AdditionalExcludeListFilesElementName);
                if (additionalExcludeListFilesElements != null && additionalExcludeListFilesElements.Any())
                {
                    o.AdditionalExcludeListFiles.Clear();
                    o.AdditionalExcludeListFiles.AddRange(additionalExcludeListFilesElements.Select(i => i.Value));
                }

                var includeListFilesElements = x.Elements(IncludeListFilesElementName);
                if (includeListFilesElements != null && includeListFilesElements.Any())
                {
                    o.IncludeListFiles.Clear();
                    o.IncludeListFiles.AddRange(includeListFilesElements.Select(i => i.Value));
                }

                var additionalIncludeListFilesElements = x.Elements(AdditionalIncludeListFilesElementName);
                if (additionalIncludeListFilesElements != null && additionalIncludeListFilesElements.Any())
                {
                    o.AdditionalIncludeListFiles.Clear();
                    o.AdditionalIncludeListFiles.AddRange(additionalIncludeListFilesElements.Select(i => i.Value));
                }
            }

            return o;
        }

    }

}
