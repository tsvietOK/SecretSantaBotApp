﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SecretSantaBotApp.Extensions
{
    /// <summary>
    /// Provides extensions methods for <see cref="System.Collections.IList" />.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Randomly shuffle <see cref="System.Collections.IList" /> using <see cref="RNGCryptoServiceProvider" />.
        /// </summary>
        /// <typeparam name="T">Specifies the element type of the <see cref="System.Collections.IList" />.</typeparam>
        /// <param name="list">Input <see cref="System.Collections.IList" /> to shuffle.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = box[0] % n;
                n--;
                T value = list[k];
                if (list[k].Equals(list[n]))
                {
                    n++;
                    continue;
                }

                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}