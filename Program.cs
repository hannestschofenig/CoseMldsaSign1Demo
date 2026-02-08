using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Cose;
using System.Text;

Console.WriteLine($".NET: {Environment.Version}");
Console.WriteLine($"MLDsa.IsSupported: {MLDsa.IsSupported}");

if (!MLDsa.IsSupported)
{
    Console.WriteLine("ML-DSA is not supported on this machine/runtime.");
    return;
}

byte[] payload = Encoding.UTF8.GetBytes("Hello COSE_Sign1 + ML-DSA");
byte[] aad = Array.Empty<byte>(); // Associated data (optional)

// Pick one: MLDsa44 / MLDsa65 / MLDsa87
using MLDsa mldsa = MLDsa.GenerateKey(MLDsaAlgorithm.MLDsa44);

// Wrap key for COSE
CoseKey coseKey = new CoseKey(mldsa);
CoseSigner signer = new CoseSigner(coseKey);

// Optional: set kid (COSE header "kid")
signer.ProtectedHeaders.Add(CoseHeaderLabel.KeyIdentifier, new byte[] { 0x01, 0x02, 0x03 });

// Sign embedded payload -> COSE_Sign1 bytes
byte[] coseSign1 = CoseSign1Message.SignEmbedded(payload, signer, aad);
Console.WriteLine($"COSE_Sign1 (hex): {Convert.ToHexString(coseSign1)}");

// Decode and verify
CoseSign1Message msg = CoseMessage.DecodeSign1(coseSign1);
bool ok = msg.VerifyEmbedded(coseKey, aad);

Console.WriteLine($"Verified: {ok}");
Console.WriteLine($"Decoded payload: {Encoding.UTF8.GetString(msg.Content?.ToArray() ?? Array.Empty<byte>())}");

// kid
if (msg.ProtectedHeaders.TryGetValue(CoseHeaderLabel.KeyIdentifier, out CoseHeaderValue kidVal))
{
    byte[] kidBytes = kidVal.GetValueAsBytes();
    Console.WriteLine($"Decoded kid: {Convert.ToHexString(kidBytes)}");
}
else
{
    Console.WriteLine("Decoded kid: <none>");
}

// alg
if (msg.ProtectedHeaders.ContainsKey(CoseHeaderLabel.Algorithm))
{
    int algId = msg.ProtectedHeaders.GetValueAsInt32(CoseHeaderLabel.Algorithm);
    Console.WriteLine($"Signature alg (COSE alg id): {algId}");
}
else
{
    Console.WriteLine("Signature alg: <none>");
}

// Signature bytes
Console.WriteLine($"Signature (hex): {Convert.ToHexString(msg.Signature.ToArray())}");

// ML-DSA key export
Console.WriteLine($"ML-DSA Public Key (SPKI, hex): {Convert.ToHexString(mldsa.ExportSubjectPublicKeyInfo())}");
Console.WriteLine($"ML-DSA Private Key (PKCS#8, hex): {Convert.ToHexString(mldsa.ExportPkcs8PrivateKey())}");

