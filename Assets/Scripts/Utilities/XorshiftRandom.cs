using System;

namespace Azarashi.Utilities
{
    /*[System.Runtime.InteropServices.ComVisible(true)]
    [Serializable]*/
    public class XorshiftRandom
    {
        private uint x = 123456789;
        private uint y = 362436069;
        private uint z = 521288629;
        private uint w = 88675123;

        public XorshiftRandom() : this((ulong)Environment.TickCount) { }

        public XorshiftRandom(ulong seed)
        {
            SetSeed(seed);
        }

        /// <summary>  
        /// シード値を設定  
        /// </summary>  
        /// <param name="seed">シード値</param>  
        public void SetSeed(ulong seed)
        {
            // x,y,z,wがすべて0にならないようにする  
            x = 521288629u;
            y = (uint)(seed >> 32) & 0xFFFFFFFF;
            z = (uint)(seed & 0xFFFFFFFF);
            w = x ^ z;
        }



        private uint InternalSample()
        {
            uint t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
            return w;
        }




        protected virtual double Sample()
        {
            //Including this division at the end gives us significantly improved
            //random number distribution.
            return (InternalSample() * (1.0 / int.MaxValue));
        }


        /// <summary>  
        /// 乱数を取得  
        /// </summary>  
        /// <returns>乱数</returns>  
        public virtual int Next()
        {   
            return (int)InternalSample();
        }

        public virtual uint Next(uint max)
        {
            return Next(0,max);
        }

        public virtual uint Next(uint min, uint max)
        {
            return ((min + InternalSample()) % max);
        }

        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)(InternalSample() % (byte.MaxValue + 1));
            }
        }
        
        public virtual double NextDouble()
        {
            return Sample();
        }
    }
}