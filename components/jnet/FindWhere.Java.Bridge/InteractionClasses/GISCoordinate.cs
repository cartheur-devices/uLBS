using System;

namespace com.teleca.fleetonline.mapping
{

    //*************************************************************************
    //
    // $Source: D:/Data/cvs/FOL5/ejb_module/src/com/teleca/fleetonline/mapping/GISCoordinate.java,v $
    // $Revision: 1.1 $
    // $Date: 2008/04/10 14:27:37 $
    //
    // Copyright : Teydo BV, Bilthoven, all rights reserved worldwide.
    //
    //*************************************************************************
    [System.Serializable]
    public class GISCoordinate
    {

        public const int SPHERE = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int WGS84 = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int NAD27 = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int International = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int Krasovsky = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int Bessel = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int WGS72 = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int WGS66 = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int FAI_sphere = 8;
        /// <summary>
        /// /
        /// </summary>
        public const int User = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int NAD83 = WGS84;
        /// <summary>
        /// 
        /// </summary>
        public const int GRS80 = WGS84;
        /// <summary>
        /// 
        /// </summary>

        public const int FEET = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int METERS = 1;
        /// <summary>
        /// 
        /// </summary>

        public const int NORTH = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int NORTHEAST = 45;
        /// <summary>
        /// 
        /// </summary>
        public const int EAST = 90;
        /// <summary>
        /// 
        /// </summary>
        public const int SOUTHEAST = 135;
        /// <summary>
        /// 
        /// </summary>
        public const int SOUTH = 180;
        /// <summary>
        /// 
        /// </summary>
        public const int SOUTHWEST = 225;
        /// <summary>
        /// 
        /// </summary>
        public const int WEST = 270;
        /// <summary>
        /// 
        /// </summary>
        public const int NORTHWEST = 315;
        /// <summary>
        /// 
        /// </summary>

        public class Ellipsoid
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dispName"></param>
            /// <param name="name"></param>
            /// <param name="a"></param>
            /// <param name="invf"></param>
            public Ellipsoid(string dispName, string name, double a, double invf)
            {
                this.dispName = dispName;
                this.name = name;
                this.a = a;
                this.invf = invf;
            }

            public string dispName;
            /// <summary>
            /// 
            /// </summary>
            public string name;
            /// <summary>
            /// 
            /// </summary>
            public double a;
            /// <summary>
            /// 
            /// </summary>
            public double invf;
        }

        private static Ellipsoid[] ells = new Ellipsoid[10];
        static GISCoordinate()
        {
            ells[0] = new Ellipsoid("Spherical (1'=1nm)", "Sphere", 180 * 60 / Math.PI, double.PositiveInfinity);
            ells[1] = new Ellipsoid("WGS84/NAD83/GRS80", "WGS84", 6378.137 / 1.852, 298.257223563);
            ells[2] = new Ellipsoid("Clarke (1866)/NAD27", "NAD27", 6378.2064 / 1.852, 294.9786982138);
            ells[3] = new Ellipsoid("International", "International", 6378.388 / 1.852, 297.0);
            ells[4] = new Ellipsoid("Krasovsky", "Krasovsky", 6378.245 / 1.852, 298.3);
            ells[5] = new Ellipsoid("Bessel (1841)", "Bessel", 6377.397155 / 1.852, 299.1528);
            ells[6] = new Ellipsoid("WGS72", "WGS72", 6378.135 / 1.852, 298.26);
            ells[7] = new Ellipsoid("WGS66", "WGS66", 6378.145 / 1.852, 298.25);
            ells[8] = new Ellipsoid("FAI sphere", "FAI sphere", 6371.0 / 1.852, 1000000000);
            ells[9] = new Ellipsoid("User Defined", "User", 0, 0); // last one!
        }

        private double _lat = 0.0; // in decimal degrees
        private double _lon = 0.0; // in decimal degrees
        private string _sep = ","; // seperator for printing out the coordinates
        private bool _printLonFirst = false;

