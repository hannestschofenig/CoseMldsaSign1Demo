# CoseMldsaSign1Demo

This .NET demonstration project illustrates **COSE_Sign1** message signing and verification using **ML-DSA** (Module-Lattice-Based Digital Signature Algorithm) based on draft-ietf-cose-dilithium-11.txt.

## Overview

This application demonstrates:
- Generating ML-DSA key pairs
- Signing payloads with COSE_Sign1 format using ML-DSA
- Verifying COSE_Sign1 messages
- Decoding and inspecting COSE message headers and signatures
- Exporting public and private keys in standard formats (SPKI/PKCS#8)

## Prerequisites

- **.NET Runtime**: .NET 10.0 or later (uses `net10.0` target framework)
- **Platform Support**: ML-DSA must be supported on your platform
- **Operating System**: Tested on Windows

## Getting Started

### 1. Clone or Download the Project

### 2. Restore Dependencies

```bash
dotnet restore
```

This will download the required NuGet packages, including `System.Security.Cryptography.Cose` (specified in the `.csproj` file).

### 3. Build the Project

```bash
dotnet build
```

This compiles the project. Dependencies are automatically restored if not already present.

### 3. Run the Application

**Option 1**: From within the project directory:
```bash
dotnet run
```

**Option 2**: From anywhere, specify the project file:
```bash
dotnet run --project .\CoseMldsaSign1Demo.csproj
```

## What the Program Does

When executed, the program:

1. **Checks ML-DSA Support**: Verifies if ML-DSA is available on your runtime
2. **Generates Keys**: Creates an ML-DSA44 key pair
3. **Creates a COSE_Sign1 Message**: Signs the payload `"Hello COSE_Sign1 + ML-DSA"`
4. **Displays the Signature**: Prints the hex-encoded COSE_Sign1 message
5. **Verifies the Signature**: Decodes and cryptographically verifies the message
6. **Displays Details**: Shows the decoded payload, key identifier, algorithm ID, and signature bytes
7. **Exports Keys**: Displays the public and private keys in standard formats

## Sample Output

```
.NET: 10.x.x
MLDsa.IsSupported: True
COSE_Sign1 (hex): 84a10138...
Verified: True
Decoded payload: Hello COSE_Sign1 + ML-DSA
Decoded kid: 010203
Signature alg (COSE alg id): -260
Signature (hex): ...
ML-DSA Public Key (SPKI, hex): ...
ML-DSA Private Key (PKCS#8, hex): ...
```

## Project Structure

```
CoseMldsaSign1Demo/
├── Program.cs                    # Main application code
├── CoseMldsaSign1Demo.csproj    # Project configuration
├── CoseMldsaSign1Demo.sln       # Solution file
└── README.md                     # This file
```

## Requirements

The project uses the following .NET cryptography APIs (included in .NET 10.0+):

- `System.Security.Cryptography.Cose` - COSE message format support
- `System.Security.Cryptography.MLDsa` - ML-DSA signature algorithm

## References

- [COSE (Concise Signed Object Encryption)](https://tools.ietf.org/html/rfc9052)
- [ML-DSA Standard](https://csrc.nist.gov/publications/detail/fips/204/final)
- [COSE ML-DSA](https://datatracker.ietf.org/doc/draft-ietf-cose-dilithium/)
- [.NET COSE Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.cose)
