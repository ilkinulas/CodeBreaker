using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker {
	public class GameViewMediatorTest {

		private IGameView view;

		[SetUp]
		public void SetUp() {
			view = Substitute.For<IGameView> ();

		}

		[Test]
		public void test() {
			IFortuneTeller fortuneTeller = Substitute.For<IFortuneTeller> ();
			fortuneTeller.tellMe ("").ReturnsForAnyArgs ("NO");

			Assert.AreEqual ("NO", fortuneTeller.tellMe ("Are you telling the truth?"));

			IHttpClient httpClient = Substitute.For<IHttpClient> ();

			bool isGoogleDown = false;
			OnSuccess successCalback = s => {
				isGoogleDown = false;
			};
			OnError errorCallback = e => {
				isGoogleDown = true;
			};

			httpClient.When (
				client => client.Get ("http://www.google.com", successCalback, errorCallback)
			).Do (callInfo => errorCallback(new Exception("Google is down!")));

			httpClient.Get ("http://www.google.com", successCalback, errorCallback);

			Assert.IsTrue (isGoogleDown);


			httpClient.Received ().Get ("http://www.google.com", successCalback, errorCallback);

			//Check Get is called a specific number of times
			httpClient.Received (1).Get ("http://www.google.com", successCalback, errorCallback);

			httpClient.DidNotReceive (1).Get ("http://www.google.com", successCalback, errorCallback);
		}



	}

	public delegate void OnSuccess(string response);
	public delegate void OnError(Exception error);

	public interface IHttpClient {	
		void Get (string url, OnSuccess successCallback, OnError errorCallback);
	}

	public interface IFortuneTeller {
		string tellMe(string question);
	}

	public interface IAccountManager {
		Account GetAccount (string name);	
	}

	public class Account {
		public int x;
	}
	public interface InputInterface {
		Vector3 MousePosition();
	}
}