        //    
        //	 * in degrees minutes format lat Format - DD.DD, DD:MM.MM or DD:MM:SS.SS lon
        //	 * Format - DD.DD, DD:MM.MM or DD:MM:SS.SS latDir - N/S lonDir - W/E
        //	 
        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public GISCoordinate(String lat, String latDir, String lon, String lonDir) throws Exception
        //public GISCoordinate(string lat, string latDir, string lon, string lonDir)
        //{
        //    setCoordinates(lat, latDir, lon, lonDir);
        //}

        //    *
        //	 * in decimal degrees/rads if rad is true, the entered lat and lon are in
        //	 * radians rather than degrees. otherwise they are in degrees
        //	 
        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public GISCoordinate(double lat, double lon, boolean rad) throws Exception
        public GISCoordinate(double lat, double lon, bool rad)
        {
            if (rad)
            {
                _lat = lat * (180 / Math.PI);
                _lon = lon * (180 / Math.PI);
            }
            else
            {
                _lat = lat;
                _lon = lon;
            }
            verify();
        }

        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public void setCoordinates(String lat, String latDir, String lon, String lonDir) throws Exception
        //public virtual void setCoordinates(string lat, string latDir, string lon, string lonDir)
        //{
        //    _lat = parselatlon(lat) * ((latDir.Equals("N")) ? 1 : -1);
        //    _lon = parselatlon(lon) * ((lonDir.Equals("E")) ? 1 : -1);
        //    verify();
        //}

        // returns latitude in radians
        //public virtual double getLatInRad()
        //{
        //    return _lat * (Math.PI / 180);
        //}

        //// returns longtitude in radians
        //public virtual double getLonInRad()
        //{
        //    return _lon * (Math.PI / 180);
        //}

        //// returns latitude in decimal degrees
        //public virtual double getLatInDecDeg()
        //{
        //    return _lat;
        //}

        //// returns longtitude in decimal degrees
        //public virtual double getLonInDecDeg()
        //{
        //    return _lon;
        //}

        //public virtual string getLatDeg()
        //{
        //    string latS = degtodm(Math.Abs(_lat));
        //    return latS.Substring(0, latS.IndexOf(':'));
        //}

        //public virtual string getLatMin()
        //{
        //    string latS = degtodm(Math.Abs(_lat));
        //    return latS.Substring(latS.IndexOf(':') + 1, latS.LastIndexOf(':'));
        //}

        //public virtual string getLatSec()
        //{
        //    string latS = degtodm(Math.Abs(_lat));
        //    return latS.Substring(latS.LastIndexOf(':') + 1);
        //}

        //public virtual string getLonDeg()
        //{
        //    string lonS = degtodm(Math.Abs(_lon));
        //    return lonS.Substring(0, lonS.IndexOf(':'));
        //}

        //public virtual string getLonMin()
        //{
        //    string lonS = degtodm(Math.Abs(_lon));
        //    return lonS.Substring(lonS.IndexOf(':') + 1, lonS.LastIndexOf(':'));
        //}

        //public virtual string getLonSec()
        //{
        //    string lonS = degtodm(Math.Abs(_lon));
        //    return lonS.Substring(lonS.LastIndexOf(':') + 1);
        //}

        //// print out in degree decimal (West and South are negative)
        //public virtual void printDEGDEC(java.io.PrintStream ps)
        //{
        //    if (_printLonFirst)
        //    {
        //        ps.print(getLonInDecDeg() + _sep + getLatInDecDeg());
        //    }
        //    else
        //    {
        //        ps.print(getLatInDecDeg() + _sep + getLonInDecDeg());
        //    }
        //}

