using System.Web.UI.WebControls;

/// <summary>
/// Google keys for different Url's.
/// </summary>
namespace FindWhere.Panel
{
    public class Maps
    {
        public Maps()
        { }

        /// <summary>
        /// Get the key depending on requested map and hostname.
        /// </summary>
        public static void SetMapScript(Literal litBeforeScriptMaps, string usrMap, string url)
        {
            if (usrMap == "googlemaps")
            {
                string mapKey = GoogleGetKey(url);
                //litBeforeScriptMaps.Text = @"<script type='text/javascript' src='http://maps.google.com/maps?file=api&amp;v=2&amp;key=" + mapKey + "' ></script>";
                litBeforeScriptMaps.Text = @"<script type='text/javascript' src='http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=true&amp;key=" + mapKey + "'></script>";
            }
            else if (usrMap == "virtualearth")
            {
                litBeforeScriptMaps.Text = @"<script type='text/javascript' src='http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2'></script>";
            }
        }

        public static string GoogleGetKey(string url)
        {
            string key = "";

            if (url.Contains("panels.findwhere.com"))
            {
                key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
            }
            else
            {

            switch (url.Substring(0, url.LastIndexOf("/")))
            {
                case "http://localhost/Panel_Vapour/Default.aspx":
                    {
                        key = "ABQIAAAAsF2frG6xTvs66Tp9FXdC7BRYJkbN8t_kCZebhd6DLKmPS9-0ABRMwkv1WB1ODTOl_Hfc8Srf0RJTkw";
                        break;
                    }
                case "http://192.168.1.23":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQ5ikm2AzEIQutoTnIO8rT0Rkv8QhSTeZXGsQJrPAvkFX_hIBaJ4OPBSQ";
                        break;
                    }
                case "http://localhost/CellIDMap":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTraBWtCgNzdRHWzp5MztWWq4PFEhQoiNPgULRQ5ICyvJXnSrsXfm3q7A";
                        break;
                    }
                case "http://www.findmyfriends.be/cellid":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhRQkOd9Sn4rAv67W4fHetqw7iEIGxSmfm2TJbMiA6uzeBOeBy3O_h1Ecg";
                        break;
                    }
                case "http://www.findmyfriends.nl/cellid":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTIoA_zckJ3Dp9cv4F8JNIMwHErCRSOJqULRVObBd4DgqmYs5m3UQWAdw";
                        break;
                    }
                case "http://playground.livecontacts.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhS3MYonVYnyqlKQ43CGq-y_d135WhQv_2oDUJ8U6h_zJocJeg_FtNbT5w";
                        break;
                    }
                case "http://playground.livecontacts.com/Public":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTLIWF0FoNmO5xyuxyTf6seufg-zBT2wskjiJy-UQOdeS7Ib-nacKzCfA";
                        break;
                    }
                case "http://playground.livecontacts.com/Pages":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhR2_r-0-IoOwhJ0viGyp9pjZdeMYxQ9hyrgWZbY6WA7VWy4C0jy6kWz-g";
                        break;
                    }
                case "http://playground.livecontacts.com/M_TopMenu":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQswBO2AAu-6aLpBxZJJ-zAMJCDVRSPvoX6Ei3jaIlaiVLVmA1-zDMthQ";
                        break;
                    }
                case "http://playground.livecontacts.com/M_Contacts":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTdiz_qI1xHQu_VbCnbLZBmF3AffhRSS5kZIPCuq5C2LA7Gs1QymKqVgw";
                        break;
                    }
                case "http://playground.livecontacts.com/M_Spots":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQjgP-O_yk_Tz-7zQhrRzeRpC6KxhT0y9aKlCCUbHohdVm3tYP_zJFbwA";
                        break;
                    }
                case "http://theplayground.livecontacts.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTnoFN5DgOz0X9EVQM2RNgsw2aUYRQKQv8MOTvZGa_XQavpjDIp_MaI1w";
                        break;
                    }
                case "http://theplayground.livecontacts.com/Public":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhRPbSlscJQ1D2ckJDnU339VokK3wxQnM3ZrU91bOmQ7iB5k5NvWhkROEA";
                        break;
                    }
                case "http://theplayground.livecontacts.com/Pages":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhSCEZ3Mcgvsv7Fas9w9S7laxA5slxTeR7Q_MwrPf22TXzWDOLkksXC4SA";
                        break;
                    }
                case "http://theplayground.livecontacts.com/M_TopMenu":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQ3N_D_MOWr9ZhgpFszGrHZZy_GPxSexT5LRfgdf9q6ByMBjNJ0i-hDkA";
                        break;
                    }
                case "http://theplayground.livecontacts.com/M_Contacts":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhS99jq3IUnwKSeyuuf0OynekN0DxxQuQPBPaQjHw8Kszy3Z4yFsbVyEfQ";
                        break;
                    }
                case "http://theplayground.livecontacts.com/M_Spots":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTLYrHdtZyqDPT4qNC6xyr4YOI8tBQ6UlttwlUH1gmjwLcmK-xwnnAlDQ";
                        break;
                    }
                case "http://www.livecontacts.com/cellidreport":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTECHCr9KLG6J_qsshJ5hmeGZz-XRRb1-q3zCNeisfq71_9duZEKJV2sQ";
                        break;
                    }
                case "http://192.168.1.12":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhRZRABPcH4kAyUzHRa0fF-CzJfJyhReVzr5naRs4fmXtg1bg0enct_I8A";
                        break;
                    }
                case "http://192.168.1.12/ME":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhRZRABPcH4kAyUzHRa0fF-CzJfJyhReVzr5naRs4fmXtg1bg0enct_I8A";
                        break;
                    }
                case "http://panels.findwhere.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQxs19YKU4Np3JLag_pQns5w1ORVRR2zVdiOE87x6syJdvvi1EjfqOmjw";
                        break;
                    }
                case "http://mogo-test.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhSXxBeGLpZi7-pzTUAZyW3iH-nNHxSdoEszhUqkzHXdnTLi-LVvvIh7-Q";
                        break;
                    }
                case "http://omega-test.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTUBRXASgQYXTsQaD70z4dv4HYuchQ6Jw2IsbNbP4FaNGAlqBU1tlL9xQ";
                        break;
                    }
                case "http://panel50-test.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTH--xYZQGZaefog0GnrzLfeD_RkhQ3mGJpiT7g93WA3efSlgW3WVAS8A";
                        break;
                    }
                case "http://panel50-test.findwhere.net:8080":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhS10t5NINXcqr30WapREjRf8bpjJBT50bot-aRZLQmcWawlKUL-QQpzyA";
                        break;
                    }
                //case "http://panel50-staging.findwhere.net":
                //    {
                //        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhSMXFDUpp7hmqzluIqyMDOa9ZX10BSSnCB7_arfcgO9hFe76AvfwthxwQ";
                //        break;
                //    }
                case "http://panels.findwhere.com/Omega":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQxs19YKU4Np3JLag_pQns5w1ORVRR2zVdiOE87x6syJdvvi1EjfqOmjw";
                        break;
                    }
                case "http://panels.findwhere.com/UK":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
                        break;
                    }
                case "http://panels.findwhere.com/ME":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
                        break;
                    }
                case "http://panels.findwhere.com/FR":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
                        break;
                    }
                case "http://panels.findwhere.com/ES":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
                        break;
                    }
                case "http://panels.findwhere.com/DE":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQqnwNrTlsdATSneKvwQRwe_WJZeBSPJT9GVwYlDqoD7tUiZwYWxharLQ";
                        break;
                    }
                case "http://panel50-demo.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTAXuMLXANtQHY-On019i7DUfQk7BRRuJNAMKbXqFZxDiQxWrLdbW7Q4A";
                        break;
                    }
                case "http://omega.gps-panel.com":
                    {
                        key = "ABQIAAAAP5hsT4kVjyz8C65zve-7GRQBCgf3eWyTci4snjKJOPA-t0TRixQy7aRtrrDg6EysvKPvZwtPVcxcvw";
                        break;
                    }
                case "http://hlsn.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhSIKD5xisA4nAWJqz6xyM3IJEeheRRppyES7ffcNdOgB1iXzEcy2Lnl1Q";
                        break;
                    }
                case "http://fancy.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQq0xDmKPPgOJt0NFZ9E8BYIXacZhRTU_fLPc1wA2LBjxTsKUoa20l6Ow";
                        break;
                    }
                case "http://www.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTrKQ_7gAjPsvMDOhOZT1HFqEmcOhQW99SLY4gWQUFxRtRxnOgiQNzTTQ";
                        break;
                    }
                case "http://mogo.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTfnO-pGQwYOMLXqB5JXzwsL7gxfBQJ04UvYw0sVYKKPpRJfcvdhodqtA";
                        break;
                    }
                case "http://farsi.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTVAtJHuZY6mJB-IecIkCmB8aaUWxQDByoAPQd5LsgCv1AvSEr3pY-uMQ";
                        break;
                    }
                case "http://farsigeotech.gps-panel.com":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhS8766pPHLrka37mt7WLPLSMQgi2xRLbM7W8u5z1foS_SATeUm5ZhkQsw";
                        break;
                    }
                case "http://omega-staging.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhR0XpfvFHs4KzdHwC1_RGm3HkLEhBT5LVX9Q1nuogyV4lbyZ8nvQEIZSQ";
                        break;
                    }
                case "http://hlsn-staging.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQFx_i_TuzDtEpUXofr1BF-v8ADURTeLJrM3ZkuZ9fArQ40zfTaHz5oPQ";
                        break;
                    }
                case "http://fancy-staging.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhTfLYjkokIVHw9xzNPR9_6fxv3lXRSkLAkJQKZ9KdWedo2dVuj1OoTp8Q";
                        break;
                    }
                case "http://mogo-staging.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhQLxasy4HX7Qd8JhgX9xVtkj_k7ChRud68ZjwK7MFyp00qObzYpPFm7Jg";
                        break;
                    }
                case "http://farsi-staging.findwhere.net":
                    {
                        key = "ABQIAAAAxZ2_y5dpXh1ZKoty8ApiyhSOaRATdcSEqDbazqrJT05KJmz0bxRk9irVHGyhCDTUNhOldjx_QvO6-A";
                        break;
                    }
                case "http://test.findwhere.net":
                    {
                        key = "ABQIAAAAFVQLWnIE1rcZSv9j64fIhxQs9fcle2eGTwkjgJvo81JToEEiPxRnR21JgW85MEW7dG6T9oR0_yroKg";
                        break;
                    }
                case "http://panel50-st.teydo.local":
                    {
                        key = "ABQIAAAAFVQLWnIE1rcZSv9j64fIhxQjedGirjDIUuK5HcHds1zBN80grRSZNFyfCjOolW7RgzIWMlX-8qwAWA";
                        break;
                    }
                case "http://panel50-staging.findwhere.net":
                    {
                        key = "ABQIAAAAsF2frG6xTvs66Tp9FXdC7BSMXFDUpp7hmqzluIqyMDOa9ZX10BRzZqpPBqJ3qmrD5hUnzQ4n2xDRjw";
                        break;
                    }
                default:
                    {
                        //key = "notfound";
                        key = "ABQIAAAAsF2frG6xTvs66Tp9FXdC7BRYJkbN8t_kCZebhd6DLKmPS9-0ABRMwkv1WB1ODTOl_Hfc8Srf0RJTkw";
                        break;
                    }
            }
            }
            return key;
        }
    }
}
