using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MobileTracker")]
[assembly: AssemblyDescription("Prototype of that suggested by menno derieckx")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Cartheur Research Laboaratories")]
[assembly: AssemblyProduct("A Template for a Mobile Client")]
[assembly: AssemblyCopyright("Copyright © CTR 2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
//[assembly: Guid("46a9cbad-67e6-4360-acd6-be5a0768816d")]
[assembly: Guid("E423C783-6491-4700-ABD6-F6E90E39AEEC")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.0.0.*")]

// Below attribute is to suppress FxCop warning "CA2232 : Microsoft.Usage : Add STAThreadAttribute to assembly"
// as Device app does not support STA thread.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2232:MarkWindowsFormsEntryPointsWithStaThread")]
