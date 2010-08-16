﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Renci.SshClient.Security;

namespace Renci.SshClient
{
    internal static class Settings
    {
        public static IDictionary<string, Func<Session, KeyExchange>> KeyExchangeAlgorithms { get; private set; }

        public static IDictionary<string, Func<Cipher>> Encryptions { get; private set; }

        public static IDictionary<string, Func<IEnumerable<byte>, HMAC>> HmacAlgorithms { get; private set; }

        public static IDictionary<string, Func<CryptoPublicKey>> HostKeyAlgorithms { get; private set; }

        static Settings()
        {
            Settings.KeyExchangeAlgorithms = new Dictionary<string, Func<Session, KeyExchange>>()
            {
                {"diffie-hellman-group1-sha1", (a) => { return new KeyExchangeDiffieHellman(a);}}
                //"diffie-hellman-group-exchange-sha1"

            };

            Settings.Encryptions = new Dictionary<string, Func<Cipher>>()
            {
                {"3des-cbc", () => { return new CipherTripleDES();}},
                //{"aes128-cbc", () => { return new CipherAES128();}},  //  TODO:   This cipher does not work
            };


            Settings.HmacAlgorithms = new Dictionary<string, Func<IEnumerable<byte>, HMAC>>()
            {
                {"hmac-md5", (key) => { return new System.Security.Cryptography.HMACMD5(key.Take(16).ToArray());}},
                {"hmac-sha1", (key) => { return new System.Security.Cryptography.HMACSHA1(key.Take(20).ToArray());}},
            };

            Settings.HostKeyAlgorithms = new Dictionary<string, Func<CryptoPublicKey>>()
            {
                {"ssh-rsa", () => { return new CryptoPublicKeyRsa();}},
                {"ssh-dsa", () => { return new CryptoPublicKeyDss();}}, //  TODO:   Need to be tested
            };
        }
    }
}
