﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TracksinoWebScrapper;

namespace TracksineWebScrapperTrial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SeleniumTrial()
        {
            string url = "https://tracksino.com/crazytime";

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            var chromerDriverService = ChromeDriverService.CreateDefaultService();
            chromerDriverService.HideCommandPromptWindow = true;
            var driver = new ChromeDriver(chromerDriverService, chromeOptions);
            driver.Navigate().GoToUrl(url);

            Thread.Sleep(100);
            try
            {
                var rowGroup = driver.FindElement(By.XPath("//table[@class='table b-table table-striped table-bordered']")).FindElement(By.XPath("//tbody[@role='rowgroup']"));
                List<SpinHistory> spinHistories = new List<SpinHistory>();

                for (int i = 1; i <= rowGroup.FindElements(By.XPath("tr")).Count; i++)
                {
                    var currentRow = rowGroup.FindElement(By.XPath($"tr[{i}]"));

                    spinHistories.Add(new SpinHistory()
                    {
                        OccuredAt = currentRow.FindElement(By.XPath("td[1]")).Text,
                        SlotResult = currentRow.FindElement(By.XPath("td[2]")).Text,
                        SpinResult = currentRow.FindElement(By.XPath("td[3]")).FindElement(By.XPath("center")).FindElement(By.XPath("i")).GetAttribute("class").Split('-')[2],
                        Multiplier = currentRow.FindElement(By.XPath("td[4]")).Text,
                        TotalWinners = currentRow.FindElement(By.XPath("td[5]")).Text,
                        TotalPayout = currentRow.FindElement(By.XPath("td[6]")).Text,
                    });
                }

                dgwMain.DataSource = spinHistories;
                driver.Quit();

            }
            catch (Exception exception)
            {
                driver.Quit();
                MessageBox.Show(exception.Message);
            }

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SeleniumTrial();
            Cursor.Current = null;
        }

    }
}
