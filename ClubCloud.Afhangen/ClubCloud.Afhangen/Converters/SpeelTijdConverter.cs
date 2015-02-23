// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ClubCloud.Afhangen.Converters
{
    public class SpeelTijdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            TimeSpan tijd = DateTime.Now.TimeOfDay;
            if (value != null)
            {

                if (value is TimeSpan)
                {
                    tijd = (TimeSpan)value;


                }
                if (value is DateTime)
                {
                    tijd = ((DateTime)value).TimeOfDay;
                }

                return string.Format("{0:D2}:{1:D2}", tijd.Hours, tijd.Minutes);
            }
            else
            {
                return string.Format("{0:D2}:{1:D2}", tijd.Hours, tijd.Minutes);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