        // print out in degree minute
        //public virtual void printDEGMIN(java.io.PrintStream ps)
        //{
        //    string lats = degtodm(Math.Abs(_lat));
        //    string lons = degtodm(Math.Abs(_lon));
        //    if (_printLonFirst)
        //    {
        //        ps.print(lons + "(" + (_lon < 0 ? "W" : "E") + ")" + _sep + lats + "(" + (_lat < 0 ? "S" : "N") + ")");
        //    }
        //    else
        //    {
        //        ps.print(lats + "(" + (_lat < 0 ? "S" : "N") + ")" + _sep + lons + "(" + (_lon < 0 ? "W" : "E") + ")");
        //    }
        //}

        //public virtual string getPrintSeperator()
        //{
        //    return _sep;
        //}

        //// used to set what the seperator is for printing a coordinate pair
        //public virtual void setPrintSeperator(string sep)
        //{
        //    _sep = sep;
        //}

        //public virtual bool getPrintLonFirst()
        //{
        //    return _printLonFirst;
        //}

        //public virtual void setPrintLonFirst(bool f)
        //{
        //    _printLonFirst = f;
        //}

        ////    
        ////	 * moves this GISCoordinate by distft Feet, to the d distft - how far in
        ////	 * feet to move direction - which directions in degrees to move to model -
        ////	 * which calculation datum to use
        ////	 
        ////JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        ////ORIGINAL LINE: public void move(double distance, int distanceIn, double direction, int model) throws Exception
        //public virtual void move(double distance, int distanceIn, double direction, int model)
        //{

        //    if (distanceIn != FEET && distanceIn != METERS)
        //    {
        //        Console.WriteLine("Distance to move should be in feet or meters.");
        //        // log
        //        // for now assume it is in feet and leave responsibility to caller
        //    }
        //    if (distanceIn == METERS)
        //    {
        //        distance = this.convertMetersToFeet(distance);
        //    }

        //    // distance should be feet here
        //    double distNM = distance / (185200.0 / 30.48); // convert to nm (does
        //    // that stand for
        //    // nautical miles??)

        //    double dirRAD = direction * Math.PI / 180; // radians

        //    Ellipsoid ellipse = ells[model];

        //    if (ellipse.name.Equals("Sphere"))
        //    {
        //        // spherical code
        //        distNM /= (180 * 60 / Math.PI); // in radians
        //        direct(getLatInRad(), -getLonInRad(), dirRAD, distNM); // EAST is
        //        // negative
        //        // for these
        //        // calcs,
        //        // not West
        //        // as is
        //        // normally
        //        // accepted.
        //    }
        //    else
        //    {
        //        // elliptic code
        //        direct_ell(getLatInRad(), getLonInRad(), dirRAD, distNM, ellipse); // ellipse
        //        // uses
        //        // East
        //        // negative
        //    }
        //}

        //      *
        //     * Converts feet to meters.
        //     * Prompt the user for a number of feet.
        //     * Convert the number of feet into meters.
        //     * Display the result of the conversion.
        //     
        public virtual double convertMetersToFeet(double meters)
        {

            // Convert the meters into feet.
            double cm = meters * 100f;
            double inches = cm / 2.54f;

            double feet = inches / 12f;

            return feet;
        }

        //public override string ToString()
        //{
        //    try
        //    {
        //        bool bLatFirst = getPrintLonFirst();
        //        string sep = getPrintSeperator();
        //        setPrintLonFirst(false);
        //        setPrintSeperator(".");
        //        java.io.ByteArrayOutputStream baos = new java.io.ByteArrayOutputStream();
        //        printDEGMIN(new java.io.PrintStream(baos));
        //        baos.Close();
        //        setPrintLonFirst(bLatFirst);
        //        setPrintSeperator(sep);
        //        return baos.ToString();
        //    }
        //    catch (System.Exception t)
        //    {
        //        return t.ToString();
        //    }
        //}

