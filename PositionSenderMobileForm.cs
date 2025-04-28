using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PositionSenderMobile.GPS;
using PositionSenderMobile.Locations;
using PositionSenderMobile.Configuration;

namespace PositionSenderMobile
{
    public partial class PositionSenderForm : Form
    {
        private AppConfiguration _configuration = null;
        private EventHandler updateDataHandler;
        private GpsPosition _position = null;

        private Gps _gps = new Gps();

        public PositionSenderForm()
        {
            InitializeComponent();
        }

        void UpdateData(object sender, EventArgs args)
        {
            if (_position != null)
            {
                if (_position.SatellitesInSolutionValid &&
                    _position.SatellitesInViewValid &&
                    _position.SatelliteCountValid)
                {
                    satellitesInViewLabel.Text = "Sats: " + _position.GetSatellitesInSolution().Length + "/" +
                        _position.GetSatellitesInView().Length + " (" +
                        _position.SatelliteCount + ")";
                }

                if (_position.LatitudeValid && _position.LongitudeValid)
                {
                    Coordinate crd = new Coordinate(_position.Latitude, _position.Longitude);
                    coordinatesLabel.Text = crd.DegreesMinutes;
                }
            }
        }

        private void PositionSenderForm_Load(object sender, EventArgs e)
        {
            _configuration = AppConfiguration.ApplicationConfiguration();

            hostTextBox.Text = _configuration.WebServiceHostName;
            portTextBox.Text = _configuration.PortNumber.ToString();
            googleAPIKey.Text = _configuration.GoogleMapAPIKey;

            if (!_gps.Opened)
            {
                _gps.Open();

                updateDataHandler = new EventHandler(UpdateData);
                _gps.LocationChanged += new LocationChangedEventHandler(_gps_LocationChanged);
            }
        }

        void _gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            _position = args.Position;
            Invoke(updateDataHandler);
        }

        private void configurationMenuItem_Click(object sender, EventArgs e)
        {
            _configuration.WebServiceHostName = hostTextBox.Text;
            _configuration.PortNumber = Convert.ToInt32(portTextBox.Text);
            _configuration.GoogleMapAPIKey = googleAPIKey.Text;

            _configuration.Save();
        }

        private void sendMenuItem_Click(object sender, EventArgs e)
        {
            double latitude = 52.031694;
            double longitude = 5.165283;

            string uri = String.Format("http://{0}:{1}/services/PositionService", hostTextBox.Text, Convert.ToInt32(portTextBox.Text));
            PositionServiceClient client = new PositionServiceClient(uri);

            if (_position != null)
            {
                if (_position.LatitudeValid && _position.LongitudeValid)
                {
                    latitude = _position.Latitude;
                    longitude = _position.Longitude;
                }
            }
            client.SendPosition(latitude, longitude, remarksTextBox.Text);

            MessageBox.Show("Position sent!");
        }

        private void getMapButton_Click(object sender, EventArgs e)
        {
            Coordinate coordinate = new Coordinate(52.031694, 5.165283);

            if (_position != null)
            {
                if (_position.LatitudeValid && _position.LongitudeValid)
                {
                    coordinate = new Coordinate(_position.Latitude, _position.Longitude);
                }
            }

            MapUrlBuilder builder = new MapUrlBuilder();
            builder.CenterCoordinate = coordinate;
            builder.MapType = "mobile";
            builder.ZoomLevel = 15;
            builder.XSize = mapPictureBox.ClientRectangle.Width;
            builder.YSize = mapPictureBox.ClientRectangle.Height;
            builder.GoogleMapsAPIKey = _configuration.GoogleMapAPIKey;

            LocationMap map = new LocationMap(builder.MapUrl);
            mapPictureBox.Image = map.Map;
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (_gps.Opened)
            {
                _gps.Close();
            }

            this.Close();
        }
    }
}