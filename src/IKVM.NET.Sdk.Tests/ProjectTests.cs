﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

using Buildalyzer;
using Buildalyzer.Environment;

using FluentAssertions;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IKVM.NET.Sdk.Tests
{

    [TestClass]
    public class ProjectTests
    {

        /// <summary>
        /// Forwards MSBuild events to the test context.
        /// </summary>
        class TargetLogger : Logger
        {

            readonly TestContext context;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="context"></param>
            /// <exception cref="ArgumentNullException"></exception>
            public TargetLogger(TestContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public override void Initialize(IEventSource eventSource)
            {
                eventSource.AnyEventRaised += (sender, evt) => context.WriteLine($"{evt.Message}");
            }

        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Can_build_test_project()
        {
            var properties = File.ReadAllLines("IKVM.NET.Sdk.Tests.properties").Select(i => i.Split('=', 2)).ToDictionary(i => i[0], i => i[1]);

            var nugetPackageRoot = Path.Combine(Path.GetTempPath(), "IKVM.NET.Sdk.Tests", "nuget", "packages");
            if (Directory.Exists(nugetPackageRoot))
                Directory.Delete(nugetPackageRoot, true);
            Directory.CreateDirectory(nugetPackageRoot);

            var ikvmCachePath = Path.Combine(Path.GetTempPath(), "IKVM.NET.Sdk.Tests", "ikvm", "cache");
            if (Directory.Exists(ikvmCachePath))
                Directory.Delete(ikvmCachePath, true);

            var ikvmExportCachePath = Path.Combine(Path.GetTempPath(), "IKVM.NET.Sdk.Tests", "ikvm", "expcache");
            if (Directory.Exists(ikvmExportCachePath))
                Directory.Delete(ikvmExportCachePath, true);

            var targets = new[]
            {
                ("net461",          "win7-x64"),
                ("netcoreapp3.1",   "win7-x64"),
                ("netcoreapp3.1",   "linux-x64"),
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                targets = new[]
                {
                    ("netcoreapp3.1",   "win7-x64"),
                    ("netcoreapp3.1",   "linux-x64"),
                };
            }

            // write out nuget.config with package sources
            // can't use property for sdk resolver
            new XDocument(
                new XElement("configuration",
                    new XElement("config",
                        new XElement("add",
                            new XAttribute("key", "globalPackagesFolder"),
                            new XAttribute("value", nugetPackageRoot))),
                    new XElement("packageSources",
                        new XElement("add",
                            new XAttribute("key", "dev"),
                            new XAttribute("value", Path.GetFullPath(@"nuget"))))))
                .Save(Path.Combine(@"Project", "nuget.config"));

            var manager = new AnalyzerManager();
            var analyzer = manager.GetProject(Path.Combine(@"Project", "Exe", "ProjectExe.msbuildproj"));
            analyzer.SetGlobalProperty("IkvmCacheDir", ikvmCachePath + Path.DirectorySeparatorChar);
            analyzer.SetGlobalProperty("IkvmExportCacheDir", ikvmExportCachePath + Path.DirectorySeparatorChar);
            analyzer.SetGlobalProperty("PackageVersion", properties["PackageVersion"]);
            analyzer.SetGlobalProperty("RestorePackagesPath", nugetPackageRoot + Path.DirectorySeparatorChar);

            // allow NuGet to locate packages in existing global packages folder if set
            // else fallback to standard location
            if (Environment.GetEnvironmentVariable("NUGET_PACKAGES") is string nugetPackagesDir && Directory.Exists(nugetPackagesDir))
                analyzer.SetGlobalProperty("RestoreAdditionalProjectFallbackFolders", nugetPackagesDir);
            else
                analyzer.SetGlobalProperty("RestoreAdditionalProjectFallbackFolders", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages"));

            analyzer.AddBuildLogger(new TargetLogger(TestContext));

            {
                var options = new EnvironmentOptions();
                options.DesignTime = false;
                options.TargetsToBuild.Clear();
                options.TargetsToBuild.Add("Restore");
                options.TargetsToBuild.Add("Clean");
                var results = analyzer.Build(options);
                results.OverallSuccess.Should().Be(true);
            }

            foreach (var (tfm, rid) in targets)
            {
                TestContext.WriteLine("Publishing with TargetFramework {0} and RuntimeIdentifier {1}.", tfm, rid);

                var options = new EnvironmentOptions();
                options.DesignTime = false;
                options.Restore = false;
                options.GlobalProperties.Add("TargetFramework", tfm);
                options.GlobalProperties.Add("RuntimeIdentifier", rid);
                options.TargetsToBuild.Clear();
                options.TargetsToBuild.Add("Build");
                options.TargetsToBuild.Add("Publish");
                var results = analyzer.Build(options);
                results.OverallSuccess.Should().Be(true);
            }
        }

    }

}