        // NOTE: Character.toString(char) is java 1.4 and not available
        // in java 1.3 !!!!!
        //	public static GISCoordinate FromString(String s) throws Exception {
        //		String lat = s.substring(0, s.indexOf('('));
        //		String latDir = Character.toString(s.charAt(s.indexOf('(') + 1));
        //
        //		String lon = s.substring(s.indexOf(".") + 1, s.lastIndexOf('('));
        //		String lonDir = Character.toString(s.charAt(s.lastIndexOf("(") + 1));
        //		GISCoordinate g = new GISCoordinate(lat, latDir, lon, lonDir);
        //		return g;
        //	}

        // the regular clone method does not support exceptions
        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public GISCoordinate makeClone() throws Exception
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual GISCoordinate makeClone()
        {
            return new GISCoordinate(_lat, _lon, false);
        }

        //    **************************************************************************
        //	 * ******************************************************************
        //	 * ******************************************************************
        //	 * PRIVATE IMPLMENTATION METHODS BELOW.
        //	 * ******************************************************************
        //	 * ******************************************************************
        //	 *************************************************************************

        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private void verify() throws Exception
        private void verify()
        {
            if (Math.Abs(_lat) > 90)
            {
                throw new Exception("latitude cannot exceed 90");
            }
            if (Math.Abs(_lon) > 180)
            {
                throw new Exception("longtitude cannot exceed 180");
            }
        }

        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private void direct(double latRad, double lonRad, double dirRad, double distNM) throws Exception
        private void direct(double latRad, double lonRad, double dirRad, double distNM)
        {
            //JAVA TO VB & C# CONVERTER TODO TASK: Octal literals cannot be represented in C#:
            double EPS = 0.00000000005;
            double dlon, lat, lon;
            if ((Math.Abs(Math.Cos(latRad)) < EPS) && !(Math.Abs(Math.Sin(dirRad)) < EPS))
            {
                throw new Exception("Only N-S courses are meaningful, starting at a pole!");
            }

            lat = Math.Asin(Math.Sin(latRad) * Math.Cos(distNM) + Math.Cos(latRad) * Math.Sin(distNM) * Math.Cos(dirRad));
            if (Math.Abs(Math.Cos(lat)) < EPS)
            {
                lon = 0; // endpoint a pole
            }
            else
            {
                dlon = Math.Atan2(Math.Sin(dirRad) * Math.Sin(distNM) * Math.Cos(latRad), Math.Cos(distNM) - Math.Sin(latRad) * Math.Sin(lat));
                lon = mod(lonRad - dlon + Math.PI, 2 * Math.PI) - Math.PI;
            }

            _lat = lat * (180 / Math.PI);
            _lon = lon * (180 / Math.PI);
        }

//        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//        //ORIGINAL LINE: private void direct_ell(double glat1, double glon1, double faz, double s, Ellipsoid ellipse) throws Exception
//        private void direct_ell(double glat1, double glon1, double faz, double s, Ellipsoid ellipse)
//        {
//        // glat1 initial geodetic latitude in radians N positive
//        // glon1 initial geodetic longitude in radians E positive
//        // faz forward azimuth in radians
//        // s distance in units of a (=nm)

////JAVA TO VB & C# CONVERTER TODO TASK: Octal literals cannot be represented in C#:
//            double EPS = 0.00000000005;
//            double r = 0, tu = 0, sf = 0, cf = 0, b = 0, cu = 0, su = 0, sa = 0, c2a = 0, x = 0, c = 0, d = 0, y = 0, sy = 0, cy = 0, cz = 0, e = 0, a = 0;
//            double glat2, glon2, f;

//            if ((Math.Abs(Math.Cos(glat1)) < EPS) && !(Math.Abs(Math.Sin(faz)) < EPS))
//            {
//                throw new Exception("Only N-S courses are meaningful, starting at a pole!");
//            }

//            a = ellipse.a;
//            f = 1 / ellipse.invf;
//            r = 1 - f;
//            tu = r * Math.Tan(glat1);
//            sf = Math.Sin(faz);
//            cf = Math.Cos(faz);
//            if (cf == 0)
//            {
//                b = 0.;
//            }
//            else
//            {
//                b = 2. * Math.Atan2(tu, cf);
//            }
//            cu = 1. / Math.Sqrt(1 + tu * tu);
//            su = tu * cu;
//            sa = cu * sf;
//            c2a = 1 - sa * sa;
//            x = 1. + Math.Sqrt(1. + c2a * (1. / (r * r) - 1.));
//            x = (x - 2.) / x;
//            c = 1. - x;
//            c = (x * x / 4. + 1.) / c;
//            d = (0.375 * x * x - 1.) * x;
//            tu = s / (r * a * c);
//            y = tu;
//            c = y + 1;
//            while (Math.Abs(y - c) > EPS)
//            {
//                sy = Math.Sin(y);
//                cy = Math.Cos(y);
//                cz = Math.Cos(b + y);
//                e = 2. * cz * cz - 1.;
//                c = y;
//                x = e * cy;
//                y = e + e - 1.;
//                y = (((sy * sy * 4. - 3.) * y * cz * d / 6. + x) * d / 4. - cz) * sy * d + tu;
//            }

//            b = cu * cy * cf - su * sy;
//            c = r * Math.Sqrt(sa * sa + b * b);
//            d = su * cy + cu * sy * cf;
//            glat2 = modlat(Math.Atan2(d, c));
//            c = cu * cy - su * sy * cf;
//            x = Math.Atan2(sy * sf, c);
//            c = ((-3. * c2a + 4.) * f + 4.) * c2a * f / 16.;
//            d = ((e * cy * c + cz) * sy * c + y) * sa;
//            glon2 = modlon(glon1 + x - (1. - c) * d * f); // fix date line
//                                                        // problems	
//            _lat = glat2 * (180 / Math.PI);
//            _lon = glon2 * (180 / Math.PI);
//        }

