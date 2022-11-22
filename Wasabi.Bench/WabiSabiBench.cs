using System.Linq;
using System.Threading;
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
	[SimpleJob(RuntimeMoniker.Net60), RankColumn]
	public class WabiSabiBench
	{
		[Params(134375000000, 2150000000000L, 4300000000000)]
		public long upperBound;  
			
		[Benchmark]
		public void Process()
		{
			var rnd = new SecureRandom();
			var sk = new CredentialIssuerSecretKey(rnd);

			var client = new WabiSabiClient(sk.ComputeCredentialIssuerParameters(), rnd, upperBound);

			var (zeroCredentialRequest, zeroValidationData) = client.CreateRequestForZeroAmount();
			var issuer = new CredentialIssuer(sk, rnd, upperBound);
			var zeroCredentialResponse = issuer.HandleRequest(zeroCredentialRequest);
			var zeroCredentials = client.HandleResponse(zeroCredentialResponse, zeroValidationData);

			var credentialsToPresent = zeroCredentials.Take(2);
			var (realCredentialsRequest, realValidationData) = client.CreateRequest(new[] { Money.Coins(1).Satoshi }, credentialsToPresent, CancellationToken.None);
			var realCredentialResponse = issuer.HandleRequest(realCredentialsRequest);
			client.HandleResponse(realCredentialResponse, realValidationData);
		}
	}
}