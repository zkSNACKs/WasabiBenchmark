using System.Linq;
using BenchmarkDotNet.Attributes;
using NBitcoin;
using BenchmarkDotNet.Jobs;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.Crypto;
using WalletWasabi.WabiSabi;
using WalletWasabi.Helpers;
using WalletWasabi.WabiSabi.Crypto;

namespace WalletWasabi.Bench
{
	[SimpleJob(RuntimeMoniker.NetCoreApp50), RankColumn]
	public class WabiSabiBench
	{
		[Params(2)]
		public int k;

		[Benchmark]
		public void Process()
		{
			var numberOfCredentials = k;
			var rnd = new SecureRandom();
			var sk = new CredentialIssuerSecretKey(rnd);

			var client = new WabiSabiClient(sk.ComputeCredentialIssuerParameters(), numberOfCredentials, rnd);

			var (credentialRequest, validationData) = client.CreateRequestForZeroAmount();
			var issuer = new CredentialIssuer(sk, numberOfCredentials, rnd);
			var credentialResponse = issuer.HandleRequest(credentialRequest);
			client.HandleResponse(credentialResponse, validationData);

			var present = client.Credentials.ZeroValue.Take(numberOfCredentials);
			(credentialRequest, validationData) = client.CreateRequest(new[] { Money.Coins(1) }, present);
			var credentialRequested = credentialRequest.Requested.ToArray();
			credentialResponse = issuer.HandleRequest(credentialRequest);
			client.HandleResponse(credentialResponse, validationData);
		}
	}
}