# Publishing SodiumBindings to NuGet.org

This guide describes the manual release process for package maintainers.

## Prerequisites

- Install the .NET SDK.
- Create a NuGet.org API key with the `Push new packages and package versions`
  scope.

## Prepare the release

1. Update `VersionPrefix` in `SodiumBindings/SodiumBindings.csproj` according to
   [Semantic Versioning](https://semver.org/).
2. From the repository root, restore and run tests:

   ```sh
   dotnet restore SodiumBindings.Tests/SodiumBindings.Tests.csproj
   dotnet run --project SodiumBindings.Tests/SodiumBindings.Tests.csproj \
     --configuration Release \
     --no-restore \
     --no-launch-profile
   ```

## Build and inspect the packages

Create the NuGet package and its symbol package:

```shell
dotnet pack SodiumBindings/SodiumBindings.csproj \
  --configuration Release \
  --no-restore \
  --output artifacts
```

- `artifacts/SodiumBindings.major.minor.patch.nupkg`
- `artifacts/SodiumBindings.major.minor.patch.snupkg`

Before publishing, inspect the `.nupkg` with NuGet Package Explorer or as a ZIP
archive and verify that:

- package identity, version, author, description, license, repository, tags,
  and release notes are correct;
- `README.md` renders correctly;
- `lib/net10.0/SodiumBindings.dll` is present in the `.nupkg`;
- the matching portable PDB is present in the `.snupkg`;
- `LICENSE` is present;
- the nuspec declares `libsodium` version 1.0.22 as a dependency;
- no test binaries, build intermediates, or secrets are present.

Optionally upload the package through the NuGet.org web portal without
submitting it, then use the metadata and README preview as a final check.

## Test the local package

Install the exact package into a temporary console application from the
local feed:

```shell
mkdir /tmp/sodium-bindings-smoke-test
cd /tmp/sodium-bindings-smoke-test
dotnet new console
dotnet add package SodiumBindings \
  --version major.minor.patch \
  --source /path/to/sodium-bindings/artifacts
```

## Publish

Export the API key for the current shell and push the package:

```shell
export NUGET_API_KEY="your-scoped-nuget-api-key"
dotnet nuget push artifacts/SodiumBindings.<version>.nupkg \
  --source https://api.nuget.org/v3/index.json
unset NUGET_API_KEY
```

After the push, wait for NuGet.org validation and indexing. Check the package
page, README, dependency list, source repository link, and symbol status. Then
install the published version in a clean project using the public NuGet.org
source and repeat the smoke test.
