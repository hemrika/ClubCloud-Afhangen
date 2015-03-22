// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved

using ClubCloud.Afhangen.UILogic.Models;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ClubCloud.Afhangen.Converters
{
    public class ReserveringStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null) //&& value is ReserveringSoort)
            {
                string status = ((ScheduleAppointmentStatus)value).Status;
                ReserveringSoort soort = ReserveringSoort.Overig;
                if (!string.IsNullOrWhiteSpace(status))
                    soort = (ReserveringSoort)Enum.Parse(typeof(ReserveringSoort), status);

                switch(soort)
                {
                    case ReserveringSoort.Afhangen:
                        return "Blue";
                    case ReserveringSoort.Competitie:
                        return "Orange";
                    case ReserveringSoort.Evenement:
                        return "OrangeRed";
                    case ReserveringSoort.Les:
                        return "Yellow";
                    case ReserveringSoort.Mobiel:
                        return "lightblue";
                    case ReserveringSoort.Onderhoud:
                        return "Red";
                    case ReserveringSoort.Overig:
                        return "Purple";
                    case ReserveringSoort.Seizoen:
                        return "Gray";
                    case ReserveringSoort.Toernooi:
                        return "OrangeRed";
                    default:
                        return "Blue";
                }
            }

            return "blue";
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