        private double mod(double x, double y)
        {
            return x - y * Math.Floor(x / y);
        }

        private double modlat(double x)
        {
            return mod(x + Math.PI / 2, 2 * Math.PI) - Math.PI / 2;
        }

        private double modlon(double x)
        {
            return mod(x + Math.PI, 2 * Math.PI) - Math.PI;
        }

        //JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private double parselatlon(String str) throws Exception
        //private double parselatlon(string str)
        //{
        //    // Parse string in the format: dd:mm:ss.sssss
        //    double ret = 0;
        //    java.util.StringTokenizer tok = new java.util.StringTokenizer(str, ":");
        //    if (tok.hasMoreTokens())
        //    {
        //        try
        //        {
        //            ret += Convert.ToInt32(tok.nextToken()); // degrees
        //        }
        //        catch (NumberFormatException e)
        //        {
        //            throw new Exception("Degrees must be an integer");
        //        }
        //        if (tok.hasMoreTokens())
        //        {
        //            int min = 0;
        //            try
        //            {
        //                min = Convert.ToInt32(tok.nextToken()); // minutes
        //            }
        //            catch (NumberFormatException e)
        //            {
        //                throw new Exception("minutes must be an integer");
        //            }
        //            if (min >= 60)
        //            {
        //                throw new Exception("minutes must be less than 60");
        //            }
        //            ret += min / 60.0;
        //            if (tok.hasMoreTokens())
        //            {
        //                double sec = 0;
        //                try
        //                {
        //                    string nextToken = tok.nextToken();
        //                    int commaPosition = nextToken.IndexOf(",");
        //                    if (commaPosition != -1)
        //                    {
        //                        string before = nextToken.Substring(0, commaPosition);
        //                        string after = nextToken.Substring(commaPosition + 1);
        //                        nextToken = before + "." + after;
        //                    }
        //                    sec = Convert.ToDouble(nextToken); // seconds
        //                }
        //                catch (NumberFormatException e)
        //                {
        //                    throw new Exception("seconds must be in decimal or integer format");
        //                }
        //                if (sec >= 60)
        //                {
        //                    throw new Exception("seconds must be less than 60");
        //                }
        //                ret += sec / 3600.0;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception(str + " is not a valid coordinate format");
        //    }
        //    return ret;
        //}

