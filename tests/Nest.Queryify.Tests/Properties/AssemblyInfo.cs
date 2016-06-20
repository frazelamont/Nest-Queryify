﻿using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Nest.Queryify.Tests")]
[assembly: AssemblyDescription("Provides a mechanism to interact with Elasticsearch via a query object pattern")]
[assembly: AssemblyCompany("Storm ID Ltd")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyProduct("Nest.Queryify.Tests")]

[assembly: ComVisible(false)]
