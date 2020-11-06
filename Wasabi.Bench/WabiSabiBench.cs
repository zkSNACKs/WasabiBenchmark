using System.Linq;
using BenchmarkDotNet.Attributes;
using NBitcoin;
using BenchmarkDotNet.Jobs;
using WalletWasabi.Crypto.Randomness;
using WalletWasabi.Crypto;
using WalletWasabi.Wabisabi;
using WalletWasabi.Helpers;

namespace WalletWasabi.Bench
{
	[SimpleJob(RuntimeMoniker.NetCoreApp31), RankColumn]
	public class WabiSabiBench
	{
		[Params(2)]
		public int k;

		[Params(50)]
		public int bits;

		[Benchmark]
		public void Process()
		{
			Constants.RangeProofWidth = bits;
			var numberOfCredentials = k;
			var rnd = new SecureRandom();
			var sk = new CoordinatorSecretKey(rnd);

			var client = new WabiSabiClient(sk.ComputeCoordinatorParameters(), numberOfCredentials, rnd);

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