        // print out in degree radians
        //private string degtodm(double degdec)
        //{
        //    // returns a rounded string DD:MM:SS.SSSSS
        //    int deg = (int)Math.Floor(degdec);
        //    int min = (int)Math.Floor(60.0 * (degdec - deg));
        //    double sec = ((60.0 * (degdec - deg)) - min) * 60;
        //    java.text.NumberFormat nf = new java.text.DecimalFormat();
        //    nf.setMaximumFractionDigits(5);
        //    return int.ToString(deg) + ":" + int.ToString(min) + ":" + nf.format(sec);
        //}

        // tester:
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        //GISCoordinate g = new GISCoordinate("37:55:15", "N", "122:20:59","W");
        //        //			GISCoordinate g = new GISCoordinate(37.913769494211174, -122.34766375445626,false);
        //        //			System.out.print("Original location in decimal degrees: ");
        //        //			g.printDEGDEC(System.out);
        //        //			System.out.println();
        //        //			System.out.print("Original location in degrees: ");
        //        //			g.printDEGMIN(System.out);
        //        //			System.out.println();
        //        //			double movedist = 1000;
        //        //			double movedeg = NORTH;
        //        //			int distanceIn = METERS;
        //        //			System.out.println("Moving " + movedist + (distanceIn==METERS?" meters":" feet") + ", in a direction of " + movedeg + " degrees...");
        //        //			g.move(movedist, METERS, movedeg, NAD83);
        //        //			System.out.print("New location in decimal degrees: ");
        //        //			g.printDEGDEC(System.out);
        //        //			System.out.println();
        //        //			System.out.print("New location in degrees: ");
        //        //			g.printDEGMIN(System.out);
        //        //			movedist = 1000;
        //        //			movedeg = EAST;
        //        //			System.out.println("Moving " + movedist + (distanceIn==METERS?" meters":" feet") + ", in a direction of " + movedeg + " degrees...");
        //        //			g.move(movedist, METERS, movedeg, NAD83);
        //        //			System.out.print("New location in decimal degrees: ");
        //        //			g.printDEGDEC(System.out);
        //        //			System.out.println();
        //        //			System.out.print("New location in degrees: ");
        //        //			g.printDEGMIN(System.out);
        //        //			System.out.println("\ntesting the toString function: " + GISCoordinate.FromString(g.toString()).toString());

        //        //JAVA TO VB & C# CONVERTER TODO TASK: Octal literals cannot be represented in C#:
        //        double latitude = 39.0435613000;
        //        double longitude = -77.3971422000;

        //        LatLong[] result = new LatLong[4];
        //        GISCoordinate g = new GISCoordinate(latitude, longitude, false);
        //        double radius = 1000;
        //        int distanceIn = GISCoordinate.METERS;
        //        int sphereDef = GISCoordinate.WGS84;

        //        g.move(radius, distanceIn, GISCoordinate.NORTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.EAST, sphereDef);
        //        result[0] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.SOUTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.EAST, sphereDef);
        //        result[1] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.SOUTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.WEST, sphereDef);
        //        result[2] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        g = new GISCoordinate(latitude, longitude, false);
        //        g.move(radius, distanceIn, GISCoordinate.NORTH, sphereDef);
        //        g.move(radius, distanceIn, GISCoordinate.WEST, sphereDef);
        //        result[3] = new LatLong(g.getLatInDecDeg(), g.getLonInDecDeg());

        //        for (int i = 0; i < result.length; i++)
        //        {
        //            Console.WriteLine(result[i].getLatitude() + ", " + result[i].getLongitude());
        //        }

        //    }
        //    catch (System.Exception t)
        //    {
        //        t.printStackTrace();
        //    }
        //}

    }
}