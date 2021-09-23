# C3D.MSBuild.Tools.EF6.GenerateViews

An MSBuild based implementation of the [`Entity Framework 6 Power Tools Community Edition`](https://github.com/ErikEJ/EntityFramework6PowerTools) `Generate Views` command.

[![Build Status](https://dev.azure.com/flexviews/C3D.MSBuild.Tools.EF6.GenerateViews/_apis/build/status/CZEMacLeod.C3D.MSBuild.Tools.EF6.GenerateViews?branchName=main)](https://dev.azure.com/flexviews/C3D.MSBuild.Tools.EF6.GenerateViews/_build/latest?definitionId=74&branchName=main)
[![NuGet package](https://img.shields.io/nuget/v/C3D.MSBuild.Tools.EF6.GenerateViews.svg)](https://nuget.org/packages/C3D.MSBuild.Tools.EF6.GenerateViews)
[![NuGet downloads](https://img.shields.io/nuget/dt/C3D.MSBuild.Tools.EF6.GenerateViews.svg)](https://nuget.org/packages/C3D.MSBuild.Tools.EF6.GenerateViews)

The samples folder shows the use of the package with NETFramework and NETCore (.Net 5)

This is for EntityFramework 6 only - not EFCore

An example of how to get the EDMX file from your context file is given in the program main.
It will write the EDMX for the context to the executable folder.
You can copy this into your application directory.
Ensure it is _not_ set to any build action or have any custom tool attached.

Set the Build Action to EntityView

Set the Custom Tool Namespace to match your database context's namespace.

On build, the Views will be generated in the obj folder and automatically included in the build.

If you are using Code-First migrations, you can add the following to automatically generate views based on the last edmx file in the migrations folder.

```xml
  <ItemGroup>
    <EntityView Include="Migrations\**\*.edmx">
      <CustomToolNamespace>MyApplication.Models.DAL</CustomToolNamespace>
    </EntityView>
  </ItemGroup>
```

## Known Properties

`EntityFramework6GenerateViewsOutputDir` - Defaults to $(IntermediateOutputPath)\EntityViews

## Known Items

* `EntityView` - One or more edmx files used to generate the Views. Only the latest (alphabetically last) file is used.
   * `CustomToolNamespace` - Item Metadata of the `EntityView` item used to determine the namespace of the DBContext. The DBContext name itself is determined from the EDMX file.
