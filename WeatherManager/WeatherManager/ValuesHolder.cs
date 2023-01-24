using System;
using System.Collections.Generic;

namespace WeatherManager
{
    public class ValuesHolder
    {
        public List<WeatherForecast> Holder = new List<WeatherForecast>();

        public void Add(WeatherForecast weatherForecast)
        {
            Holder.Add(weatherForecast);
        }

        public List<WeatherForecast> Get()
        {
            return Holder;
        }

        public int GetValuesCount()
        {
            return Holder.Count;
        }

        public void DeleteValue (DateTime dateTime)
        {
            for(int i = 0; i < Holder.Count;i++)
            {
                if (Holder[i].Date == dateTime)
                {
                    Holder.RemoveAt(i);
                }
            }
        }
    }
}
