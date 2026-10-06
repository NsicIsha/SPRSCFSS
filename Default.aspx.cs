using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

using System.Xml;
using System.Xml.Linq;
using Image = System.Drawing.Image;

public partial class _Default : System.Web.UI.Page
{
    //private string GetAddress(string latitude, string longitude)
    //{
    //    //string locationName = "";
    //    //string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude, longitude);
    //    //XElement xml = XElement.Load(url);
    //    //if (xml.Element("status").Value == "OK")
    //    //{
    //    //    locationName = string.Format("{0}",
    //    //        xml.Element("result").Element("formatted_address").Value);
    //    //    txtname.Text = locationName;
    //    //}
    //    //return locationName;

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        ////ReadImageLatitudeAndLongitude();

        ////Image image = Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");

        ////float? dd_longitude = GetLatitude(image);

        ////float? dd_latitude = GetLongitude(image);

        ////Response.Write("Latitudeeeeee :- " + dd_longitude);
        ////Response.Write("<br/>");
        ////Response.Write("Longitudeeee :- " + dd_latitude);



        ////////GetAddress("28.796422", "77.077281");

        //////string filePath = @"TimePhoto_20240403_133112.jpg";
        //////float lat = GetLatitude(filePath)

        //////Console.WriteLine("enter coordinate:");
        ////string coordinate = "28.796422,77.077281"; //Console.ReadLine();

        //////XmlDocument xDoc = new XmlDocument();
        //////xDoc.Load("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + coordinate);

        //////XmlNodeList xNodelst = xDoc.GetElementsByTagName("result");
        //////XmlNode xNode = xNodelst.Item(0);
        //////string FullAddress = xNode.SelectSingleNode("formatted_address").InnerText;
        //////string Number = xNode.SelectSingleNode("address_component[1]/long_name").InnerText;
        //////string Street = xNode.SelectSingleNode("address_component[2]/long_name").InnerText;
        //////string Village = xNode.SelectSingleNode("address_component[3]/long_name").InnerText;
        //////string Area = xNode.SelectSingleNode("address_component[4]/long_name").InnerText;
        //////string County = xNode.SelectSingleNode("address_component[5]/long_name").InnerText;
        //////string State = xNode.SelectSingleNode("address_component[6]/long_name").InnerText;
        //////string Zip = xNode.SelectSingleNode("address_component[8]/long_name").InnerText;
        //////string Country = xNode.SelectSingleNode("address_component[7]/long_name").InnerText;
        //////Console.WriteLine("Full Address: " + FullAddress);
        //////Console.WriteLine("Number: " + Number);
        //////Console.WriteLine("Street: " + Street);
        //////Console.WriteLine("Village: " + Village);
        //////Console.WriteLine("Area: " + Area);
        //////Console.WriteLine("County: " + County);
        //////Console.WriteLine("State: " + State);
        //////Console.WriteLine("Zip: " + Zip);
        //////Console.WriteLine("Country: " + Country);

        //////Console.ReadLine();



    }


    private void ReadImageLatitudeAndLongitude()
    {
        string filePath = @"Images\" + "TimePhoto_20240403_133112.jpg";
        string physicalLocation = Server.MapPath(filePath);
        if (File.Exists(physicalLocation))
        {
            Bitmap bmp = new Bitmap(physicalLocation);
            // set Variable Values
            double? latitdue = null;
            double? longitude = null;

            foreach (PropertyItem propItem in bmp.PropertyItems)
            {
                switch (propItem.Type)
                {
                    case 5:
                        if (propItem.Id == 2) // Latitude Array
                            latitdue = GetLatitudeAndLongitude(propItem);
                        if (propItem.Id == 4) //Longitude Array
                            longitude = GetLatitudeAndLongitude(propItem);
                        break;

                }
            }

            Response.Write("Latitude :- " + latitdue);
            Response.Write("<br/>");
            Response.Write("Latitude :- " + longitude);
        }
    } // ReadImageLatitudeAndLongitude

    private static double? GetLatitudeAndLongitude(PropertyItem propItem)
    {
        Double final;
        try
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);

            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);


            //   float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);


            // final = Convert.ToDouble(Convert.ToDouble(degreesNumerator) + Convert.ToDouble(Convert.ToDouble(minutesNumerator) + Convert.ToDouble(secondsNumerator));



            //return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
            //       (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            //(Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator))

            //(Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +

            double f1,d1, d2, d3;

            d1 = (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator));
            d2 = (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60);
            d3= (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            f1= d1 + d2 + d3;

            //(Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
                   (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));


        }
        catch (Exception)
        {

            return null;
        }
    } // GetLatitudeAndLongitude

    public static float? GetLatitude(Image targetImg)
    {
        try
        {
            //Property Item 0x0001 - PropertyTagGpsLatitudeRef
            PropertyItem propItemRef = targetImg.GetPropertyItem(1);
            //Property Item 0x0002 - PropertyTagGpsLatitude
            PropertyItem propItemLat = targetImg.GetPropertyItem(2);
            return ExifGpsToFloat(propItemRef, propItemLat);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    public static float? GetLongitude(Image targetImg)
    {
        try
        {
            ///Property Item 0x0003 - PropertyTagGpsLongitudeRef
            PropertyItem propItemRef = targetImg.GetPropertyItem(3);
            //Property Item 0x0004 - PropertyTagGpsLongitude
            PropertyItem propItemLong = targetImg.GetPropertyItem(4);
            return ExifGpsToFloat(propItemRef, propItemLong);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
    {
        uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
        uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
        float degrees = degreesNumerator / (float)degreesDenominator;

        uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
        uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
        float minutes = minutesNumerator / (float)minutesDenominator;

        uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
        uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
        float seconds = secondsNumerator / (float)secondsDenominator;

        float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);
        string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
        if (gpsRef == "S" || gpsRef == "W")
            coorditate = 0 - coorditate;
        return coorditate;
    }
}
